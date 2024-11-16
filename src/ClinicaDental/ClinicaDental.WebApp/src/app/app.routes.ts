import { Routes } from '@angular/router';
import { ScheduleAppointmentComponent } from './schedule-appointment/schedule-appointment.component';
import { HomeComponent } from './home/home.component';

export const routes: Routes = [
    { 
        path: '', 
        component: HomeComponent,
        title: 'Página Principal' // No se asigna componente aquí
    },
    {
        path: 'schedule-appointment',
        component: ScheduleAppointmentComponent,
        title: 'Agendar Cita'
    }
];
