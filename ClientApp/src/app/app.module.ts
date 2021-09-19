import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { MatGridListModule } from '@angular/material';
import { MatStepperModule } from '@angular/material/stepper';
import { MatButtonModule } from '@angular/material/button';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ReservationsComponent } from './reservations/reservations.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatDatepickerModule } from '@angular/material/datepicker'
import { MatInputModule } from '@angular/material/input'
import { MatNativeDateModule } from '@angular/material/core';
import { AdminComponent } from './admin/admin.component';
import { AdminhomeComponent } from './adminhome/adminhome.component';
import { AdminCrudEventComponent } from './admin-crud-event/admin-crud-event.component';
import { AdminViewReservationsComponent } from 'src/admin-view-reservations/admin-view-reservations.component';
import { AdminPhotoGalleryComponent } from 'src/admin-photo-gallery/admin-photo-gallery.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { CalendarComponent } from './calendar/calendar.component';
import { ContactComponent } from './contact/contact.component';
import { MenuComponent } from './menu/menu.component';
import { PhotosComponent } from './photos/photos.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    ReservationsComponent,
    AdminComponent,
    AdminhomeComponent,
    AdminCrudEventComponent,
    AdminViewReservationsComponent,
    AdminPhotoGalleryComponent,
    AboutUsComponent,
    CalendarComponent,
    ContactComponent,
    MenuComponent,
    PhotosComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    MatIconModule,
    MatGridListModule,
    MatStepperModule,
    MatButtonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,

    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'reservations', component: ReservationsComponent },
      { path: 'admin', component: AdminComponent },
      { path: 'adminhome', component: AdminhomeComponent },
      { path: 'adminCrudEvent', component: AdminCrudEventComponent },
      { path: 'adminViewReservations', component: AdminViewReservationsComponent },
      { path: 'adminPhotoGallery', component: AdminPhotoGalleryComponent },
      { path: 'aboutUs', component: AboutUsComponent },
      { path: 'calendar', component: CalendarComponent },
      { path: 'contact', component: ContactComponent },
      { path: 'menu', component: MenuComponent },
      { path: 'photos', component: PhotosComponent },
      { path: '**', redirectTo: '', pathMatch: 'full' } /*DO NOT MOVE FROM END OF PATHS*/
    ]),
    NoopAnimationsModule,
    MatDatepickerModule,
    MatInputModule,
    MatNativeDateModule
  ],
  exports: [MatStepperModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
