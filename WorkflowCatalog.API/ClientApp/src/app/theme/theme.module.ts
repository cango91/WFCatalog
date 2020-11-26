import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { SideNavComponent } from './side-nav/side-nav.component';
import { NbButtonModule, NbCardModule, NbDialogModule, NbMenuModule, NbMenuService } from '@nebular/theme';
import { ConfirmationPromptComponent } from './confirmation-prompt/confirmation-prompt.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';



@NgModule({
  declarations: [HeaderComponent,FooterComponent, SideNavComponent, ConfirmationPromptComponent],
  imports: [
    CommonModule,
    NbMenuModule.forRoot(),
    NbCardModule,
    NbDialogModule,
    NgbModule,
    NbButtonModule,
    
  ],
  exports: [
    CommonModule, 
    HeaderComponent,
    FooterComponent, 
    SideNavComponent,
    ConfirmationPromptComponent,
    ],
  providers: [NbMenuService]
})
export class ThemeModule { }
