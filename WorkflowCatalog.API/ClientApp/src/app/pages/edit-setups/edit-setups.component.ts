import { Component, Inject, OnInit } from '@angular/core';
import { EnumsClient, SetupsClient } from 'src/app/web-api-client';
import { SetupsDataSource } from './edit-setups.datasource';
import { ConfirmDeleteDialogComponent } from './confirm-delete-dialog/confirm-delete-dialog.component';
import { NbDialogService, NbToastrService } from '@nebular/theme';
import { AppComponent } from 'src/app/app.component';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-edit-setups',
  templateUrl: './edit-setups.component.html',
  styleUrls: ['./edit-setups.component.scss']
})
export class EditSetupsComponent implements OnInit {

  source: SetupsDataSource;
  setupStatusEnum;
  showDeleteAction: boolean = false;


  paging = {
    itemsCount: 0,
    pageSize: 5,
  }

  currentPage = 1;

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
    },
    delete: {
      deleteButtonContent: '<span class="material-icons">delete</span>',
      confirmDelete: true
    },
    add: {
      addButtonContent: '<span class="material-icons">add</span>',
      cancelButtonContent: '<span class="material-icons">cancel</span>',
      createButtonContent: '<span class="material-icons">check</span>',
    },
    columns: {
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
              { value: 0, title: 'Passive' },
              { value: 1, title: 'Active' }
            ]
          }
        },
        valuePrepareFunction: val => {
          return [{ value: 0, title: 'Passive' },
          { value: 1, title: 'Active' }].find(x => x.value === parseInt(val)).title;
        },
        editor: {
          type: 'list',
          config: {
            list: [
              { value: 0, title: 'Passive' },
              { value: 1, title: 'Active' }
            ]
          }
        }

      }
    },
    pager: {
      hide: true,
      perPage: 5,
    }

  };

  constructor(http: HttpClient, private setupsClient: SetupsClient, private enumsClient: EnumsClient,
    private dialogService: NbDialogService, @Inject(AppComponent) protected parent: AppComponent,protected toast: NbToastrService) {
    this.source = new SetupsDataSource(setupsClient, enumsClient);
  }

  ngOnInit(): void {
    this.source.paging.subscribe(res => this.paging = res);

    this.source.onChanged().subscribe(res => {
      this.parent.refresh();
      if (res.action === 'filter') {
        this.source.setPaging(1, 5);
      }

      if(res.action === 'update'){
        this.source.refresh();
        this.toast.success('Setup updated successfully');
      }
      if(res.action === 'add' || res.action === 'prepend'){
        this.source.refresh();
        this.toast.success('Setup added successfully');
      }
      if(res.action === 'remove'){
        this.toast.success('Setup removed successfully');
      }
    });
    /*this.source.onChanged().subscribe(change => {
       console.log(change);
        if (change.action === 'filter' && !isEmpty(change.elements)) {
                 let filters = change.filter.filters;
              
                 if (isEmpty(filters)) {
                   // There is no filters, it means filters were deleted, so dont do any  
                   return;
                 }
           //this.source.setPage(1,true);
                 // Do whatever you want with the filter event
         
               } 
     })*/
  }


  updateSettings() {
    this.settings.actions.delete = this.showDeleteAction;
    this.settings = Object.assign({}, this.settings);
  }

  confirmDelete(event: any) {
    const ref = this.dialogService.open(ConfirmDeleteDialogComponent, {
      context: {
        setupAbb: event.data.shortName,
        setupName: event.data.name,
      }, dialogClass: 'modal-half'
    });
    ref.onClose.subscribe(x => {
      if (ref.componentRef.instance.confirmed) {
        event.confirm.resolve();
        setTimeout(() => {
          this.parent.refresh();
        }, 200);
      } else {
        event.confirm.reject();
      }
    })
  }


  handlePageChange(event: any) {
    this.source.setPaging(event, this.paging.pageSize, true);
  }

}

