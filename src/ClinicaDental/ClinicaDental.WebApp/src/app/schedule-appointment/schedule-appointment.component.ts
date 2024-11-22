import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { Observable } from 'rxjs';
import { Appointment } from '../../../models/appointment.model';
import { AsyncPipe } from '@angular/common';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { routes } from '../app.routes';
import { Modal } from 'bootstrap';
import { Doctor } from '../../../models/doctor.model';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-schedule-appointment',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive, HttpClientModule, AsyncPipe, FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './schedule-appointment.component.html',
  styleUrls: ['./schedule-appointment.component.css']
})
export class AppointmentComponent implements OnInit{
  confirmationData: any = {};
  isPatientFirstAppointment: boolean | null = null;
  formVisibility: boolean = false;
  availableSlots: string[] = [];
  
  constructor(private http: HttpClient, private router: Router) {}
  ngOnInit(): void {
    this.getAllDoctors();
  }

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

  showAppointmentInfoForm(choice: boolean) {
    this.resetformVisibility();
    this.isPatientFirstAppointment = choice;
    if (choice) {
      this.styleButtonForYes();
    }else{
      this.styleButtonForNo();
    }
  }

  scheduleAppointment(form: any) {
    if (form.valid) {
      const appointment = {
        doctorId: this.appointmentData.doctorId,
        patientId: this.appointmentData.patientId,
        date: this.appointmentData.date,
        startTime: this.appointmentData.startTime,
        durationInHours: 1, // Duración fija de 1 hora
        patientName: this.appointmentData.patientName,
        patientPhone: this.appointmentData.patientPhone
      };

      console.log("pelanaaa"+ appointment.doctorId.toString())

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


          const confirmationAppointmentWindow = new Modal(document.getElementById('appointmentConfirmationModal')!);
          confirmationAppointmentWindow.show();
        }, (error) => {
          console.error('Error registrando la cita:', error);
          alert('Hubo un problema al registrar la cita.');
        });
    }
  }


  findAvailableSlots(doctorId:number) {
    console.log(doctorId.toString());
    this.getAvailableSlots(doctorId);
    this.continueScheduling();
  }

  getAvailableSlots(_doctorId: number) {
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

  continueScheduling(){
    this.formVisibility = true;
  }

  resetformVisibility(){
    this.formVisibility = false;
  }

  onSubmitNormalAppointment(form: any) {
    if (form.valid) {
      this.http.post('/api/appointments', this.appointmentData)
        .subscribe(() => alert('Cita registrada exitosamente.'));
    }
  }

  styleButtonForYes(){
    const yesButton = document.getElementById("yesButton");
    yesButton?.classList.add("buttonSelected");
    const noButton = document.getElementById("noButton");
    noButton?.classList.remove("buttonSelected");
  }

  styleButtonForNo(){
    const noButton = document.getElementById("noButton");
    noButton?.classList.add("buttonSelected");
    const yesButton = document.getElementById("yesButton");
    yesButton?.classList.remove("buttonSelected");
  }
  

  redirectToHomepage() {
    // Cierra el modal y redirige al homepage
    const modal = new Modal(document.getElementById('appointmentConfirmationModal')!);
    modal.dispose();
    const backdrop = document.querySelector('.modal-backdrop');
    if (backdrop) {
      backdrop.remove();  // Elimina el fondo atenuado
    }
    this.router.navigate(['/']);
    modal.dispose();
    const body = document.body;
    body.classList.remove('modal-open');
    body.style.overflow = '';
    body.style.paddingRight = '';
  }

  url: string=environment.apiBaseUrl+"/appointments";
  dentists: Doctor[]=[];
  formData: Appointment = new Appointment();

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



