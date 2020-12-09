import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { NbDialogService, NbToastrService } from '@nebular/theme';
import { map } from 'rxjs/operators';
import { ConfirmationPromptComponent } from 'src/app/theme/confirmation-prompt/confirmation-prompt.component';
import { UseCasesClient } from 'src/app/web-api-client';
import { WorkflowService } from 'src/app/_providers/workflow.service';
import { UsesCasesFormComponent } from '../uses-cases-form/uses-cases-form.component';
import { ActorsFilterComponent } from './actors/actors-filter/actors-filter.component';
import { UCItemActionsComponent } from './item-actions/item-actions.component';
import { UseCasesGridDataSource } from './use-cases-grid.datasource';

@Component({
  selector: 'app-use-cases-grid',
  templateUrl: './use-cases-grid.component.html',
  styleUrls: ['./use-cases-grid.component.scss']
})
export class UseCasesGridComponent implements OnInit {

  @Input()
  get workflowId(){
    return this.__workflowId;
  }
  set workflowId(val: string){
    //debugger;
    if(val===this.__workflowId){
      return;
    }
    this.__workflowId = val;
    this.refresh();
  }

  paging = {
    itemsCount: 0,
    pageSize: 5,
  }

  @Input()
  setupId: string;

  source: UseCasesGridDataSource;



  __workflowId: string;


  confirmDialogContext = {
    bodyText: "Are yousure you want to delete the <strong>use case</strong>? This action is <strong>irreversible.</strong>",
    headerText: "Confirm Use Case Deletion",
    cancelButtonStatus: 'info',
    confirmButtonStatus: 'danger',
  }

  settings = {
    actions: {
      add: false,
      delete: false,
      edit: false
    },
    columns: {
      id: {
        hide: true,
      },
      workflowId: {
        hide: true,
      },
      name: {
        title: 'Name',
      },
      description: {
        title: 'Description',
        editor: 'textarea'
      },
      actors: {
        title: 'Actors',
        filter: {
          type: 'custom',
          component: ActorsFilterComponent,
          onComponentInitFunction: (x) => {
            x.setupId = this.setupId;
          }
        },
        valuePrepareFunction: (x)=> {
          return x.map(s => s.name);
        }

      },
      preconditions: {
        title: 'Pre-conditions',
        hide: true,
      },
      postconditions: {
        title: 'Post-conditions',
        hide: true,
      },
      normalCourse:{
        title: 'Normal Course',
        editor: 'textarea',
        hide: true,
      },
      altCourse: {
        title: 'Alternative Course',
        editor: 'textarea',
        hide: true,
      },
      actions: {
        title: 'Actions',
        filter: false,
        sort: false,  
        type: 'custom',
        renderComponent: UCItemActionsComponent,
        onComponentInitFunction: (comp) =>{
          comp.setEditAuthority(true);
          comp.onClick.subscribe(x => {
            this.handleAction(x)
          })
        }
      },

    },
    pager: {
      hide: true,
      perPage: 5,
    }
  }

  constructor(private useCasesClient: UseCasesClient,private nbDialogService: NbDialogService,protected workflowService: WorkflowService, protected toast: NbToastrService) { 
    workflowService.selectedWorkflowId.subscribe(x => {
      if(x){
        if(x!==this.__workflowId){
          this.__workflowId = x;
          //this.source = new UseCasesGridDataSource(this.workflowId,this.useCasesClient);
          this.refresh();
        }

      }
      
    })
  }

  ngOnInit(): void {
    
  }



  refresh(){
    this.source = new UseCasesGridDataSource(this.workflowId,this.useCasesClient);
    this.source.paging.subscribe(x => {
      this.paging = Object.assign({},{itemsCount: x.itemsCount, pageSize: x.pageSize});
    });
    this.source.onChanged().subscribe(res => {
      if (res.action === 'filter') {
        this.source.setPaging(1, 5);
      }
    });

  }

  onSelect(event:any){

  }

  handleAction(event: any){
    switch(event.action){
      case 'edit':
        //console.log('Action: view/edit');
        this.nbDialogService.open(UsesCasesFormComponent,{context: 
          {mode: 'edit',
           element: {
             id: event.element.id,
             name: event.element.name,
             description: event.element.description,
             preconditions: event.element.preconditions,
             postconditions: event.element.postconditions,
             normalCourse: event.element.normalCourse,
             altCourse: event.element.altCourse,
             actors: event.element.actors.map(x =>({
               id: x.id,
               text: x.name
             }))
           }
        }}).onClose.subscribe(x => {
          this.refresh();
        })
        break;
      case 'delete':
        this.nbDialogService.open(ConfirmationPromptComponent,{context:this.confirmDialogContext}).onClose.subscribe(x => {
          if(x){
            this.source.remove(event.element).then(()=> {
              this.toast.success('Use Case removed successfully');
            }).catch(err => {
              this.toast.danger('Could not remove use case:'.concat(err));
            });
          }
        })
        break;
    }
  }

  handlePageChange(event){
    this.source.setPaging(event,this.paging.pageSize,true);
  }

}