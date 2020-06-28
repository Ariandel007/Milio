import { BrowserModule, HammerGestureConfig } from '@angular/platform-browser';
import { NgModule, Injectable } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HeroComponent } from './hero/hero.component';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { FooterComponent } from './footer/footer.component';
import { MessagesComponent } from './messages/messages.component';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { JwtModule } from '@auth0/angular-jwt';
import { AuthService } from './_services/auth.service';
import { CarerListComponent } from './carers/carer-list/carer-list.component';
import { CarerCardComponent } from './carers/carer-card/carer-card.component';
import { AuthGuard } from './_guards/auth.guard';
import { HasRoleDirective } from './_directives/hasRole.directive';
import { RouterModule } from '@angular/router';
import { appRoutes } from './routes';
import { RegisterComponent } from './register/register.component';
import { ToastrModule } from 'ngx-toastr';
import { LoginComponent } from './login/login.component';
import { UserService } from './_services/user.service';
import { CarerListResolver } from './_resolvers/carer-list.resolver';
import { MessagesResolver } from './_resolvers/messages.resolver';
import { ContractService } from './_services/contract.service';
import { JobsComponent } from './jobs/jobs.component';


export function tokenGetter() {
  return localStorage.getItem('token');
}

@Injectable()
export class CustomHammerConfig extends HammerGestureConfig  {
  overrides = {
      pinch: { enable: false },
      rotate: { enable: false }
  };
}


@NgModule({
  declarations: [
     AppComponent,
     NavComponent,
     HeroComponent,
     FooterComponent,
     MessagesComponent,
     CarerListComponent,
     CarerCardComponent,
     HasRoleDirective,
     RegisterComponent,
     LoginComponent,
     JobsComponent
  ],
  imports: [
   BrowserModule,
   CarouselModule.forRoot(),
   FormsModule,
   ReactiveFormsModule,
   HttpClientModule,
   JwtModule.forRoot({
     config: {
       tokenGetter: tokenGetter,
       whitelistedDomains: ['localhost:5001'],
       blacklistedRoutes: ['localhost:5001/api/auth']
     }
   }),
   RouterModule.forRoot(appRoutes),
   BsDropdownModule.forRoot(),
   BrowserAnimationsModule,
   BsDatepickerModule.forRoot(),
   ToastrModule.forRoot(), // ToastrModule added
   ButtonsModule.forRoot(),
   PaginationModule.forRoot()
],
  providers: [
    AuthService,
    AuthGuard,
    UserService,
    ContractService,
    CarerListResolver,
    MessagesResolver
  ],
  bootstrap: [
     AppComponent
  ]
})
export class AppModule { }
