import { TestBed } from '@angular/core/testing';

import { ScheduleAppointmentService } from './schedule-appointment.service';

describe('ScheduleAppointmentService', () => {
  let service: ScheduleAppointmentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ScheduleAppointmentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
