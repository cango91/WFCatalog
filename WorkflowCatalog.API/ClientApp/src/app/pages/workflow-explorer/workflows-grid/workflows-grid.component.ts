import { Component, EventEmitter, Inject, Input, OnChanges, OnInit, Output, SimpleChanges, ViewChild } from '@angular/core';
import { DiagramsClient, EnumsClient, WorkflowsClient } from 'src/app/web-api-client';
import { ItemActionsComponent } from './item-actions/item-actions.component';
import { WokrflowsGridDataSource } from './workflows-grid.datasource';
import { isNil } from 'lodash';
import {  NbDialogService } from '@nebular/theme';
import { ConfirmationPromptComponent } from 'src/app/theme/confirmation-prompt/confirmation-prompt.component';
import { AppComponent } from 'src/app/app.component';
import { Ng2SmartTableComponent } from 'ng2-smart-table';
import { SetupService } from 'src/app/_providers/setup.service';

@Component({
  selector: 'app-workflows-grid',
  templateUrl: './workflows-grid.component.html',
  styleUrls: ['./workflows-grid.component.scss']
})
export class WorkflowsGridComponent implements OnInit {

  @Input()
  get setupId(): string { return this.__setupId; }
  set setupId(id:string) {
    this.__setupId = id;
    if(id){
      this.refresh();
    }
    
  }

  @Output()
  onWorkflowSelected: EventEmitter<string> = new EventEmitter<string>();


  //@ViewChild('table') table: Ng2SmartTableComponent;
  source: WokrflowsGridDataSource;
  
  paging = {
    itemsCount: 0,
    pageSize: 5,
  }

  private __setupId: string;

  settings = {
    actions: {
      add: false,
      edit: false,
      delete: false,
      position: 'right',
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
        primaryDiagramId: {
          hide: true
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
             //console.log(val);
             return [
              {value: 0, title: 'Main Flow'},
              {value: 1, title: 'Sub-flow'}]
              .find(x => x.value === parseInt(val)).title;
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
        },

      },
      mode: 'external',
      pager: {
        hide: true,
        perPage: 5,
      },  
       
  }

  confirmDialogContext = {
    bodyText: "Are yousure you want to delete the <strong>workflow</strong> and all associated <strong>use cases</strong>? This action is <strong>irreversible.</strong>",
    headerText: "Confirm Workflow Deletion",
    cancelButtonStatus: 'info',
    confirmButtonStatus: 'danger',
  }

  constructor(
    private workflowsClient:WorkflowsClient, 
    private enumsClient: EnumsClient, 
    private nbDialogService: NbDialogService, 
    @Inject(AppComponent) protected parent:AppComponent, 
    private diagramService:DiagramsClient, 
    private currentSetupService: SetupService) {
      currentSetupService.currentSetupId.subscribe(x => this.setupId = x);
      this.source = new WokrflowsGridDataSource(this.setupId,workflowsClient, enumsClient);
   }

/*   ngOnChanges(changes: SimpleChanges): void {
    if(changes && changes.selectedWorkflowId && changes.selectedWorkflowId.previousValue && changes.selectedWorkflowId.currentValue && changes.selectedWorkflowId.currentValue !== changes.selectedWorkflowId.previousValue){

    }
  } */

  refresh(){
    this.source = new WokrflowsGridDataSource(this.setupId,this.workflowsClient, this.enumsClient);
    //this.source.setPaging(1,this.paging.pageSize,true);
    this.source.paging.subscribe(x => {
      this.paging = Object.assign({},{itemsCount: x.itemsCount, pageSize: x.pageSize});
    })
    this.parent.refresh();
    this.source.onChanged().subscribe(res => {
      this.parent.refresh();
      if (res.action === 'filter') {
        this.source.setPaging(1, 5);
      }
    });
  }

  ngOnInit(): void {
    //this.source.paging.subscribe(res => this.paging = res);

    /* this.source.onChanged().subscribe(res => {
      this.parent.refresh();
      if (res.action === 'filter') {
        this.source.setPaging(1, 5);
      }
    }); */
  }

  handlePageChange(event: any){
    this.source.setPaging(event, this.paging.pageSize, true);
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
              this.parent.refresh();
            },300);

          }
        });
        break;
      case 'diagram':
        this.diagramService.getDiagramById(event.element.primaryDiagramId).subscribe((res)=>{
          var a = document.createElement("a");
          a.href = URL.createObjectURL(res.data);
          a.download = res.fileName;
          a.click();
          a.remove();
        });
        break;
    }
  }

  onSelect({data}){
    this.onWorkflowSelected.emit(data);
  }

}
