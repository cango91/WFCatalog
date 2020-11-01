import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ThemeModule } from 'src/theme/theme.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { API_BASE_URL } from './web-api-client';
import { AuthorizeInterceptor } from './authentication/authorize.intercepter';

@NgModule({
    declarations: [AppComponent,],
    imports: [BrowserModule, HttpClientModule, AppRoutingModule, ThemeModule, NgbModule],
    providers: [
        { provide: API_BASE_URL, useValue: '/' },
        { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
    ],
    bootstrap: [AppComponent]
})

export class AppModule { }