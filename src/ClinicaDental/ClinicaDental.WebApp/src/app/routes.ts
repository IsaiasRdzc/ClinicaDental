import { Routes } from "@angular/router";
import { HomeComponent } from "./home/home.component";
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
