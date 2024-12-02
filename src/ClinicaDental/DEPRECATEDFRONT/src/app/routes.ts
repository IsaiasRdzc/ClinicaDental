import { Routes } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { PaymentDetailsComponent } from "./payment-details/payment-details.component";

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
    }
];

export default routeConfig;