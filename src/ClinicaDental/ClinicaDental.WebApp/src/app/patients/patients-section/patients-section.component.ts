import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-patients-section',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './patients-section.component.html',
  styleUrl: './patients-section.component.css'
})

export class PatientsSectionComponent implements OnInit{
  router: any;

  constructor(private http: HttpClient) {}

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
    const doctorId = localStorage.getItem('doctorId');
    if(doctorId != null){
      this.doctorId = doctorId;
    }else{
      console.error('No se encontró el ID del doctor en localStorage');
    }
  }

  getPatientsByDoctorId(): Observable<any[]> {
    const url = `api/patientsInformation/PatientsByDoctorId?doctorId=${1}`; // Adjust the query parameter as per your API
    return this.http.get<any[]>(url);
  }

  loadPatients(): void {
    this.getPatientsByDoctorId().subscribe({
      next: (data) => {
        this.patients = data; // Almacena la lista de pacientes
      },
      error: (err) => {
        console.error('Error al obtener los pacientes:', err);
      }
    });
  }

  viewDetails(patientId: string): void {
    console.log('ID del paciente seleccionado:', patientId);
    // Ejemplo: Redirigir a otra página con el ID como parámetro
    this.router.navigate(['/patient-details', patientId]);
  }
}
