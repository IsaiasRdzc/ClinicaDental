import { Component, OnInit } from '@angular/core';
import { PurchaseService } from '../purchase.service';

@Component({
  selector: 'app-purchase-list',
  templateUrl: './purchase-list.component.html',
  styleUrls: ['./purchase-list.component.css']
})
export class PurchaseListComponent implements OnInit {
  purchases: any[] = []; // Aquí se almacenarán las compras

  constructor(private purchaseService: PurchaseService) { }

  ngOnInit(): void {
    this.getPurchases();
  }

  getPurchases(): void {
    this.purchaseService.getAllPurchases().subscribe(
      (data) => {
        this.purchases = data;
      },
      (error) => {
        console.error('Error al cargar las compras:', error);
      }
    );
  }
}
