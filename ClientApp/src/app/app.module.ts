import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { MatGridListModule } from '@angular/material';
import { MatStepperModule } from '@angular/material/stepper';
import { MatButtonModule } from '@angular/material/button';
import {MatExpansionModule} from '@angular/material/expansion';
import {MatTableModule} from '@angular/material/table';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ReservationsComponent } from './reservations/reservations.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatNativeDateModule } from '@angular/material/core';
import { AdminComponent } from './admin/admin.component';
import { AdminhomeComponent } from './adminhome/adminhome.component';
import { AdminCrudEventComponent } from './admin-crud-event/admin-crud-event.component';
import { AdminViewReservationsComponent } from './admin-view-reservations/admin-view-reservations.component';
import { AdminPhotoGaleryComponent } from './admin-photo-gallery/admin-photo-galery.component';
import { AdminSendEmailComponent } from './admin-send-email/admin-send-email.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { CalendarComponent } from './calendar/calendar.component';
import { ContactComponent } from './contact/contact.component';
import { MenuComponent } from './menu/menu.component';
import { PhotosComponent } from './photos/photos.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatDialogModule } from '@angular/material/dialog';
import { NgxPayPalModule } from 'ngx-paypal';
import { PaypalComponent } from './paypal/paypal.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ReservationsComponent,
    AdminComponent,
    AdminhomeComponent,
    AdminCrudEventComponent,
    AdminViewReservationsComponent,
    AdminPhotoGaleryComponent,
    AdminSendEmailComponent,
    AboutUsComponent,
    CalendarComponent,
    ContactComponent,
    MenuComponent,
    PhotosComponent,
    PaypalComponent
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
    MatSnackBarModule,
    MatDialogModule,
    NgxPayPalModule,
    MatExpansionModule,
    MatTableModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'reservations', component: ReservationsComponent },
      { path: 'admin', component: AdminComponent },
      { path: 'adminhome', component: AdminhomeComponent },
      { path: 'adminCrudEvent', component: AdminCrudEventComponent },
      { path: 'adminViewReservations', component: AdminViewReservationsComponent },
      { path: 'adminPhotoGallery', component: AdminPhotoGaleryComponent },
      { path: 'adminSendEmail', component: AdminSendEmailComponent },
      { path: 'aboutUs', component: AboutUsComponent },
      { path: 'calendar', component: CalendarComponent },
      { path: 'contact', component: ContactComponent },
      { path: 'menu', component: MenuComponent },
      { path: 'photos', component: PhotosComponent },
      { path: 'paypal', component: PaypalComponent },
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
