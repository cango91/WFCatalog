import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { SideNavComponent } from './side-nav/side-nav.component';
import { NbButtonModule, NbCardModule, NbDialogModule, NbMenuModule, NbMenuService, NbContextMenuModule, NbIconModule, NbBadgeModule } from '@nebular/theme';
import { ConfirmationPromptComponent } from './confirmation-prompt/confirmation-prompt.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { OverlayNavComponent } from './overlay-nav/overlay-nav.component';



@NgModule({
  declarations: [HeaderComponent,FooterComponent, SideNavComponent, ConfirmationPromptComponent, OverlayNavComponent],
  imports: [
    CommonModule,
    NbMenuModule.forRoot(),
    NbCardModule,
    NbDialogModule,
    NgbModule,
    NbContextMenuModule,
    NbButtonModule,
  ],
  exports: [
    CommonModule, 
    HeaderComponent,
    FooterComponent, 
    SideNavComponent,
    NbButtonModule,
    ],
  providers: [NbMenuService]
})
export class ThemeModule { }
