import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WorkflowsComponent } from './workflows/workflows.component';
import { ThemeModule } from 'src/theme/theme.module';
import { PagesComponent } from './pages.component';
import { PagesRoutingModule } from './pages-routing.module';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { WorkflowItemMenuComponent } from './workflows/workflow-item-menu/workflow-item-menu.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { WorkflowsTableComponent } from './workflows/workflows-table/workflows-table.component';
import { UsecasesTableComponent } from './workflows/usecases-table/usecases-table.component';
import { EditUseCaseComponent } from './edits/usecase/edit-use-case/edit-use-case.component';
import { EditWorkflowComponent } from './edits/workflow/edit-workflow/edit-workflow.component';
import { EditSetupComponent } from './edits/setup/edit-setup/edit-setup.component';
import { DeleteSetupComponent } from './edits/setup/delete-setup/delete-setup.component';



@NgModule({
  declarations: [WorkflowsComponent, PagesComponent, WorkflowItemMenuComponent, WorkflowsTableComponent, UsecasesTableComponent, EditUseCaseComponent, EditWorkflowComponent, EditSetupComponent, DeleteSetupComponent],
  imports: [
    CommonModule,
    ThemeModule,
    PagesRoutingModule,
    Ng2SmartTableModule,
    NgbModule
  ],
})
export class PagesModule { }
