import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NbDialogService } from '@nebular/theme';
import { AppComponent } from 'src/app/app.component';
import { SetupsClient, SingleSetupDto, SingleWorkflowDto } from 'src/app/web-api-client';
import { SetupService } from 'src/app/_providers/setup.service';
import { WorkflowFormComponent } from '../workflow-form/workflow-form.component';
import { UseCasesGridComponent } from './use-cases-grid/use-cases-grid.component';
import { WorkflowsGridComponent } from './workflows-grid/workflows-grid.component';

@Component({
  selector: 'app-workflow-explorer',
  templateUrl: './workflow-explorer.component.html',
  styleUrls: ['./workflow-explorer.component.scss']
})
export class WorkflowExplorerComponent implements OnInit {
  setupId: string;
  workflowId: string;
  setup: SingleSetupDto;
  workflowName: string;

  @ViewChild(UseCasesGridComponent) useCasesComponent: UseCasesGridComponent
  @ViewChild(WorkflowsGridComponent) workflowGridComponent: WorkflowsGridComponent

  constructor(protected activatedRoute: ActivatedRoute, protected dialogService: NbDialogService, protected setupsClient: SetupsClient, @Inject(AppComponent) protected parent: AppComponent, private setupService: SetupService) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(data => {
      this.setupId = data.id;
      //console.log(`Nexting ${this.setupId}`);
      //this.setupService.currentSetupId.next(this.setupId);
      this.setupsClient.getSetupById(this.setupId).subscribe(x => {
        this.setup = x;
        this.setupService.currentSetupId.next(this.setupId);
      });
    });
  }

  handleWorkflowSelect(event: any) {
    this.workflowId = event.id;
    this.workflowName = event.name;
  }

  onAddWorkflow() {
    const ref = this.dialogService.open(WorkflowFormComponent);
    ref.onClose.subscribe(res => {
      this.workflowGridComponent.refresh();
    })
  }

}
