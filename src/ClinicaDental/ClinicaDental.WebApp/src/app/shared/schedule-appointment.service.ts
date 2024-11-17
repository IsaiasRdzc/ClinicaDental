import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Appointment } from '../../../models/appointment.model';
import { NgForm } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class ScheduleAppointmentService {
  url: string=environment.apiBaseUrl+"/appointments";
  formData: Appointment = new Appointment();
  constructor(private http: HttpClient) { }

  scheduleAppointment(){
    return this.http.post(this.url, this.formData);
  }

  resetForm(form: NgForm){
    form.form.reset();
    this.formData = new Appointment();
  }
}
