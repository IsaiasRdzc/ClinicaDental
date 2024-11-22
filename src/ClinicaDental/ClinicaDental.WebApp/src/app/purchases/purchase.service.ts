import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PurchaseService {
  private apiUrl = 'http://localhost:5347/api/purchases'; // Cambia el puerto según tu configuración

  constructor(private http: HttpClient) { }

  getAllPurchases(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/getAllPurchases`);
  }

  // Crear una nueva compra
  createPurchase(purchase: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/newPurchase`, purchase);
  }
}
