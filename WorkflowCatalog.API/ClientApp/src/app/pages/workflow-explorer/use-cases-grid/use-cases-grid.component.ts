import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { NbDialogService } from '@nebular/theme';
import { UseCasesClient } from 'src/app/web-api-client';
import { ActorsFilterComponent } from './actors/actors-filter/actors-filter.component';
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
    bodyText: "Are yousure you want to delete the <strong>use case</strong> and all associated <strong>actors</strong>? This action is <strong>irreversible.</strong>",
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
        }

      },
      preconditions: {
        title: 'Pre-conditions',
        hide: true,
      },
      postcondtions: {
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

    }
  }

  constructor(private useCasesClient: UseCasesClient,private nbDialogService: NbDialogService) { }

  ngOnInit(): void {
    this.source = new UseCasesGridDataSource(this.workflowId,this.useCasesClient);
  }



  refresh(){
    this.source = new UseCasesGridDataSource(this.workflowId,this.useCasesClient);
  }

  onSelect(event:any){

  }

}