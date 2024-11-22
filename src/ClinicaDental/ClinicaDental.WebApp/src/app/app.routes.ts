import { Routes } from '@angular/router';
import { AppointmentComponent } from './schedule-appointment/schedule-appointment.component';
import { HomeComponent } from './home/home.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { PatientDetailsComponent } from './patients/patients-section/patient-details/patient-details/patient-details.component';
import { MedicalRecordDetailComponent } from './patients/patients-section/medical-record-detail/medical-record-detail/medical-record-detail.component';

export const routes: Routes = [
    { 
        path: '', 
        component: HomeComponent,
        title: 'Página Principal' // No se asigna componente aquí
    },
    {
        path: 'schedule-appointment',
        component: AppointmentComponent,
        title: 'Agendar Cita'
    },
    {
        path: 'dashboard',
        component: DashboardComponent,
        title: 'dashboard'
    },
    {
        path: 'patientDetails/:patientId',
        component: PatientDetailsComponent,
        title: 'Detalle Paciente'
    },
    {
        path: 'medicalRecordDetails/:recordId',
        component: MedicalRecordDetailComponent,
        title: 'Detalle Registro'
    }
];
