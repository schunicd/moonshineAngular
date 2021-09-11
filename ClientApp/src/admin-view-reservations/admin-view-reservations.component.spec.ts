import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminViewReservationsComponent } from './admin-view-reservations.component';

describe('AdminViewReservationsComponent', () => {
  let component: AdminViewReservationsComponent;
  let fixture: ComponentFixture<AdminViewReservationsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminViewReservationsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminViewReservationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
