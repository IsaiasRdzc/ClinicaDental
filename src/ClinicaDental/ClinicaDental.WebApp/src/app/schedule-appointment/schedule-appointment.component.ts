import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router, RouterLink} from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Modal } from 'bootstrap';
import { Doctor } from '../../../models/doctor.model';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-schedule-appointment',
  standalone: true,
  imports: [RouterLink, FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './schedule-appointment.component.html',
  styleUrls: ['./schedule-appointment.component.css']
})

export class AppointmentComponent implements OnInit{
  url: string=environment.apiBaseUrl+"/HR";
  availableSlots: string[] = [];
  dentists: Doctor[]=[];
  isPatientFirstAppointment: boolean = true;
  formVisibility: boolean = false;

  confirmationData: any = {};
  appointmentData = {
    id: 0,
    doctorId: 1,
    patientId: 0,
    date: '',
    startTime: "",
    durationInHours: 1,
    patientName: '',
    patientPhone: ''
  };
  
  constructor(private http: HttpClient, private router: Router) {}
  
  ngOnInit(): void {
    this.getAllDoctors();
    this.setUpForm();
  }
  
  setUpForm() {
    this.hideCompleteForm();
    this.styleButtons();
  }

  scheduleAppointment(form: any) {
    if (form.valid) 
    {
      this.http.post('/api/appointments', this.appointmentData)
      .subscribe((response: any) => 
      {
        // Manejar el folio recibido del backend
        this.confirmationData = {
          folio: response,
          patientName: this.appointmentData.patientName,
          date: this.appointmentData.date,
          startTime: this.appointmentData.startTime
        };

        const confirmationAppointmentWindow = new Modal(document.getElementById('appointmentConfirmationModal')!);
        confirmationAppointmentWindow.show();
      }, 
      (error) => 
      {
        console.error('Error registrando la cita:', error);
        alert('Hubo un problema al registrar la cita.');
      });
    }
  }

  findAvailableSlots(doctorId:number) 
  {
    this.getAvailableSlots(doctorId);
    this.showCompleteForm();
  }

  getAvailableSlots(_doctorId: number) 
  {
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

  showCompleteForm(){
    this.formVisibility = true;
  }

  hideCompleteForm(){
    this.formVisibility = false;
  }

  patientIsOnFirstAppointment()
  {
    this.isPatientFirstAppointment = true;
    this.setUpForm();
  }
  
  patientIsNotOnFirstAppointment()
  {
    this.isPatientFirstAppointment = false;
    this.setUpForm();
  }

  styleButtons()
  {
    if(this.isPatientFirstAppointment)
    {
      const yesButton = document.getElementById("yesButton");
      yesButton?.classList.add("buttonSelected");
      const noButton = document.getElementById("noButton");
      noButton?.classList.remove("buttonSelected");
    }else
    {
      const noButton = document.getElementById("noButton");
      noButton?.classList.add("buttonSelected");
      const yesButton = document.getElementById("yesButton");
      yesButton?.classList.remove("buttonSelected");
    }
  }

  redirectToHomepage() {
    // Cierra el modal y redirige al homepage
    const modal = new Modal(document.getElementById('appointmentConfirmationModal')!);
    modal.dispose();
    const backdrop = document.querySelector('.modal-backdrop');
    if (backdrop) 
    {
      backdrop.remove();  // Elimina el fondo atenuado
    }
    this.router.navigate(['/']);
    modal.dispose();
    const body = document.body;
    body.classList.remove('modal-open');
    body.style.overflow = '';
    body.style.paddingRight = '';
  }

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



