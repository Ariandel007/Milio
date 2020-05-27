import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {};
  photoUrl: string;

  constructor(public authService: AuthService) {}

  ngOnInit() {
    // this.authService.currentPhotoUrl.subscribe(photoUrl => this.photoUrl = photoUrl);
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
     //this.alertify.success('Inicio de sesion con exito');
     console.log('Inicio de sesion con exito');
     console.log(this.model);
    }, error => {
      //this.alertify.error(error);
      console.log(error);
    }, () => {
      //this.router.navigate(['/members']);
      console.log('Deberia mandarte a otra pestaña, tal vez la de perfil, depende aun no lo se :c');
    });
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
    // this.alertify.message('log out');
    // this.router.navigate(['home']);
  }

}
