import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-medical-record-detail',
  standalone: true,
  imports: [CommonModule,RouterLink],
  templateUrl: './medical-record-detail.component.html',
  styleUrls: ['./medical-record-detail.component.css','../../../../dashboard/dashboard.component.css']
})

export class MedicalRecordDetailComponent implements OnInit {
  recordId: string | null = null;
  medicalRecord: any = {}; 
  patient: any = {};

  constructor(
    private route: ActivatedRoute,
    private http: HttpClient,
  ) {}

  ngOnInit(): void {
    this.recordId = this.route.snapshot.paramMap.get('recordId');
    console.log('ID del registro recibido:', this.recordId);
    this.loadMedicalRecord(this.recordId);
  }

  getRecordDetails(recordId: string|null): Observable<any>{
    const url = `/api/medicalRecords/RecordById?medicalRecordId=${recordId}`;
    return this.http.get<any>(url);
  }

  loadMedicalRecord(recordId: string|null): void {
    if(recordId){
      this.getRecordDetails(recordId).subscribe({
        next: (data) => {
          this.medicalRecord = data; 
          this.loadPatientDetails(this.medicalRecord.patientId);
          console.log('Registros relacionados con:', this.recordId);
        },
        error: (err) => {
          console.error('Error al obtener los registros', err);
        }
      });
    }
  }

  loadPatientDetails(patientId: string|null): void {
    if(patientId){
      this.getPatientInformation(patientId).subscribe({
        next: (data) => {
          this.patient = data; 
          console.log('Detalles del paciente:', this.patient.patientName);
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
}
