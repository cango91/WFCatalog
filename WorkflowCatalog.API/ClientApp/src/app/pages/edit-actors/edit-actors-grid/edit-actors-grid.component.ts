import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { NbDialogRef, NbDialogService } from '@nebular/theme';
import { NgbPagination, NgbPaginationConfig } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationPromptComponent } from 'src/app/theme/confirmation-prompt/confirmation-prompt.component';
import { ActorsClient } from 'src/app/web-api-client';
import { ActorsDataSource } from './edit-actors-grid.datasource';

@Component({
  selector: 'app-edit-actors-grid',
  templateUrl: './edit-actors-grid.component.html',
  styleUrls: ['./edit-actors-grid.component.scss']
})
export class EditActorsGridComponent implements OnInit {

  @Input()
  get setupId(): string { return this.__setupId; }
  set setupId(id: string) {
    this.__setupId = id;
    if (id) {
      //debugger;
      this.source = new ActorsDataSource(this.setupId, this.actorsClient);
      this.source.setPaging(1, 5, null);
      this.source.paging.subscribe(x => {
        this.paging = Object.assign({},{itemsCount: x.itemsCount, pageSize: x.pageSize});
      })
    }
  }

  paging = {
    itemsCount: 0,
    pageSize: 5,
  }

  private __setupId;

  source: ActorsDataSource;


  settings = {
    actions: {
      add: true,
      delete: true,
      edit: true,
      position: 'right',
    },
    pager: {
      hide: true,
      perPage: 5,
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


  constructor(protected actorsClient: ActorsClient, protected dialogService: NbDialogService) {
    this.source = new ActorsDataSource(this.setupId,this.actorsClient)
  }

  ngOnInit(): void {
    this.source.paging.subscribe(res => this.paging = res);
    //this.source.setPaging(1, 5, null);
    this.source.onChanged().subscribe(res => {
      if (res.action === 'filter') {
        this.source.setPaging(1, 5, true);
      }
    })
  }



  confirmDelete(event: any) {
    this.dialogService.open(ConfirmationPromptComponent, { context: this.confirmDialogContext }).onClose.subscribe(x => {
      if (x) {
        event.confirm.resolve()
        setTimeout(() => {
          event.confirm.resolve();
        }, 300);
      } else {
        event.confirm.reject();
      }
    })
  }



  handlePageChange(event: any) {
    this.source.setPaging(event, this.paging.pageSize, true);
  }

}