import { Component, OnInit } from '@angular/core';
import { PurchaseService } from '../purchase.service';

@Component({
  selector: 'app-purchase-manager',
  templateUrl: './purchase-manager.component.html',
  styleUrls: ['./purchase-manager.component.css'],
})
export class PurchaseManagerComponent implements OnInit {
  purchases: any[] = []; // Lista de compras
  materials: any[] = []; // Lista dinámica de materiales

  constructor(private purchaseService: PurchaseService) { }

  ngOnInit(): void {
    this.loadAllPurchases();
  }

  // Cargar todas las compras
  loadAllPurchases(): void {
    this.purchaseService.getAllPurchases().subscribe(
      (data) => (this.purchases = data),
      (error) => console.error('Error al cargar las compras:', error)
    );
  }

  // Obtener detalles de una compra
  getPurchaseById(id: number): void {
    console.log(`Detalles de la compra con ID: ${id}`);
    // Lógica para mostrar los detalles
  }

  // Método para agregar un material a la lista
  addMaterial(): void {
    this.materials.push({
      materialId: '',
      materialName: '',
      quantity: '',
      unitCost: ''
    });
  }

  // Método para eliminar un material de la lista
  removeMaterial(index: number): void {
    this.materials.splice(index, 1);
  }

  // Método para agregar una nueva compra
  addNewPurchase(): void {
    const newPurchase = {
      id: 0,
      createdDate: (document.getElementById('createdDateInput') as HTMLInputElement).value,
      requiredDate: (document.getElementById('requiredDateInput') as HTMLInputElement).value,
      typeId: parseInt((document.getElementById('typeInput') as HTMLInputElement).value, 10),
      supplierId: parseInt((document.getElementById('supplierInput') as HTMLInputElement).value, 10),
      details: this.materials.map((material) => ({
        materialId: parseInt(material.materialId, 10),
        material: {
          name: material.materialName,
          price: parseFloat(material.unitCost)
        },
        quantity: parseInt(material.quantity, 10),
        unitCost: parseFloat(material.unitCost)
      }))
    };

    this.purchaseService.createPurchase(newPurchase).subscribe(
      (response) => {
        console.log('Compra creada exitosamente:', response);
        this.loadAllPurchases();
      },
      (error) => {
        console.error('Error al crear la compra:', error);
      }
    );
  }
}
