import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { SetupsClient, SetupsDto, WorkflowsClient, WorkflowsDto } from 'src/app/web-api-client';
import { WorkflowItemMenuComponent } from '../workflow-item-menu/workflow-item-menu.component';
import { WorkflowsDatasource } from './workflows.datasource';

@Component({
  selector: 'app-workflows-table',
  templateUrl: './workflows-table.component.html',
  styleUrls: ['./workflows-table.component.scss']
})
export class WorkflowsTableComponent implements OnInit, OnChanges {

  @Input() setupId: number;

  @Output() onWorkflowSelect: EventEmitter<WorkflowsDto> = new EventEmitter();

  setup: SetupsDto;

  settings = {
    actions: {
      add: false,
      edit: false,
      delete: false
    },
    columns: {
      name: {
        title: 'WF Name'
      },
      description: {
        title: 'WF Description'
      },
      type: {
        title: 'WF Type',
        valuePrepareFunction: (data) => this.source.workflowTypes.find(s => s.value == data).name,
        filter: {
          type: 'list',
          config: {
            selectText: 'Select...',
            list: [
              { value: 0, title: 'MainFlow' },
              { value: 1, title: 'SubFlow' },
            ],
          },
        }
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

  source: WorkflowsDatasource;

  constructor(protected workflowsClient: WorkflowsClient, protected setupsClient: SetupsClient) {
    this.source = new WorkflowsDatasource(this.workflowsClient);
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes.setupId && changes.setupId.currentValue !== changes.setupId.previousValue) {
      this.reload();
    }
  }

  reload() {
    this.setupsClient.getById(this.setupId).subscribe(res => {
      this.setup = res.setups.length > 0 ? res.setups[0] : null;
    })

    this.source.updateSetup(this.setupId);
  }

  selectRow({ data }) {
    this.onWorkflowSelect.emit(data);
  }

  ngOnInit(): void {
  }

}
