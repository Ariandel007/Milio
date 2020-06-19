import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { User } from '../_models/user';
import { UserService } from '../_services/user.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';
import { Carer } from '../_models/carer';

@Injectable()
export class CarerListResolver implements Resolve<Carer[]> {
  pageNumber = 1;
  pageSize = 5;

  constructor(private userService: UserService, private router: Router, private toastrService: ToastrService) {}
    resolve(route: ActivatedRouteSnapshot): Observable<Carer[]> {
      return this.userService.getCarers(this.pageNumber, this.pageSize).pipe(
        catchError(error => {
          this.toastrService.error('Problemas recibiendo datos');
          this.router.navigate(['/home']);
          return of(null);
        })
      );
    }
}
