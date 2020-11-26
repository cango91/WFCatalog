import { Component, Input, OnInit } from '@angular/core';
import { NbDialogRef, NbDialogService} from '@nebular/theme';
import { ConfirmationPromptComponent } from 'src/app/theme/confirmation-prompt/confirmation-prompt.component';
import { ActorsClient, UCActorDto } from 'src/app/web-api-client';
import { PaginatedQueryConfig } from 'src/app/_models/paginated-query-config.model';
import { ActorsDataSource } from './edit-actors-grid.datasource';

@Component({
  selector: 'app-edit-actors-grid',
  templateUrl: './edit-actors-grid.component.html',
  styleUrls: ['./edit-actors-grid.component.scss']
})
export class EditActorsGridComponent implements OnInit {

  @Input()
  get setupId(): string { return this.__setupId; }
  set setupId(id:string) {
    this.__setupId = id;
    this.refresh();
  }

  private __setupId;
  
  actors: UCActorDto[];
  source: ActorsDataSource;

  pageSize: number = 5;

  queryConfig = {
    page: 1,
    pageSize: this.pageSize,
    filters: null,
    sorts: null,
  }


  settings = {
    actions: {
      add: true,
      delete: true,
      edit: true,
      position: 'right',
    },
    edit: {
      editButtonContent: '<span class="material-icons">edit</span>',
      cancelButtonContent: '<span class="material-icons">cancel</span>',
      saveButtonContent: '<span class="material-icons">check</span>',
      confirmSave: true,
    },
    delete: {
      deleteButtonContent: '<span class="material-icons">delete</span>',
      confirmDelete: true
    },
    add: {
      addButtonContent: '<span class="material-icons">add</span>',
      cancelButtonContent: '<span class="material-icons">cancel</span>',
      createButtonContent: '<span class="material-icons">check</span>',
      confirmCreate: true,
    },
    columns: {
      id: {
        hide: true,
        editable: false,
      },
      setup: {
        hide: true,
        editable: false,
      },
      name: {
        title: "Name",
      },
      description: {
        title: "Description",
        editor: {
          type: "textarea",
        },
      },
    },
  };

  confirmDialogContext = {
    bodyText: "Are yousure you want to delete the actor? This action is <strong>irreversible.</strong>",
    headerText: "Confirm Actor Deletion",
    cancelButtonStatus: 'info',
    confirmButtonStatus: 'danger',
  }


  constructor(protected actorsClient:ActorsClient, protected dialogService: NbDialogService) {
    this.source = new ActorsDataSource(this.setupId,this.actorsClient,this.queryConfig)
   }

  ngOnInit(): void {

  }

  refresh(query?:PaginatedQueryConfig){
    if(query){
      this.source = new ActorsDataSource(this.setupId,this.actorsClient,query);
      return;
    }
    this.queryConfig = {
      page: 1,
      pageSize: this.pageSize,
      filters: null,
      sorts: null,
    }
    this.source = new ActorsDataSource(this.setupId,this.actorsClient,this.queryConfig);
  }

  confirmCreate(event: any){
    event.confirm.resolve();
    setTimeout(() => {
      let sorts = this.source.getSort();
      let filters = this.source.getFilter();
      this.queryConfig.sorts = sorts;
      this.queryConfig.filters = filters;
      this.refresh(this.queryConfig);
    },500);
  }

  confirmDelete(event: any){
    this.dialogService.open(ConfirmationPromptComponent,{context: this.confirmDialogContext}).onClose.subscribe(x => {
      if(x){
        event.confirm.resolve()
        setTimeout(() => {
          let sorts = this.source.getSort();
      let filters = this.source.getFilter();
      this.queryConfig.sorts = sorts;
      this.queryConfig.filters = filters;
      this.refresh(this.queryConfig);
        },300);
      }else{
        event.confirm.reject();
      }
    })
  }

  confirmSave(event:any){
    event.confirm.resolve();
    setTimeout(() => {
      let sorts = this.source.getSort();
      let filters = this.source.getFilter();
      this.queryConfig.sorts = sorts;
      this.queryConfig.filters = filters;
      this.refresh(this.queryConfig);
    },500);
  }

  handlePageChange(event: any){
    this.queryConfig.page = event;
    let sorts = this.source.getSort();
    let filters = this.source.getFilter();
    this.queryConfig.filters = filters;
    this.queryConfig.sorts = sorts;
    this.source = new ActorsDataSource(this.setupId,this.actorsClient,this.queryConfig);

  }

}