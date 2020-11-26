import { Component, EventEmitter, Inject, Input, OnChanges, OnInit, Output, SimpleChanges, ViewChild } from '@angular/core';
import { EnumsClient, WorkflowsClient } from 'src/app/web-api-client';
import { PaginatedQueryConfig } from 'src/app/_models/paginated-query-config.model';
import { ItemActionsComponent } from './item-actions/item-actions.component';
import { WokrflowsGridDataSource } from './workflows-grid.datasource';
import { isNil } from 'lodash';
import {  NbDialogService } from '@nebular/theme';
import { ConfirmationPromptComponent } from 'src/app/theme/confirmation-prompt/confirmation-prompt.component';
import { AppComponent } from 'src/app/app.component';
import { Ng2SmartTableComponent } from 'ng2-smart-table';

@Component({
  selector: 'app-workflows-grid',
  templateUrl: './workflows-grid.component.html',
  styleUrls: ['./workflows-grid.component.scss']
})
export class WorkflowsGridComponent implements OnInit, OnChanges {

  @Input()
  get setupId(): string { return this.__setupId; }
  set setupId(id:string) {
    this.__setupId = id;
    this.refresh();
  }

  @Output()
  onWorkflowSelected: EventEmitter<string> = new EventEmitter<string>();


  @ViewChild('table') table: Ng2SmartTableComponent;
  source: WokrflowsGridDataSource;
  
  pageSize: number = 5;

  queryConfig: PaginatedQueryConfig = {
    page: 1,
    pageSize: this.pageSize,
    sorts: null,
    filters: null,
  };

  

  private __setupId: string;

  settings = {
    actions: {
      add: false,
      edit: false,
      delete: false,
      position: 'right',
    },
    add: {
      addButtonContent: '<span class="material-icons">add</span>',
      cancelButtonContent: '<span class="material-icons">cancel</span>',
      createButtonContent: '<span class="material-icons">check</span>',
      confirmCreate: true,
    },
    columns:
      {
        id: {
          hide: true,
        },
        setupId: {
          hide: true,
        },
        name: {
          title: 'Name',
        },
        description: {
          title: 'Description',
          editor: 'textarea'
        },
        workflowType: {
          title: 'Type',
          defaultValue: 0,
          filter: {
            type: 'list',
            config: {
              selectText: 'All',
              list: [
                {value: 0, title: 'Main Flow'},
                {value: 1, title: 'Sub-flow'}
              ]
            }
          },
          editor: {
            type: 'list',
            config: {
              list: [
                {value: 0, title: 'Main Flow'},
                {value: 1, title: 'Sub-flow'}
              ]
            }
          },

           valuePrepareFunction: val => {
           return val ? this.source.workflowTypes.find(x => x.value === parseInt(val)).name : null;
            
          }
        },
         actions: {
          title: 'Actions',
          filter: false,
          sort: false,  
          type: 'custom',
          renderComponent: ItemActionsComponent,
          onComponentInitFunction: (comp) =>{
            comp.setEditAuthority(true);
            comp.onClick.subscribe(x => {
              this.handleAction(x)
            })
          }
        } 

      },
      mode: 'external',
       
  }

  confirmDialogContext = {
    bodyText: "Are yousure you want to delete the <strong>workflow</strong> and all associated <strong>use cases</strong>? This action is <strong>irreversible.</strong>",
    headerText: "Confirm Workflow Deletion",
    cancelButtonStatus: 'info',
    confirmButtonStatus: 'danger',
  }

  constructor(private workflowsClient:WorkflowsClient, private enumsClient: EnumsClient, private nbDialogService: NbDialogService, @Inject(AppComponent) protected parent:AppComponent) {
    this.source = new WokrflowsGridDataSource(this.setupId,workflowsClient, enumsClient, this.queryConfig);
   }
  ngOnChanges(changes: SimpleChanges): void {
    if(changes && changes.selectedWorkflowId && changes.selectedWorkflowId.previousValue && changes.selectedWorkflowId.currentValue && changes.selectedWorkflowId.currentValue !== changes.selectedWorkflowId.previousValue){

    }
  }

  refresh(query?: PaginatedQueryConfig){
    if(query){
      this.source = new WokrflowsGridDataSource(this.setupId,this.workflowsClient, this.enumsClient, query);
      return;
    }
    this.queryConfig = {
      page: 1,
      pageSize: this.pageSize,
      sorts: null,
      filters: null,
    };
    this.source = new WokrflowsGridDataSource(this.setupId,this.workflowsClient, this.enumsClient, this.queryConfig);
  }

  ngOnInit(): void {

  }

  handlePageChange(event: any){
    this.queryConfig.page = event;
    let sorts = this.source.getSort();
    let filters = this.source.getFilter();
    this.queryConfig.sorts = sorts;
    this.queryConfig.filters = filters;
    this.source = new WokrflowsGridDataSource(this.setupId,this.workflowsClient,this.enumsClient,this.queryConfig);

  }

  handleAction(event:any){
    switch(event.action){
      case 'edit':
        console.log('Action: view/edit');
        break;
      case 'delete':
        this.nbDialogService.open(ConfirmationPromptComponent, { context: this.confirmDialogContext }).onClose.subscribe(x => {
          if(x){
            this.source.remove(event.element);
            
            setTimeout( () => {
              let sorts = this.source.getSort();
              let filters = this.source.getFilter();
              this.queryConfig.sorts = sorts;
              this.queryConfig.filters = filters;
              this.source = new WokrflowsGridDataSource(this.setupId,this.workflowsClient,this.enumsClient,this.queryConfig);
              this.parent.refresh();
            },300);

          }
        });
        break;
    }
  }

  onSelect({data}){
    this.onWorkflowSelected.emit(data);
  }

}
