import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model: any = {};
  photoUrl: string;

  constructor(public authService: AuthService, private router: Router, private toastr: ToastrService) { }

  ngOnInit() {

  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      this.toastr.success('Inicio de sesion con exito');
    }, error => {
      this.toastr.error(error);
    }, () => {
      if (this.authService.decodedToken.role == 'Client') {
        this.router.navigate(['/carers']);
      }
      if (this.authService.decodedToken.role == 'Babysitter') {
        this.router.navigate(['/jobs']);
      }
    });
  }

}
