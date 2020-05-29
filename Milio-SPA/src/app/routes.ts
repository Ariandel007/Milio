import {Routes} from '@angular/router';
import { HeroComponent } from './hero/hero.component';
import { CarerListComponent } from './carers/carer-list/carer-list.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';


export const appRoutes: Routes = [
  {path: '', component: HeroComponent},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  { path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: 'carers', component: CarerListComponent, data: {roles: ['Admin', 'Client']}},
      {path: 'messages', component: MessagesComponent, data: {roles: ['Admin', 'Client', 'Babysitter']}},
    ]
  },
  {path: '**', redirectTo: '', pathMatch: 'full'}
];
