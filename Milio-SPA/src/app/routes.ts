import {Routes} from '@angular/router';
import { HeroComponent } from './hero/hero.component';
import { CarerListComponent } from './carers/carer-list/carer-list.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { CarerListResolver } from './_resolvers/carer-list.resolver';
import { MessagesResolver } from './_resolvers/messages.resolver';
import { JobsComponent } from './jobs/jobs.component';


export const appRoutes: Routes = [
  {path: '', component: HeroComponent},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  { path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: 'carers', component: CarerListComponent, data: {roles: ['Admin', 'Client']}, resolve: {carers: CarerListResolver}},
      {path: 'messages', component: MessagesComponent
          , resolve: {messages: MessagesResolver}, data: {roles: ['Admin', 'Client', 'Babysitter']}, },
      {path: 'jobs', component: JobsComponent, data: {roles: ['Admin', 'Babysitter']} },
    ]
  },
  {path: '**', redirectTo: '', pathMatch: 'full'}
];
