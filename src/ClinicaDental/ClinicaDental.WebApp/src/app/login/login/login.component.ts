import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive, HttpClientModule, AsyncPipe, FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginData = {
    username: '',
    password: ''
  };

  constructor(private http: HttpClient, private router: Router) {}

  onSubmit(loginForm: any) {
    if (loginForm.valid) {
      const loginPayload = {
        username: this.loginData.username,
        password: this.loginData.password
      };

      // Realiza el POST al backend
      this.http.post('/api/login', loginPayload).subscribe({
        next: (response: any) => {
          if (response.success) {
            // Si la respuesta es positiva, redirige a otra página
            this.router.navigate(['/payment']); // Ajusta la ruta de destino
          } else {
            // Manejo de error si el login es incorrecto
            alert('Credenciales incorrectas');
          }
        },
        error: (err) => {
          console.error('Error de autenticación:', err);
          alert('Hubo un problema con el inicio de sesión');
        }
      });
    }
  }
}
