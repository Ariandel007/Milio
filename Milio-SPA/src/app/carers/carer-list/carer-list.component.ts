import { Component, OnInit } from '@angular/core';
import { Carer } from 'src/app/_models/carer';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/_services/user.service';
import { ActivatedRoute } from '@angular/router';

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

}
