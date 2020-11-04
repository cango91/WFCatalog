import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { NbDialogService, NbMenuService } from '@nebular/theme';
import { filter, map } from 'rxjs/operators';
import { UseCasesClient, WorkflowsDto } from '../../../web-api-client';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
import { EditUseCaseComponent } from './edit-use-case/edit-use-case.component';
import { UseCaseMenuitemComponent } from './use-case-menuitem/use-case-menuitem.component';
import { UseCasesDataSource } from './use-cases.datasource';

@Component({
  selector: 'ngx-use-cases',
  templateUrl: './use-cases.component.html',
  styleUrls: ['./use-cases.component.scss']
})
export class UseCasesComponent implements OnInit, OnChanges {

  editAuthority: boolean = true;

  @Input() workflow: WorkflowsDto;

  settings = {
    actions: {
      add: false,
      edit: false,
      delete: false
    },
    columns: {
      name: {
        title: 'UC Name'
      },
      description: {
        title: 'UC Description'
      },
      actors: {
        title: 'Actors'
      },
      actions: {
        title: '',
        type: 'custom',
        filter: false,
        renderComponent: UseCaseMenuitemComponent,
        onComponentInitFunction: (comp) => {
          comp.onClick.subscribe(({ id, action }) => {
            if (action === 'edit') {
              this.openEdit(id);
            }else if(action === 'delete'){
              this.confirmDelete(id);
            }
          });
          comp.setEditAuthority(this.editAuthority);
        }
      }
    }
  };

  source: UseCasesDataSource;

  constructor(protected usecasesClient: UseCasesClient, protected dialogService: NbDialogService) {
    this.source = new UseCasesDataSource(this.usecasesClient);

  }

  ngOnInit(): void {
    this.reload();
  }

  openEdit(id) {
    const ref = this.dialogService.open(EditUseCaseComponent, { context: { useCaseId: id , editAuthority: this.editAuthority} });
    ref.onClose.subscribe(res => {
      this.reload();
    })
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes.workflow && changes.workflow.currentValue && changes.workflow.previousValue && changes.workflow.currentValue.id !== changes.workflow.previousValue.id) {
      this.reload();
    }
  }

  reload() {
    this.source.updateWorkflow(this.workflow ? this.workflow.id : null);
  }

  confirmDelete(id){
    console.log('delete uc with id: '+id);
    const ref = this.dialogService.open(ConfirmDialogComponent, {context: {
      title: 'Confirm delete operation',
      content: 'Are you sure you want to delete this use case? This is an unrecoverable action.'
    }})
  }

  openNew(){
    const ref = this.dialogService.open(EditUseCaseComponent);
  }

}
