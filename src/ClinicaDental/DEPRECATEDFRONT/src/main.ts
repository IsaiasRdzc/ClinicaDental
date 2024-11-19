import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';
import { provideRouter } from '@angular/router';
import routeConfig from './app/routes';

bootstrapApplication(AppComponent, {
  ...appConfig, // Combina la configuración existente
  providers: [
    ...appConfig.providers || [], // Mantén los providers existentes, si los hay
    provideRouter(routeConfig), // Agrega el router
  ]
}).catch((err) => console.error(err));

  //this is where every execution starts