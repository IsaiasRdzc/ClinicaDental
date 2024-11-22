import { Routes } from "@angular/router";
import { Component } from "../../node_modules/@angular/core/index";
import { HomeComponent } from "./home/home.component";
import { PaymentDetailsComponent } from "./payment-details/payment-details.component";
import { LoginComponent } from "./login/login.component";
import { DashboardComponent } from "./dashboard/dashboard.component";
import { InventorySectionComponent } from "./inventory/inventory-section/inventory-section.component";
import { PatientsSectionComponent } from "./patients/patients-section/patients-section.component";


const routeConfig: Routes=[
    {
        path: "",
        component: HomeComponent,
        title: "Clinica Dental"
    },
    {
        path: "payment",
        component: PaymentDetailsComponent,
        title: "PaymentReynaldo"
  },

    {
        path: "login",
        component: LoginComponent,
        title: "Dentist Login"
    },
    {
        path: "dashboard",
        component: DashboardComponent,
        title: "Dashboard"
    },
    {
        path: "inventory-section",
        component: InventorySectionComponent,
        title: "Inventory"
    },
    {
        path: "patients-section",
        component: PatientsSectionComponent,
        title: "Pacientes"
    }

    
   
];

export default routeConfig;
