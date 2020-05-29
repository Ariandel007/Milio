import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
// import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {};
  photoUrl: string;
  menuBarActive: boolean = false;

  constructor(public authService: AuthService, private router: Router, private toastr: ToastrService) {}

  ngOnInit() {
    this.authService.currentPhotoUrl.subscribe(photoUrl => this.photoUrl = photoUrl);
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.authService.decodedToken = null;
    this.authService.currentUser = null;
    localStorage.removeItem('token');
    this.toastr.info('log out');
    this.router.navigate(['home']);
    this.model = {};
  }

  toggleMenu(){
    this.menuBarActive = !this.menuBarActive;
  }

}
