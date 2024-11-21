import { Routes } from '@angular/router';
import { ScheduleAppointmentComponent } from './schedule-appointment/schedule-appointment.component';
import { HomeComponent } from './home/home.component';
import { PurchaseListComponent } from './purchases/purchase-list/purchase-list.component';

export const routes: Routes = [
  { path: '', component: PurchaseListComponent },
  { path: '**', redirectTo: '' } // Redirección para rutas no válidas
];

export const appRoutes = [
  { path: 'purchases', component: PurchaseListComponent },
  // Otras rutas...
];
