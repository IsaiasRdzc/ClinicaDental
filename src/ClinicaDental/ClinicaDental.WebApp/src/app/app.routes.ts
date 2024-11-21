import { Routes } from '@angular/router';
import { AppointmentComponent } from './schedule-appointment/schedule-appointment.component';
import { HomeComponent } from './home/home.component';
import { DashboardComponent } from './dashboard/dashboard.component';

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
    }
];
