import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminCrudEventComponent } from './admin-crud-event.component';

describe('AdminCrudEventComponent', () => {
  let component: AdminCrudEventComponent;
  let fixture: ComponentFixture<AdminCrudEventComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminCrudEventComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminCrudEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
