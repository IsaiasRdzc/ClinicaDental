import { Routes } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { PaymentDetailsComponent } from "./payment-details/payment-details.component";
import { LoginComponent } from "./login/login.component";
import { DashboardComponent } from "./dashboard/dashboard.component";

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
    }
   
];

export default routeConfig;