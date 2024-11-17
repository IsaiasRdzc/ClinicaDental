import { Time } from "@angular/common";

export class Appointment {
  id: number = 0;              
  doctorId: number = 0;        
  date: Date = new Date();     
  startTime: Time = { hours: 0, minutes: 0 }; 
  duration: number = 0;        
  patientName: string = '';    
  patientPhone: string = '';   
  endTime: Time = { hours: 0, minutes: 0 }; 
}

