import { Routes } from "@angular/router";
import { Component } from "../../node_modules/@angular/core/index";
import { HomeComponent } from "./home/home.component";
import { PaymentDetailsComponent } from "./payment-details/payment-details.component";
import { apppurchaselist } from "./purchases/purchase-list/purchase-list.component.ts";

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
      path: "purchases",
      component: apppurchaselist,
      title: "TestPurchases"
    }
];

export default routeConfig;
