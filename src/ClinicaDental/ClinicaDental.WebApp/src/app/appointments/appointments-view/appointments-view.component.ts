import { AsyncPipe, CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-appointments-view',
  standalone: true,
  imports: [HttpClientModule, FormsModule, ReactiveFormsModule, CommonModule, RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './appointments-view.component.html',
  styleUrls: ['./appointments-view.component.css'],
})
export class AppointmentsViewComponent implements OnInit {
  appointments: any[] = []; // Aquí se almacenarán las citas obtenidas.
  dateStart: string = ''; // Fecha de inicio del rango (YYYY-MM-DD).
  dateEnd: string = ''; // Fecha de fin del rango (YYYY-MM-DD).
  errorMessage: string = ''; // Para manejar errores en la consulta.

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit() {
    // Opcional: Realiza una consulta inicial con un rango predefinido.
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

  Tyrone(folio: number) {
    console.log(`Acción Tyrone llamada para la cita con Folio: ${folio}`);
    // Aquí puedes implementar la funcionalidad adicional necesaria.
  }
}
