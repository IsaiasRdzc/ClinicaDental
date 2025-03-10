import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-patients-section',
  standalone: true,
  imports: [CommonModule,RouterLink],
  templateUrl: './patients-section.component.html',
  styleUrls: ['./patients-section.component.css','../../dashboard/dashboard.component.css']
})

export class PatientsSectionComponent implements OnInit{

  constructor(private http: HttpClient, private router:Router) {}

  doctorId : string = ""; 
  patients: any[] = [];
  
  ngOnInit(): void {
    this.getDoctorIdFromStorage();

    if(this.doctorId){
      this.loadPatients();
    }

    this.loadPatients();

  }
  
  getDoctorIdFromStorage(){
    const doctorId = localStorage.getItem('doctorID');
    if(doctorId != null){
      this.doctorId = doctorId;
      console.log(this.doctorId)
    }else{
      console.error('No se encontró el ID del doctor en localStorage');
    }
  }

  getPatientsByDoctorId(): Observable<any[]> {
    const url = `api/patientsInformation/PatientsByDoctorId?doctorId=${this.doctorId}`; 
    return this.http.get<any[]>(url);
  }

  loadPatients(): void {
    this.getPatientsByDoctorId().subscribe({
      next: (data) => {
        this.patients = data; 
      },
      error: (err) => {
        console.error('Error al obtener los pacientes:', err);
      }
    });
  }

  redirectToPatientDetails(patientId: string): void {
    console.log('ID del paciente seleccionado:', patientId);
    this.router.navigate(['/patientDetails', patientId]);
  }

  redirectToNewRecord(patientId: string): void {
    console.log('ID del paciente seleccionado:', patientId);
    this.router.navigate(['/newMedicalRecord', patientId]);
  }

  redirectToNewPatient(): void {
    this.router.navigate(['/newPatient',this.doctorId]);
  }
}
