import { Routes } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { PaymentDetailsComponent } from "./payment-details/payment-details.component";
import { LoginComponent } from "./login/login/login.component";
import { InventorySectionComponent } from "./inventory/inventory-section/inventory-section.component";

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
        path: "inventory-section",
        component: InventorySectionComponent,
        title: "Inventory"
    }
   
];

export default routeConfig;