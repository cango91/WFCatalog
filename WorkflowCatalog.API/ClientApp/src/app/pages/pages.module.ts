import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PagesComponent } from './pages.component';
import { WorkflowExplorerComponent } from './workflow-explorer/workflow-explorer.component';
import { FunctionExplorerComponent } from './function-explorer/function-explorer.component';
import { PagesRoutingModule } from './pages-routing.module';
import { NbButtonModule, NbCardModule, NbCheckboxModule, NbContextMenuModule, NbDialogModule, NbInputModule, NbLayoutModule, NbMenuModule, NbPopoverModule, NbSelectModule, NbToastrModule, NbToggleModule, NbTooltipModule } from '@nebular/theme';
import { RouterModule } from '@angular/router';
import { EditSetupsComponent } from './edit-setups/edit-setups.component';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { ConfirmDeleteDialogComponent } from './edit-setups/confirm-delete-dialog/confirm-delete-dialog.component';
import { NgbModule, NgbPopoverModule } from '@ng-bootstrap/ng-bootstrap';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EditActorsComponent } from './edit-actors/edit-actors.component';
import { EditActorsGridComponent } from './edit-actors/edit-actors-grid/edit-actors-grid.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { WorkflowsGridComponent } from './workflow-explorer/workflows-grid/workflows-grid.component';
import { ItemActionsComponent } from './workflow-explorer/workflows-grid/item-actions/item-actions.component';
import { UseCasesGridComponent } from './workflow-explorer/use-cases-grid/use-cases-grid.component';
import { ActorsFilterComponent } from './workflow-explorer/use-cases-grid/actors/actors-filter/actors-filter.component';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { SetupService } from '../_providers/setup.service';
import { WorkflowFormComponent } from './workflow-explorer/workflow-form/workflow-form.component';
import { UsesCasesFormComponent } from './workflow-explorer/uses-cases-form/uses-cases-form.component';
import { WorkflowService } from '../_providers/workflow.service';
import { UCItemActionsComponent } from './workflow-explorer/use-cases-grid/item-actions/item-actions.component';
import { DiagramComponent } from './workflow-explorer/workflow-form/diagram/diagram.component';

@NgModule({
  declarations: [
    WorkflowExplorerComponent, 
    FunctionExplorerComponent,
    PagesComponent,
    EditSetupsComponent,
    ConfirmDeleteDialogComponent,
    EditActorsComponent,
    EditActorsGridComponent,
    WorkflowsGridComponent,
    ItemActionsComponent,
    UseCasesGridComponent, 
    ActorsFilterComponent, 
    WorkflowFormComponent, 
    UsesCasesFormComponent,
    UCItemActionsComponent,
    DiagramComponent,
    
  ],
  imports: [
    CommonModule,
    PagesRoutingModule,
    NbCardModule,
    NbLayoutModule,
    RouterModule,
    NbInputModule,
    Ng2SmartTableModule,
    NbToggleModule,
    NbToastrModule.forRoot(),
    NbDialogModule.forRoot(),
    NgbModule,
    NbButtonModule,
    MatIconModule,
    FormsModule,
    ReactiveFormsModule,
    NbSelectModule,
    NgbPopoverModule,
    MatPaginatorModule, 
    NbContextMenuModule,
    NbMenuModule.forRoot(),
    NgMultiSelectDropDownModule,
    NbCheckboxModule,
    NbTooltipModule,
  ],
  exports: [PagesComponent],
  providers: [WorkflowService]
})
export class PagesModule { }
