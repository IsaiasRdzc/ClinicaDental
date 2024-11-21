import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PurchaseService {
  private apiUrl = 'http://localhost:5347/api/purchases'; 

  constructor(private http: HttpClient) { }

  getAllPurchases(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/getAllPurchases`);
  }

  getPurchaseById(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/getPurchaseById/${id}`);
  }

  createPurchase(purchase: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/newPurchase`, purchase);
  }

  updatePurchase(id: number, purchase: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/updatePurchase/${id}`, purchase);
  }

  deletePurchaseById(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/deletePurchaseById/${id}`);
  }
}
