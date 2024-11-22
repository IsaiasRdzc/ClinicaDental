import { HttpClient} from '@angular/common/http';
import { Component} from '@angular/core';
import { Router, RouterLink} from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterLink, FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})

export class LoginComponent 
{
  loginData = {
    username: '',
    password: ''
  };

  constructor(private http: HttpClient, private router: Router) {}

  onSubmit(loginForm: any) 
  {
    if (!loginForm.valid) 
    {
      return;
    }

    // Realiza el POST al backend
    this.http.post('/api/login', this.loginData).subscribe({
      next: (response: any) => 
      {
        localStorage.setItem('doctorID', response.id);
        localStorage.setItem('doctorName', response.name);
        this.router.navigate(['/dashboard']);
      },
      error: () => 
      {
        alert('Credenciales incorrectas');
      }
    });
  }
}
