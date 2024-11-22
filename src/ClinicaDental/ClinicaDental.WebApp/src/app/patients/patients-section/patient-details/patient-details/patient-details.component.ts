import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-patient-details',
  templateUrl: './patient-details.component.html',
  styleUrls: ['./patient-details.component.css']
})
export class PatientDetailsComponent implements OnInit {
  patientId: string | null = null;
  patient: any = {}; 
  records: any[] = [];  
  constructor(
    private route: ActivatedRoute,
    private http: HttpClient
  ) {}

  ngOnInit(): void {
    this.patientId = this.route.snapshot.paramMap.get('id');
    console.log('ID del paciente recibido:', this.patientId);
    this.loadPatientDetails(this.patientId);
    this.getPatientRecords(this.patientId);
  }

  loadPatientDetails(patientId: string|null): void {
    if (patientId) {
      this.getPatientInformation(patientId).subscribe(
        (data) => {
          this.patient = data;
          console.log('Detalles del paciente:', this.patient);
        },
        (error) => {
          console.error('Error al obtener los detalles del paciente', error);
        }
      );
    }
  }

  getPatientInformation(patientId:string): Observable<any> {
    const url = `/api/patientsInformation/PatientById?patientId=1${patientId}`;
    return this.http.get<any>(url);
  }

  getRelatedPatientRecords(patientId:string): Observable<any> {
    const url = `/api/patientsInformation/PatientById?patientId=1${patientId}`;
    return this.http.get<any>(url);
  }
    
  // Obtener los registros relacionados con el paciente
  getPatientRecords(patientId: string | null): void {
    if (patientId) {
      this.http.get<any[]>(`/api/records?patientId=${patientId}`).subscribe(
        (data) => {
          this.records = data;
          console.log('Registros relacionados con el paciente:', this.records);
        },
        (error) => {
          console.error('Error al obtener los registros', error);
        }
      );
    }
  }
}
