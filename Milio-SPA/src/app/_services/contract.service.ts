import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Appointment } from '../_models/appointment';

@Injectable({
  providedIn: 'root'
})
export class ContractService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  createAppointment(idClient: number, appointment: Appointment) {
    console.log(appointment);
    return this.http.post(this.baseUrl + 'contracts/' + idClient + '/appointments', appointment);
  }

}
