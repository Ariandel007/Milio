import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import {JwtHelperService} from '@auth0/angular-jwt';
import { BehaviorSubject } from 'rxjs';
import { User } from '../_models/user';
import {map} from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { Carer } from '../_models/carer';
import { Client } from '../_models/client';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl + 'auth/';
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  currentUser: User;
  photoUrl = new BehaviorSubject<string>('../../assets/user.jpg');
  currentPhotoUrl = this.photoUrl.asObservable();

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post(this.baseUrl + 'login', model).pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('token', user.token);
          localStorage.setItem('user', JSON.stringify(user.user));
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          // this.currentUser = user.user;
          // this.changeMemberPhoto(this.currentUser.photoUrl);
        }
      })
    );
  }

  registerClient(client: Client) {
    return this.http.post(this.baseUrl + 'registerClient', client);
  }

  registerCarer(carer: Carer) {
    return this.http.post(this.baseUrl + 'registerCarer', carer);
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  roleMatch(allowedRoles: Array<string>): boolean {
    let isMatch = false;
    const userRoles = this.decodedToken.role as Array<string>;

    allowedRoles.forEach(element => {
      if (userRoles.includes(element)) {
        isMatch = true;
        return;
      }
    });

    return isMatch;
  }

}
