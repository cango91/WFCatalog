import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PagesComponent } from './pages.component';
import { WorkflowExplorerComponent } from './workflow-explorer/workflow-explorer.component';
import { FunctionExplorerComponent } from './function-explorer/function-explorer.component';
import { PagesRoutingModule } from './pages-routing.module';
import { NbButtonModule, NbCardModule, NbContextMenuModule, NbDialogModule, NbInputModule, NbLayoutModule, NbMenuModule, NbMenuService, NbPopoverModule, NbSelectModule, NbToastrModule, NbToggleModule } from '@nebular/theme';
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
import { WorkflowFormComponent } from './workflow-form/workflow-form.component';

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
  ],
  exports: [PagesComponent],
  providers: [SetupService]
})
export class PagesModule { }
