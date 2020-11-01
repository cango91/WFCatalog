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



@NgModule({
  declarations: [WorkflowsComponent, PagesComponent, WorkflowItemMenuComponent, WorkflowsTableComponent, UsecasesTableComponent],
  imports: [
    CommonModule,
    ThemeModule,
    PagesRoutingModule,
    Ng2SmartTableModule,
    NgbModule
  ],
})
export class PagesModule { }
