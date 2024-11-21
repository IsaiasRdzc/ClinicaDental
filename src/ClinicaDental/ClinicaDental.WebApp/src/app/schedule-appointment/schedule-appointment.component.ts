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
  dateSelected: boolean | null = null;

  // Modelos de datos
  firstAppointmentData = {
    id: 0,
    doctorId: 1,
    date: '',
    startTime: "",
    duration: "",
    endTime: '',
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
      //this.fetchAvailableSlots();
    }
  }

  isADateSelected(choice: boolean) {
    this.dateSelected = choice;
    if (choice) {
      this.fetchAvailableSlots();
    }
  }

  fetchAvailableSlots() {
    const doctorId = 1;
    const date = this.firstAppointmentData.date;
    const url = `/api/appointments/availableSlots?doctorId=${doctorId}&date=${date}`;
    this.http.get<string[]>(url)
      .subscribe((slots) => {this.availableSlots = slots
        if (slots.length > 0) {
          this.firstAppointmentData.startTime = slots[0]; 
        }
      });
    
  }

  onSubmitFirstAppointment(form: any) {
    if (form.valid) {
      const appointment = {
        doctorId: this.firstAppointmentData.doctorId,
        date: this.firstAppointmentData.date,
        startTime: this.firstAppointmentData.startTime,
        duration: 1, // Duración fija de 1 hora
        patientName: this.firstAppointmentData.patientName,
        patientPhone: this.firstAppointmentData.patientPhone
      };
  
      this.http.post('/api/appointments', appointment)
        .subscribe(() => alert('Cita registrada exitosamente.'), (error) => {
          console.error('Error registrando la cita:', error);
          alert('Hubo un problema al registrar la cita.');
        });
    }
  }

  onSubmitNormalAppointment(form: any) {
    if (form.valid) {
      this.http.post('/api/appointments', this.appointmentData)
        .subscribe(() => alert('Cita registrada exitosamente.'));
    }
  }
}
