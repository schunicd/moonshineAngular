import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminPhotoGaleryComponent } from './admin-photo-galery.component';

describe('AdminPhotoGaleryComponent', () => {
  let component: AdminPhotoGaleryComponent;
  let fixture: ComponentFixture<AdminPhotoGaleryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminPhotoGaleryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminPhotoGaleryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
