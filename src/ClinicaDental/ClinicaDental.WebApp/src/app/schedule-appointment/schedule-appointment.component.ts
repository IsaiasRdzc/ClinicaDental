import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { Observable } from 'rxjs';
import { Appointment } from '../../../models/appointment.model';
import { AsyncPipe } from '@angular/common';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, NgForm } from '@angular/forms';
import { ScheduleAppointmentService } from '../shared/schedule-appointment.service';
import { CommonModule } from '@angular/common';

// @Component({
//   selector: 'app-schedule-appointment',
//   standalone: true,
//   imports: [RouterOutlet, RouterLink, RouterLinkActive, HttpClientModule, AsyncPipe, FormsModule, ReactiveFormsModule, CommonModule],
//   templateUrl: './schedule-appointment.component.html',
//   styleUrls: ['./schedule-appointment.component.css']
// })
// export class ScheduleAppointmentComponent implements OnInit{
//   constructor(public service: ScheduleAppointmentService){

//   }

//   onSubmit(form:NgForm){
//     this.service.scheduleAppointment()
//     .subscribe({
//       next: res=>{
//         this.service.resetForm(form)
        
//       },
//       error: err =>{console.log(err)}
//     })
//   }

//   ngOnInit(): void{
//     this.service.getAllDoctors();
//   }
  
// }

@Component({
  selector: 'app-schedule-appointment',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive, HttpClientModule, AsyncPipe, FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './schedule-appointment.component.html',
  styleUrls: ['./schedule-appointment.component.css']
})
export class AppointmentComponent {
 
  firstAppointment: boolean | null = null;

  // Modelos de datos
  firstAppointmentData = {
    doctorId: 1,
    date: '',
    time: '',
    patientName: '',
    patientPhone: ''
  };

  appointmentData = {
    date: '',
    startTime: '',
    duration: 1,
    patientName: '',
    patientPhone: ''
  };

  availableSlots: string[] = [];

  constructor(private http: HttpClient, public service: ScheduleAppointmentService) {}

  isFirstAppointment(choice: boolean) {
    this.firstAppointment = choice;
    if (choice) {
      this.fetchAvailableSlots();
    }
  }

  fetchAvailableSlots() {
    const doctorId = this.firstAppointmentData.doctorId;
    const date = this.firstAppointmentData.date;

    this.http.get<string[]>(`/api/appointments/availableSlots?doctorId=${doctorId}&date=${date}`)
      .subscribe((slots) => this.availableSlots = slots);
  }

  onSubmitFirstAppointment(form: any) {
    if (form.valid) {
      const appointment = {
        ...this.firstAppointmentData,
        duration: 1
      };

      this.http.post('/api/appointments', appointment)
        .subscribe(() => alert('Cita registrada exitosamente.'));
    }
  }

  onSubmitNormalAppointment(form: any) {
    if (form.valid) {
      this.http.post('/api/appointments', this.appointmentData)
        .subscribe(() => alert('Cita registrada exitosamente.'));
    }
  }
}
