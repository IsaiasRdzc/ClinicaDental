import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Appointment } from '../../../models/appointment.model';
import { NgForm } from '@angular/forms';
import { Doctor } from '../../../models/doctor.model';

@Injectable({
  providedIn: 'root'
})
export class ScheduleAppointmentService {
  url: string=environment.apiBaseUrl+"/appointments";
  dentists: Doctor[]=[];
  formData: Appointment = new Appointment();
  constructor(private http: HttpClient) { }

  getAllDoctors(){
    this.http.get(this.url+"/doctor")
    .subscribe({
      next: res=>{
        this.dentists = res as Doctor[]; 
      },
      error: err=>{console.log(err)}
    })
  }
}
