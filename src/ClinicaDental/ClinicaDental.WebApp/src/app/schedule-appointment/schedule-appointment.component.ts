import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { Observable } from 'rxjs';
import { Appointment } from '../../../models/appointment.model';
import { AsyncPipe } from '@angular/common';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, NgForm } from '@angular/forms';
import { ScheduleAppointmentService } from '../shared/schedule-appointment.service';

@Component({
  selector: 'app-schedule-appointment',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive, HttpClientModule, AsyncPipe, FormsModule, ReactiveFormsModule],
  templateUrl: './schedule-appointment.component.html',
  styleUrls: ['./schedule-appointment.component.css']
})
export class ScheduleAppointmentComponent {
  constructor(public service: ScheduleAppointmentService){

  }

  onSubmit(form:NgForm){
    this.service.scheduleAppointment()
    .subscribe({
      next: res=>{
        this.service.resetForm(form)
        
      },
      error: err =>{console.log(err)}
    })
  }
  
}
