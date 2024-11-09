import { Time } from "@angular/common";

export interface Appointment {
    doctorId: number;
    date: Date;
    startTime: Time;
    duration: number;
    patientName: string;
    patientPhone: string;
    endTime: Time;
}

