import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';


@Component({
  selector: 'app-medical-record-creation',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule],
  templateUrl: './medical-record-creation.component.html',
  styleUrl: './medical-record-creation.component.css'
})
export class MedicalRecordCreationComponent implements OnInit{
  medicalRecordForm: FormGroup;
  patientId: string|null = "";

  constructor(private fb: FormBuilder,private route: ActivatedRoute,private http: HttpClient,private router:Router ) {
    this.medicalRecordForm = this.fb.group({
      doctorId: [0, Validators.required],
      patientId: [0],
      dateCreated: [this.getTimeInISOForGTM6()],
      diagnosis: this.fb.array([]),
      teeths: this.fb.array([]),
      medicalProcedures: this.fb.array([]),
      coment: ['']
    });
  }

  ngOnInit(): void {
    this.patientId = this.route.snapshot.paramMap.get('patientId');
    console.log('ID del paciente recibido:', this.patientId);
  }

  get diagnosis(): FormArray {
    return this.medicalRecordForm.get('diagnosis') as FormArray;
  }

  get teeths(): FormArray {
    return this.medicalRecordForm.get('teeths') as FormArray;
  }

  get medicalProcedures(): FormArray {
    return this.medicalRecordForm.get('medicalProcedures') as FormArray;
  }

  getTimeInISOForGTM6(){
    const currentDate = new Date();
    const gmtMinus6Date = new Date(currentDate.getTime() - 6 * 60 * 60 * 1000);
    return gmtMinus6Date.toISOString();
  }

  addDiagnosis(): void {
    this.diagnosis.push(
      this.fb.group({
        name: ['', Validators.required],
        description: ['', Validators.required],
        treatments: this.fb.array([])
      })
    );
  }
  

  addTreatment(diagnosisIndex: number): void {
    const diagnosisControl = this.diagnosis.at(diagnosisIndex);
    if (diagnosisControl && diagnosisControl.get('treatments')) {
      const treatments = diagnosisControl.get('treatments') as FormArray;
      treatments.push(
        this.fb.group({
          description: ['', Validators.required],
          dose: ['', Validators.required]
        })
      );
    }
  }

  getTreatments(diagnosisIndex: number): FormArray {
    const control = this.diagnosis.at(diagnosisIndex).get('treatments');
    return control ? (control as FormArray) : this.fb.array([]);
  }  

  addTeeth(): void {
    this.teeths.push(
      this.fb.group({
        name: ['', Validators.required],
        description: ['', Validators.required],
        isSuperNumerary: [false, Validators.required],
        medicalRecordId: [0, Validators.required]
      })
    );
  }

  addMedicalProcedure(): void {
    this.medicalProcedures.push(
      this.fb.group({
        name: ['', Validators.required],
        description: ['', Validators.required],
        procedureCost: [0, Validators.required, Validators.min(0)]
      })
    );
  }

  submitForm(): void {
    if (this.medicalRecordForm.valid) {
      const formData = this.medicalRecordForm.value;
      console.log(formData)
      formData.patientId = this.patientId;

      this.http.post("/api/medicalRecords/MedicalRecord", formData).subscribe({
        next: (response) => {
          console.log('Formulario enviado con éxito', response);
          alert('El registro médico fue creado correctamente.');
          this.redirectToPatients();
        },
        error: (error) => {
          console.error('Error al enviar el formulario', error);
          alert('Ocurrió un error al crear el registro médico.');
        }
      });
    } else {
      alert('Por favor, completa todos los campos obligatorios.');
    }
  }

  redirectToPatients(): void {
    this.router.navigate(['/patients-section']);
  }
}


export interface MedicalRecord {
  doctorId: number;
  patientId: number;
  dateCreated: string;
  diagnosis: {
    name: string;
    description: string;
    treatments: {
      description: string;
      dose: string;
    }[];
  }[];
  teeths: {
    name: string;
    description: string;
    isSuperNumerary: boolean;
    medicalRecordId: number;
  }[];
  medicalProcedures: {
    name: string;
    description: string;
    procedureCost: number;
  }[];
  coment: string;
}

