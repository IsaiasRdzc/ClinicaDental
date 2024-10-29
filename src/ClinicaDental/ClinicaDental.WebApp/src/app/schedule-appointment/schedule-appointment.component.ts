import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { Observable } from 'rxjs';
import { Appointment } from '../../../models/appointment.model';
import { AsyncPipe } from '@angular/common';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-schedule-appointment',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive, HttpClientModule, AsyncPipe, FormsModule, ReactiveFormsModule],
  templateUrl: './schedule-appointment.component.html',
  styleUrls: ['./schedule-appointment.component.css']
})
export class ScheduleAppointmentComponent implements OnInit {
  http = inject(HttpClient);

  appointmentsForm = new FormGroup({
    date: new FormControl<Date | null>(null),
    startTime: new FormControl<string | null>(null), // Cambiado a string
    duration: new FormControl<number | null>(null),
    patientName: new FormControl<string | null>(null),
    patientPhone: new FormControl<string | null>(null),
    endTime: new FormControl<string | null>({ value: null, disabled: true }) // Cambiado a string
  });

  ngOnInit() {
    this.appointmentsForm.get('startTime')?.valueChanges.subscribe(() => this.calculateEndTime());
    this.appointmentsForm.get('duration')?.valueChanges.subscribe(() => this.calculateEndTime());
  }

  private calculateEndTime() {
    const { startTime, duration } = this.appointmentsForm.value;
    if (startTime && duration) {
      // Parseamos `startTime` como un string en formato "HH:mm"
      const [hours, minutes] = startTime.split(':').map(Number);
      const start = new Date();
      start.setHours(hours, minutes, 0, 0); // Configuramos los segundos y milisegundos en 0
      const end = new Date(start.getTime() + duration * 60000); // Sumamos duración en minutos
      const endTimeString = `${end.getHours().toString().padStart(2, '0')}:${end.getMinutes().toString().padStart(2, '0')}`;
      this.appointmentsForm.patchValue({ endTime: endTimeString }, { emitEvent: false });
    }
  }

  onFormSubmit() {
    const scheduleAppointmentRequest = this.appointmentsForm.getRawValue();

    this.http.post("http://localhost:5347/api/appointments", scheduleAppointmentRequest)
      .subscribe({
        next: (value) => {
          console.log(value);
          this.appointmentsForm.reset();
        },
        error: (error) => {
          console.error("Error al agendar la cita:", error);
        }
      });
  }

  appointments$ = this.getAppointments();

  private getAppointments(): Observable<Appointment[]> {
    return this.http.get<Appointment[]>("http://localhost:5347/api/appointments");
  }
}
