import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { RouterLink } from '@angular/router';


@Component({
  selector: 'app-patient-details',
  standalone: true,
  imports: [CommonModule,RouterLink],
  templateUrl: './patient-details.component.html',
  styleUrls: ['./patient-details.component.css','../../../../dashboard/dashboard.component.css']
})
export class PatientDetailsComponent implements OnInit {
  patientId: string | null = null;
  patient: any = {}; 
  records: any[] = [];  
  constructor(
    private route: ActivatedRoute,
    private http: HttpClient,
    private router:Router
  ) {}

  ngOnInit(): void {
    this.patientId = this.route.snapshot.paramMap.get('patientId');
    console.log('ID del paciente recibido:', this.patientId);
    this.loadPatientDetails(this.patientId);
    this.loadPatientRecord(this.patientId);
  }
  
  loadPatientDetails(patientId: string|null): void {
    if(patientId){
      this.getPatientInformation(patientId).subscribe({
        next: (data) => {
          this.patient = data; 
          console.log('Detalles del paciente:', this.patient);
        },
        error: (err) => {
          console.error('Error al obtener los detalles del paciente', err);
        }
      });
    }
  }

  getPatientInformation(patientId:string|null): Observable<any> {
    const url = `/api/patientsInformation/PatientById?patientId=${patientId}`;
    return this.http.get<any>(url);
  }

  getPatientRecord(patientId:string|null): Observable<any> {
    const url = `/api/medicalRecords/RecordsByPatientId?patientId=${patientId}`;
    return this.http.get<any>(url);
  }
    
  loadPatientRecord(patientId: string|null): void {
    if(patientId){
      this.getPatientRecord(patientId).subscribe({
        next: (data) => {
          this.records = data; 
          console.log('Registros relacionados con el paciente:', this.patientId);
        },
        error: (err) => {
          console.error('Error al obtener los registros', err);
        }
      });
    }
  }

  redirectRecordToDetails(recordId: string): void {
    console.log('ID del registro seleccionado:', recordId);
    this.router.navigate(['/medicalRecordDetails', recordId]);
  }
}
