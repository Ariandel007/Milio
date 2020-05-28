import { BrowserModule, HammerGestureConfig } from '@angular/platform-browser';
import { NgModule, Injectable } from '@angular/core';
import { FormsModule } from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HeroComponent } from './hero/hero.component';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FooterComponent } from './footer/footer.component';
import { MessagesComponent } from './messages/messages.component';
import { JwtModule } from '@auth0/angular-jwt';
import { AuthService } from './_services/auth.service';
import { AlertifyService } from './_services/alertify.service';
import { CarerListComponent } from './carers/carer-list/carer-list.component';
import { CarerCardComponent } from './carers/carer-card/carer-card.component';
import { AuthGuard } from './_guards/auth.guard';
import { HasRoleDirective } from './_directives/hasRole.directive';
import { RouterModule } from '@angular/router';
import { appRoutes } from './routes';
import { RegisterComponent } from './register/register.component';

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
      RegisterComponent
   ],
   imports: [
      BrowserModule,
      CarouselModule.forRoot(),
      FormsModule,
      HttpClientModule,
      JwtModule.forRoot(\nconfig
   ],
   blacklistedRoutes: [
      'localhost
   ]
}),
      RouterModule.forRoot(appRoutes),
      BsDropdownModule.forRoot(),
      BrowserAnimationsModule

   ],
   providers: [
     AuthService,
     AlertifyService,
     AuthGuard
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
