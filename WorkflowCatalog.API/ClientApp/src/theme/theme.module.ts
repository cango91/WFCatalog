import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SidebarComponent } from './sidebar/sidebar.component';
import { HeaderComponent } from './header/header.component';

export const COMPONENTS = [
  SidebarComponent,
  HeaderComponent
]


@NgModule({
  declarations: [...COMPONENTS],
  imports: [
    CommonModule,
    NgbModule
  ],
  exports: [...COMPONENTS]
})
export class ThemeModule { }
