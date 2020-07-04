import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { ContractService } from '../_services/contract.service';
import { Appointment } from '../_models/appointment';

@Component({
  selector: 'app-jobs',
  templateUrl: './jobs.component.html',
  styleUrls: ['./jobs.component.css']
})
export class JobsComponent implements OnInit {

  appointments: Appointment[];
  acepted: any = {};

  constructor(
    private toastr: ToastrService,
    // private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private contractService: ContractService
  ) { }

  ngOnInit(): void {
    this.loadAppointments();
  }

  loadAppointments() {
    this.contractService.getAppointments(this.authService.decodedToken.nameid).subscribe((response) => {
      this.appointments = response;
    }, error => {
        this.toastr.error(error);
      });

  }

  confirmApointemnt(idAppointment: number){
    this.acepted.acepted = true;
    console.log(this.acepted);
    this.contractService.confirmAppointment(this.authService.decodedToken.nameid, idAppointment, this.acepted)
    .subscribe(
      () => {
        this.toastr.success('Trabajo Aceptado');
      },
      error => {
        this.toastr.error(error);
      },
      () => {
          this.router.navigate(['/messages']);
      }
    );
  }

  rejectApointemnt(idAppointment: number){

    this.contractService.deleteAppointment(this.authService.decodedToken.nameid, idAppointment)
    .subscribe(
      () => {
        this.toastr.info('Trabajo Rechazado');
        this.loadAppointments();
      },
      error => {
        this.toastr.error(error);
      },
      () => {
          this.router.navigate(['/jobs']);
      }
    );
  }

}
