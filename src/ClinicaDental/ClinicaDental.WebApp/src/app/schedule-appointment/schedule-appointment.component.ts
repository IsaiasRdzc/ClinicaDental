import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { Observable } from 'rxjs';
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
  
  constructor(private http: HttpClient, private router: Router) {}
  ngOnInit(): void {
    this.cleanupModalState();
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


  isPatientFirstAppointment: boolean | null = null;
  showAppointmentInfoForm(choice: boolean) {
    this.resetformVisibility();
    this.isPatientFirstAppointment = choice;
    this.styleButtonDecision(this.isPatientFirstAppointment)
  }

  styleButtonDecision(choice: boolean){
    if (choice) {
      this.styleButtonForYes();
    }else{
      this.styleButtonForNo();
    }
  }

  scheduleAppointment(form: any): void {
    if (form.valid) {
      const appointment = this.createAppointmentData();
  
      this.saveAppointment(appointment).subscribe({
        next: (response) => this.showAppointmentConfirmationDetailsModal(response, appointment),
        error: (error) => this.handleError(error)
      });
    }
  }

  private createAppointmentData(): any {
    return {
      doctorId: this.appointmentData.doctorId,
      patientId: this.appointmentData.patientId,
      date: this.appointmentData.date,
      startTime: this.appointmentData.startTime,
      durationInHours: 1, // Todas las citas duran una hora
      patientName: this.appointmentData.patientName,
      patientPhone: this.appointmentData.patientPhone
    };
  }

  private saveAppointment(appointment: any): Observable<any> {
    return this.http.post('/api/appointments', appointment);
  }

  appointmentConfirmationDetails: any = {};
  private showAppointmentConfirmationDetailsModal(response: any, appointment: any): void {
    this.appointmentConfirmationDetails = {
      folio: response,
      patientName: appointment.patientName,
      date: appointment.date,
      startTime: appointment.startTime
    };
  
    console.log(response);
  
    const modalElement = document.getElementById('appointmentConfirmationModal');
    if (modalElement) {
      const confirmationModal = new Modal(modalElement);
      confirmationModal.show();
    }
  }

  private handleError(error: any): void {
    console.error('Error registrando la cita:', error);
    alert('Hubo un problema al registrar la cita.');
  }
  


  findAvailableSlots(doctorId:number) {
    this.getAvailableSlots(doctorId);
    this.continueScheduling();
  }

  availableSlots: string[] = [];
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

  canDisplayForm: boolean = false;
  continueScheduling(){
    this.canDisplayForm = true;
  }

  resetformVisibility(){
    this.canDisplayForm = false;
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
  

  redirectToHomepage() 
  {
    // Cierra el modal y redirige al homepage
    const modal = new Modal(document.getElementById('appointmentConfirmationModal')!);
    modal.dispose();
    const backdrop = document.querySelector('.modal-backdrop');
    if (backdrop) {
      backdrop.remove();  // Elimina el fondo atenuado
    }
    this.router.navigate(['/']);
  }

  
  dentists: Doctor[]=[];
  getAllDoctors(){
    this.http.get("/api/HR/doctor")
    .subscribe({
      next: res=>{
        this.dentists = res as Doctor[]; 
      },
      error: err=>{console.log(err)}
    })
  }

  private cleanupModalState(): void {
    document.body.classList.remove('modal-open');
    document.body.style.overflow = '';
    document.body.style.paddingRight = '';
    const backdrop = document.querySelector('.modal-backdrop');
    if (backdrop) {
      backdrop.remove();
    }
  }

}



