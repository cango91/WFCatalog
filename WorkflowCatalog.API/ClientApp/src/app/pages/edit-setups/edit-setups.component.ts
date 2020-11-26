import { Component, Inject, OnInit } from '@angular/core';
import { EnumsClient, SetupsClient } from 'src/app/web-api-client';
import { SetupsDataSource } from './edit-setups.datasource';
import { ConfirmDeleteDialogComponent } from './confirm-delete-dialog/confirm-delete-dialog.component';
import { NbDialogService } from '@nebular/theme';
import { AppComponent } from 'src/app/app.component';
import { PaginatedQueryConfig } from 'src/app/_models/paginated-query-config.model';

@Component({
  selector: 'app-edit-setups',
  templateUrl: './edit-setups.component.html',
  styleUrls: ['./edit-setups.component.scss']
})
export class EditSetupsComponent implements OnInit {

  source: SetupsDataSource
  setupStatusEnum;
  showDeleteAction: boolean = false;
  pageSize = 5;

  queryConfig = {
    page: 1,
    pageSize: this.pageSize,
    filters: null,
    sorts: null,
  }


  settings = {
    actions: {
      add: true,
      delete: false,
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
    columns : {
      id: {
        hide: true
      },
      name: {
        title: "Name"
      },
      shortName: {
        title: "Abbreviation"
      },
      description: {
        title: "Description",
        editor: {
          type: "textarea"
        }
      },
      workflowCount: {
        title: "# Workflows",
        editable: false,
        addable: false,
        filter: false,
        defaultValue: 0,
      },
      status: {
        title: "Status",
        defaultValue: 1,
        addable: false,
        filter: {
          type: 'list',
          config: {
            selectText: 'All',
            list: [
              {value: 0, title: 'Passive'},
              {value: 1, title: 'Active'}
            ]
          }
        },
        valuePrepareFunction: val => {
          return this.source.setupStatuses.find(x => x.value === parseInt(val)).name;
        },
        editor: {
          type: 'list',
          config: {
            list: [
              {value: 0, title: 'Passive'},
              {value: 1, title: 'Active'}
            ]
          }
        }

      }
    },

  };

  constructor(private setupsClient: SetupsClient, private enumsClient: EnumsClient,
    private dialogService: NbDialogService, @Inject(AppComponent) protected parent: AppComponent) { 
    this.source = new SetupsDataSource(setupsClient, enumsClient,this.queryConfig);
  }

  ngOnInit(): void {

  } 


  updateSettings(){
    this.settings.actions.delete = this.showDeleteAction;
    this.settings = Object.assign({},this.settings);
  }

  confirmDelete(event:any){
    const ref = this.dialogService.open(ConfirmDeleteDialogComponent,{
      context: {
        setupAbb: event.data.shortName,
        setupName: event.data.name,
      }, dialogClass: 'modal-half'});
      ref.onClose.subscribe(x => {
        if(ref.componentRef.instance.confirmed){
          //console.log('delete confirmed');
          event.confirm.resolve();
          setTimeout(() => {
            let sorts = this.source.getSort();
            let filters = this.source.getFilter();
    //console.log(filters);
            this.queryConfig.sorts = sorts;
            this.queryConfig.filters = filters;
            this.source = new SetupsDataSource(this.setupsClient, this.enumsClient,this.queryConfig);
            this.parent.refresh();
          },200);
          //this.source = new SetupsDataSource(this.setupsClient,this.enumsClient);
        }else{
          console.log('delete cancelled');
          event.confirm.reject();
        }
      })
  }

  confirmEdit(event:any){
    event.confirm.resolve();
    setTimeout(() => {
      let sorts = this.source.getSort();
    let filters = this.source.getFilter();
    //console.log(filters);
    this.queryConfig.sorts = sorts;
    this.queryConfig.filters = filters;      
      this.source = new SetupsDataSource(this.setupsClient, this.enumsClient,this.queryConfig);
      this.parent.refresh();
    },200);
  }

  confirmCreate(event:any){
    event.confirm.resolve();
     setTimeout(() => {
      let sorts = this.source.getSort();
      let filters = this.source.getFilter();
      //console.log(filters);
      this.queryConfig.sorts = sorts;
      this.queryConfig.filters = filters;
      this.source = new SetupsDataSource(this.setupsClient, this.enumsClient, this.queryConfig);
      this.parent.refresh();
    },200);
  }
 
  handlePageChange(event: any){
    this.queryConfig.page = event;
    let sorts = this.source.getSort();
    let filters = this.source.getFilter();
    this.queryConfig.sorts = sorts;
    this.queryConfig.filters = filters;
    this.source = new SetupsDataSource(this.setupsClient,this.enumsClient,this.queryConfig);
  }

}

