import { NgModule } from '@angular/core';
import { NbButtonModule, NbCardModule, NbContextMenuModule, NbDialogModule, NbInputModule, NbMenuModule, NbSelectModule } from '@nebular/theme';

import { ThemeModule } from '../@theme/theme.module';
import { PagesComponent } from './pages.component';
import { DashboardModule } from './dashboard/dashboard.module';
import { PagesRoutingModule } from './pages-routing.module';
import { SetupComponent } from './setup/setup.component';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { UseCasesComponent } from './setup/use-cases/use-cases.component';
import { UseCaseMenuitemComponent } from './setup/use-cases/use-case-menuitem/use-case-menuitem.component';
import { EditUseCaseComponent } from './setup/use-cases/edit-use-case/edit-use-case.component';
import { FormsModule } from '@angular/forms';
import { EditWorkflowComponent } from './setup/edit-workflow/edit-workflow.component';
import { WorkflowMenuitemComponent } from './setup/workflow-menuitem/workflow-menuitem.component';
import { ConfirmDialogComponent } from './setup/confirm-dialog/confirm-dialog.component';

@NgModule({
  imports: [
    PagesRoutingModule,
    ThemeModule,
    NbMenuModule,
    NbCardModule,
    Ng2SmartTableModule,
    DashboardModule,
    NbContextMenuModule,
    NbButtonModule,
    NbInputModule,
    NbSelectModule,
    FormsModule,
    NbDialogModule.forRoot()
  ],
  declarations: [
    PagesComponent,
    SetupComponent,
    UseCasesComponent,
    UseCaseMenuitemComponent,
    EditUseCaseComponent,
    EditWorkflowComponent,
    WorkflowMenuitemComponent,
    ConfirmDialogComponent,
  ],
})
export class PagesModule {
}
