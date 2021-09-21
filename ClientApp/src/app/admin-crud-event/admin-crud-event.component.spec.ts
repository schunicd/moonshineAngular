import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { CreateEventDialog } from './admin-crud-event.component';
import { AdminCrudEventComponent } from './admin-crud-event.component';
import { DeleteEventDialog } from './admin-crud-event.component';
import { EditEventDialog } from './admin-crud-event.component'

describe('AdminCrudEventComponent', () => {
  let component: AdminCrudEventComponent;
  let fixture: ComponentFixture<AdminCrudEventComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminCrudEventComponent, CreateEventDialog, DeleteEventDialog, EditEventDialog ]
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
