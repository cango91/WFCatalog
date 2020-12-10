import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NbThemeModule, NbLayoutModule, NbMenuModule, NbSidebarModule, NbSidebarService, NbThemeService } from '@nebular/theme';
import { NbEvaIconsModule } from '@nebular/eva-icons';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthorizeInterceptor } from './_services/authorize.interceptor';
import { ThemeModule } from './theme/theme.module';
import { NgProgressModule } from 'ngx-progressbar';
import { NgProgressHttpModule } from 'ngx-progressbar/http';
import { PagesModule } from './pages/pages.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SetupService } from './_providers/setup.service';
import { API_BASE_URL } from './web-api-client';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    NbThemeModule.forRoot({ name: 'corporate' }),
    NbLayoutModule,
    NbEvaIconsModule,
    ThemeModule,
    NgProgressModule.withConfig({
      spinnerPosition: 'right',
      color: '#f71cff'
    }),
    NgProgressHttpModule,
    HttpClientModule,
    NbSidebarModule.forRoot(),
    PagesModule,
    NbMenuModule.forRoot(),
    NgbModule,
    
  ],
  providers: [
    {provide: API_BASE_URL,useValue:'/workflow-api'},
    {provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true},
    NbSidebarService, NbThemeService, SetupService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
