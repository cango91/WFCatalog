import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SetupsClient, SetupsDto, WorkflowsClient, WorkflowsDto } from '../../web-api-client';
import { LocalDataSource } from 'ng2-smart-table';
import { WorkflowsDatasource } from './setup.datasource';
import { WorkflowMenuitemComponent } from './workflow-menuitem/workflow-menuitem.component';

@Component({
  selector: 'ngx-setup',
  templateUrl: './setup.component.html',
  styleUrls: ['./setup.component.scss']
})
export class SetupComponent implements OnInit {

  id: number;
  setup: SetupsDto;

  workflowsSource: WorkflowsDatasource;

  selectedWorkflow: WorkflowsDto;

  workflowSettings = {
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
        valuePrepareFunction: (data) => this.workflowsSource.workflowTypes.find(s => s.value == data).name,
        filter: {
          type: 'list',
          config: {
            selectText: 'All',
            list: [
              { value: 0, title: 'MainFlow' },
              { value: 1, title: 'SubFlow' },
            ],
          },
        }
      },
      actions: {
        title: '',
        filter: false,
        type: 'custom',
        renderComponent: WorkflowMenuitemComponent,
        onComponentInitFunction: (comp) => {
          comp.setEditAuthority(true);
          
        }
      }
    }
  };

  constructor(protected activatedRoute: ActivatedRoute, protected setupClient: SetupsClient, protected workflowsClient: WorkflowsClient) {
    this.workflowsSource = new WorkflowsDatasource(this.workflowsClient);
  }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(data => {
      this.id = data.id;
      this.reload();
    })
  }

  onSelect({ data }) {
    this.selectedWorkflow = data;
  }

  reload() {
    this.selectedWorkflow = null;
    this.setupClient.getById(this.id).subscribe(res => {
      this.setup = res.setups[0];
    })
    this.workflowsSource.updateSetup(this.id);
  }

}
