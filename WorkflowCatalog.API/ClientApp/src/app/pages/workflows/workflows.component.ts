import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LocalDataSource } from 'ng2-smart-table';
import { SetupsClient, SetupsDto, UseCasesClient, WorkflowsClient, WorkflowsDto } from 'src/app/web-api-client';
import { WorkflowItemMenuComponent } from './workflow-item-menu/workflow-item-menu.component';
import { WorkflowsDatasource } from './workflows-table/workflows.datasource';

@Component({
  selector: 'app-workflows',
  templateUrl: './workflows.component.html',
  styleUrls: ['./workflows.component.scss']
})
export class WorkflowsComponent implements OnInit {

  setupId: number;
  selectedWorkflow: WorkflowsDto;

  constructor(protected activeRouter: ActivatedRoute) {
    this.activeRouter.params.subscribe(res => {
      this.setupId = res.id;
    })
  }

  ngOnInit(): void {
  }

  onWorkflowSelect(workflow) {
    this.selectedWorkflow = workflow;
  }

}
