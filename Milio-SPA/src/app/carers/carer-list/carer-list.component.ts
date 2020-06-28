import { Component, OnInit } from '@angular/core';
import { Carer } from 'src/app/_models/carer';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/_services/user.service';
import { ActivatedRoute } from '@angular/router';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { Appointment } from 'src/app/_models/appointment';

@Component({
  selector: 'app-carer-list',
  templateUrl: './carer-list.component.html',
  styleUrls: ['./carer-list.component.css']
})
export class CarerListComponent implements OnInit {
  carers: Carer[];
  genderList = [{value: 'male', display: 'Hombres'}, {value: 'female', display: 'Mujeres'}, {value: '', display: 'Todos'}];
  filterList = [{value: 'lastActive', display: 'Actividad'}, {value: 'created', display: 'Nuevos'}, {value: 'fare', display: 'Tarifa'}];
  userParams: any = {};
  pagination: Pagination;
  carerSelected: Carer;
  bsConfig: Partial<BsDatepickerConfig>;
  dateParams: any = {};
  costInShow: any;
  appointmentToSend: any = {};


  constructor(private toastr: ToastrService, private userService: UserService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.carers = data.carers.result;
      this.pagination = data.carers.pagination;
    });

    this.userParams.gender = '';
    this.userParams.minAge = 18;
    this.userParams.maxAge = 99;
    this.userParams.orderBy = 'lastActive';
    this.userParams.minFareForHour = 1.0;
    this.userParams.maxFareForHour = 99.0;
    this.carerSelected = null;
    this.costInShow = '?';
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadUsers();
  }

  loadUsers() {
    this.userService.getCarers(this.pagination.currentPage, this.pagination.itemsPerPage, this.userParams)
    .subscribe(
      (res: PaginatedResult<Carer[]>) => {
      this.carers = res.result;
      this.pagination = res.pagination;
      }, error => {
        this.toastr.error(error);
      });
  }

  resetFilters() {
    this.userParams.gender = null;
    this.userParams.minAge = 18;
    this.userParams.maxAge = 99;
    this.loadUsers();
  }

  setCarerSelected(carer: Carer)
  {
    this.carerSelected = carer;
  }

  CarerNoSelected()
  {
    this.carerSelected = null;
  }

  setDates()
  {
    this.dateParams.dateStart.setHours(0, 0 , 0, 0);

    let date1 = new Date();
    date1 = this.dateParams.dateStart;
    const date2 = new Date(this.dateParams.dateStart.valueOf());

    this.appointmentToSend.carerId = this.carerSelected.id;
    date1.setHours(this.dateParams.startHour);
    date2.setHours(this.dateParams.endHour);

    console.log(date1);
    console.log(date2);

    this.appointmentToSend.start = date1;
    this.appointmentToSend.end = date2;

    const diff = this.dateParams.endHour - this.dateParams.startHour;
    this.costInShow = diff * this.carerSelected.fareForHour;
  }

  doContract(){
    //inserte apointemnts

  }

}
