import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InventorySectionComponent } from './inventory-section.component';

describe('InventorySectionComponent', () => {
  let component: InventorySectionComponent;
  let fixture: ComponentFixture<InventorySectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InventorySectionComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InventorySectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
