import { AsyncPipe, CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { SupplyData } from '../../../../models/supply-data.model';
import { MedicalSupplyData } from '../../../../models/medical-supply-data.model';
import { SurgicalSupplyData } from '../../../../models/surgical-supply-data.model';
import { CleaningSupplyData } from '../../../../models/cleaning-supply-data.model';
@Component({
  selector: 'app-inventory-section',
  standalone: true,
  imports: [
    RouterOutlet,
    RouterLink,
    RouterLinkActive,
    HttpClientModule,
    AsyncPipe,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
  ],
  templateUrl: './inventory-section.component.html',
  styleUrls: ['./inventory-section.component.css'],
})
export class InventorySectionComponent implements OnInit {
  supplyType: string = '';

  constructor(private http: HttpClient) { }
  ngOnInit(): void {
    this.getSupplies();
    this.supplyType = 'All';
  }

  supplies: SupplyData[] = [];
  medicalSupplies: MedicalSupplyData[] = [];
  surgicalSupplies: SurgicalSupplyData[] = [];
  cleaningSupplies: CleaningSupplyData[] = [];

  getSupplies() {
    const url = '/api/supplies';
    this.http.get<SupplyData[]>(url).subscribe({
      next: (res) => {
        this.supplies = res.sort((a, b) => a.id - b.id);; // Mapeo directo a la lista
        console.log('Supplies:', this.supplies);
      },
      error: (err) => {
        console.error('Error fetching supplies:', err); // Referencia correcta al error
      },
    });
  }

  getMedicalSupplies(type: string) {
    const url = `/api/supplies/${type}`;
    console.log('URL:', url);
    this.http.get<MedicalSupplyData[]>(url).subscribe({
      next: (res) => {
        this.medicalSupplies = res.sort((a, b) => a.id - b.id);; // Mapeo directo a la lista
        console.log('Supplies:', this.supplies);
      },
      error: (err) => {
        console.error('Error fetching supplies:', err); // Referencia correcta al error
      },
    });
  }

  getSurgicalSupplies(type: string) {
    const url = `/api/supplies/${type}`;
    console.log('URL:', url);
    this.http.get<SurgicalSupplyData[]>(url).subscribe({
      next: (res) => {
        this.surgicalSupplies = res.sort((a, b) => a.id - b.id);; // Mapeo directo a la lista
        console.log('Supplies:', this.supplies);
      },
      error: (err) => {
        console.error('Error fetching supplies:', err); // Referencia correcta al error
      },
    });
  }

  getCleaningSupplies(type: string) {
    const url = `/api/supplies/${type}`;
    console.log('URL:', url);
    this.http.get<CleaningSupplyData[]>(url).subscribe({
      next: (res) => {
        this.cleaningSupplies = res.sort((a, b) => a.id - b.id);; // Mapeo directo a la lista
        console.log('Supplies:', this.supplies);
      },
      error: (err) => {
        console.error('Error fetching supplies:', err); // Referencia correcta al error
      },
    });
  }

  supplyTypeSelected(type: string) {

    switch (type) {
      case 'All':
        this.supplyType = type;
        this.getSupplies();
        break;
      case 'Medical':
        this.supplyType = type;
        this.getMedicalSupplies(type);
        break;
      case 'Surgical':
        this.supplyType = type;
        this.getSurgicalSupplies(type);
        break;
      case 'Cleaning':
        this.supplyType = type;
        this.getCleaningSupplies(type);
        break;
    }
  }
}
