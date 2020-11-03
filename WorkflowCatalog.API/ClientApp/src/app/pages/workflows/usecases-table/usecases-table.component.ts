import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { UseCasesClient, WorkflowsDto } from 'src/app/web-api-client';
import { WorkflowItemMenuComponent } from '../workflow-item-menu/workflow-item-menu.component';
import { UseCasesDatasource } from './usecases.datasource';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { EditUseCaseComponent } from '../../edits/usecase/edit-use-case/edit-use-case.component';

@Component({
  selector: 'app-usecases-table',
  templateUrl: './usecases-table.component.html',
  styleUrls: ['./usecases-table.component.scss']
})
export class UsecasesTableComponent implements OnInit, OnChanges {

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
        renderComponent: WorkflowItemMenuComponent
      },
    },
    attr: {
      class: 'table table-bordered'
    }
  };

  source: UseCasesDatasource;

  constructor(protected usecasesClient: UseCasesClient, protected modalService: NgbModal) {
    this.source = new UseCasesDatasource(this.usecasesClient);
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes.workflow && changes.workflow.currentValue && changes.workflow.previousValue && changes.workflow.currentValue.id !== changes.workflow.previousValue.id) {
      this.reload();
    }
  }

  ngOnInit(): void {
    setTimeout(()=>{
      const modalRef = this.modalService.open(EditUseCaseComponent);
    },5000);
  }

  reload() {
    this.source.updateWorkflow(this.workflow.id);
  }

}
