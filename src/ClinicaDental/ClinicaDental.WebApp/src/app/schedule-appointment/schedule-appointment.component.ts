import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { Observable } from 'rxjs';
import { Appointment } from '../../../models/appointment.model';
import { AsyncPipe } from '@angular/common';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, NgForm } from '@angular/forms';
import { ScheduleAppointmentService } from '../shared/schedule-appointment.service';
import { CommonModule } from '@angular/common';
import { routes } from '../app.routes';
import { Modal } from 'bootstrap';
import { Doctor } from '../../../models/doctor.model';

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
export class AppointmentComponent implements OnInit{
  confirmationData: any = {};
  firstAppointment: boolean | null = null;
  dateSelected: boolean | null = null;
  
  //model
  appointmentData = {
    id: 0,
    doctorId: 1,
    patientId: 0,
    date: '',
    startTime: "",
    durationInHours: 0,
    patientName: '',
    patientPhone: ''
  };

  


  availableSlots: string[] = [];

  constructor(private http: HttpClient, public service: ScheduleAppointmentService, private router: Router) {}
  ngOnInit(): void {
    this.service.getAllDoctors();
  }

  isFirstAppointment(choice: boolean) {
    this.firstAppointment = choice;
    if (choice) {
      const yesButton = document.getElementById("yesButton");
      yesButton?.classList.add("buttonSelected");
      const noButton = document.getElementById("noButton");
      noButton?.classList.remove("buttonSelected");
    }else{
      const noButton = document.getElementById("noButton");
      noButton?.classList.add("buttonSelected");
      const yesButton = document.getElementById("yesButton");
      yesButton?.classList.remove("buttonSelected");
    }
  }

  isADateSelected(choice: boolean, id:number) {
    console.log(choice.valueOf().toString() + id.toString());
    this.dateSelected = choice;
    if (choice) {
      this.findAvailableSlots(id);
    }
  }

  findAvailableSlots(_doctorId: number) {
    const doctorId = _doctorId;
    const date = this.appointmentData.date;
    const url = `/api/appointments/availableSlots?doctorId=${doctorId}&date=${date}`;
    this.http.get<string[]>(url)
      .subscribe((slots) => {this.availableSlots = slots
        if (slots.length > 0) {
          this.appointmentData.startTime = slots[0]; 
        }
      });
    
  }

  onSubmitNormalAppointment(form: any) {
    if (form.valid) {
      this.http.post('/api/appointments', this.appointmentData)
        .subscribe(() => alert('Cita registrada exitosamente.'));
    }
  }

  onSubmitFirstAppointment(form: any) {
    if (form.valid) {
      const appointment = {
        doctorId: this.appointmentData.doctorId,
        date: this.appointmentData.date,
        startTime: this.appointmentData.startTime,
        durationInHours: 1, // Duración fija de 1 hora
        patientName: this.appointmentData.patientName,
        patientPhone: this.appointmentData.patientPhone
      };

      this.http.post('/api/appointments',appointment)
        .subscribe((response: any) => {
          // Manejar el folio recibido del backend
          this.confirmationData = {
            folio: response, // Ajusta esto según la estructura de la respuesta
            patientName: appointment.patientName,
            date: appointment.date,
            startTime: appointment.startTime
          };

          console.log(response)

          // Mostrar el modal
          const modal = new Modal(document.getElementById('appointmentConfirmationModal')!);
          modal.show();
        }, (error) => {
          console.error('Error registrando la cita:', error);
          alert('Hubo un problema al registrar la cita.');
        });
    }
  }


  redirectToHomepage() {
    // Cierra el modal y redirige al homepage
    const modal = new Modal(document.getElementById('appointmentConfirmationModal')!);
    modal.hide();
    const backdrop = document.querySelector('.modal-backdrop');
    if (backdrop) {
      backdrop.remove();  // Elimina el fondo atenuado
    }
    this.router.navigate(['/']); // Ajusta el path del homepage según tu configuración
  }

}



