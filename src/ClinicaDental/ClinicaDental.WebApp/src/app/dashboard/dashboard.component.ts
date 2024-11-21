import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { not } from 'rxjs/internal/util/not';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit{
  doctorName!: String | null;
  
  ngOnInit(): void {
    if(localStorage.getItem("DoctorName") != null){
      this.doctorName = localStorage.getItem("DoctorName");
    }
  }
}
