import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminPhotoGalleryComponent } from './admin-photo-gallery.component';

describe('AdminPhotoGalleryComponent', () => {
  let component: AdminPhotoGalleryComponent;
  let fixture: ComponentFixture<AdminPhotoGalleryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminPhotoGalleryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminPhotoGalleryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
