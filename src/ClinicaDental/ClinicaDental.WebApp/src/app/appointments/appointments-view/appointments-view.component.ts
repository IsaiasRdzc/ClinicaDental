import { CommonModule } from '@angular/common';
import { HttpClient} from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';


@Component({
  selector: 'app-appointments-view',
  standalone: true,

  imports: [ FormsModule, ReactiveFormsModule, CommonModule,RouterLink, RouterOutlet, RouterLinkActive],
  templateUrl: './appointments-view.component.html',
  styleUrls: ['./appointments-view.component.css','../../dashboard/dashboard.component.css'],
})
export class AppointmentsViewComponent implements OnInit {
  appointments: any[] = []; 
  dateStart: string = ''; 
  dateEnd: string = ''; 
  errorMessage: string = ''; 

  constructor(private http: HttpClient, private router: Router) {}
  ngOnInit() {
    const today = new Date();
    this.dateStart = today.toISOString().split('T')[0];
    this.dateEnd = new Date(today.setDate(today.getDate() + 7))
      .toISOString()
      .split('T')[0];
    this.fetchAppointments(); // Consulta inicial.
  }

  fetchAppointments() {
    if (!this.dateStart || !this.dateEnd) {
      this.errorMessage = 'Por favor, selecciona ambas fechas.';
      return;
    }

    const url = `/api/appointments?dateStart=${this.dateStart}&dateEnd=${this.dateEnd}`;
    console.log(url)
    this.http.get<any[]>(url).subscribe({
      next: (data) => {
        this.appointments = data;
        this.errorMessage = '';
      },
      error: (error) => {
        console.error('Error fetching appointments:', error);
        this.errorMessage =
          'Ocurrió un error al obtener las citas. Intenta nuevamente.';
      },
    });
  }

  saveNewPatient(doctorId:string){
      this.router.navigate(['/newPatient',doctorId]);
  }

  addNewRecordToPatient(patientId:string){
      console.log('ID del paciente seleccionado:', patientId);
      this.router.navigate(['/newMedicalRecord', patientId]);
  }
}
