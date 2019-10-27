import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { CitizenComponent } from './citizen/citizen.component';
import { SecurityForceComponent } from './security-force/security-force.component';

import { ChartsModule } from 'ng4-charts/ng4-charts';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { library } from '@fortawesome/fontawesome-svg-core';
import { faFolder } from '@fortawesome/free-solid-svg-icons';
import { faVideo } from '@fortawesome/free-solid-svg-icons';
import { faImage } from '@fortawesome/free-solid-svg-icons';
import { faFileAudio } from '@fortawesome/free-solid-svg-icons';
import { faBars } from '@fortawesome/free-solid-svg-icons';
import { ServicesModule } from './services/services.module';

// Add an icon to the library for convenient access in other components
library.add(faFolder, faImage, faVideo, faFileAudio, faBars);


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    CitizenComponent,
    SecurityForceComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ChartsModule,
    NgxDatatableModule,
    FontAwesomeModule,
    ServicesModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'citizen', component: CitizenComponent },
      { path: 'security-force', component: SecurityForceComponent }
    ])
  ],
  providers: [ServicesModule],
  bootstrap: [AppComponent]
})
export class AppModule { }
