import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit{
  doctorName!: String | null;
  
  ngOnInit(): void 
  {
    var doctorName = localStorage.getItem("doctorName");

    if(doctorName != null)
    {
      this.doctorName = doctorName;
    }
    this.getDoctorIdFromStorage();
  }

  getDoctorIdFromStorage(){
    const doctorId = localStorage.getItem('doctorID');
    if(doctorId != null){
      console.log(doctorId)
    }else{
      console.error('No se encontró el ID del doctor en localStorage');
    }
  }

}
