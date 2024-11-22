import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';


@Component({
  selector: 'app-patient-profile-creation',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule],
  templateUrl: './patient-profile-creation.component.html',
  styleUrl: './patient-profile-creation.component.css'
})
export class PatientProfileCreationComponent implements OnInit{
  patientForm: FormGroup;
  doctorId: string|null = "";

  constructor(private fb: FormBuilder, private http: HttpClient,private router:Router  ) {
    this.patientForm = this.fb.group({
      patientNames: ['', [Validators.required]],
      patientSecondNames: ['', [Validators.required]],
      patientAge: [0, [Validators.required, Validators.min(0), Validators.max(100)]],
      patientDirection: ['', [Validators.required]],
      doctorId: [0],
      patientPhoneNumber: ['', [Validators.required, Validators.pattern(/^[0-9]+$/)]],
      patientEmail: ['', [Validators.email]],
    });
  }
  ngOnInit(): void {
    this.getDoctorIdFromStorage();
  }

  getDoctorIdFromStorage(){
    const doctorId = localStorage.getItem('DoctorID');
    if(doctorId != null){
      this.doctorId = doctorId;
      console.log(this.doctorId)
    }else{
      console.error('No se encontró el ID del doctor en localStorage');
    }
  }

  onSubmit() {
    if (this.patientForm.valid) {
      const patientData = this.patientForm.value;
      patientData.doctorId = this.doctorId;
      console.log(patientData);
      this.http.post('/api/patientsInformation/patient', patientData).subscribe({
        next: (response) =>{
          console.log("Exito",response);
          alert('Paciente guardado con exito');
          this.redirectToPatients();
        },
        error: (error) => console.error('Error al enviar datos:', error),
      });
    } else {
      alert('Por favor, completa todos los campos obligatorios.');
      this.markAllFieldsAsTouched();
      console.error('Formulario inválido');
    }
  }

  markAllFieldsAsTouched() {
    Object.keys(this.patientForm.controls).forEach((field) => {
      const control = this.patientForm.get(field);
      control?.markAsTouched();
    });
  }

  redirectToPatients(): void {
    this.router.navigate(['/patients-section']);
  }
}
