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

}
