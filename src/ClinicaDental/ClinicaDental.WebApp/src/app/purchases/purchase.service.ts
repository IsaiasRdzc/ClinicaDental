import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PurchaseService {
  private baseUrl = 'http://localhost:5347/api/purchases'; // Cambia al puerto de tu API si es necesario

  constructor(private http: HttpClient) { }

  // Crear una nueva compra
  createPurchase(purchase: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/newPurchase`, purchase);
  }

  // Obtener una compra por ID
  getPurchaseById(purchaseId: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/getPurchaseById/${purchaseId}`);
  }

  // Obtener todas las compras
  getAllPurchases(): Observable<any> {
    return this.http.get(`${this.baseUrl}/getAllPurchases`);
  }

  // Actualizar una compra
  updatePurchase(purchaseId: number, purchase: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/updatePurchase/${purchaseId}`, purchase);
  }

  // Eliminar una compra por ID
  deletePurchaseById(purchaseId: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/deletePurchaseById/${purchaseId}`);
  }
}
