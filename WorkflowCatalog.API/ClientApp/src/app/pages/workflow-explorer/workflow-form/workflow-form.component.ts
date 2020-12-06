import { Component, OnInit } from '@angular/core';
import { NbDialogRef, NbToastrService } from '@nebular/theme';
import { CreateWorkflowCommand, DeleteWorkflowCommand, DiagramsClient, ICreateWorkflowCommand, WorkflowsClient } from 'src/app/web-api-client';
import { SetupService } from 'src/app/_providers/setup.service';

@Component({
  selector: 'app-workflow-form',
  templateUrl: './workflow-form.component.html',
  styleUrls: ['./workflow-form.component.scss']
})
export class WorkflowFormComponent implements OnInit {

  request: ICreateWorkflowCommand = {
    name: null,
    description: null,
    workflowType: null,
    setupId: null
  }
  

  diagramFile: File = null;

  constructor(protected workflowService: WorkflowsClient, protected dialogRef: NbDialogRef<WorkflowFormComponent>, protected toast: NbToastrService, protected diagramService: DiagramsClient, protected setupService: SetupService) { }

  ngOnInit(): void {
    this.setupService.currentSetupId.subscribe(a => this.request.setupId = a);
  }

  handleFileInput(files: File[]) {
    this.diagramFile = files[0];
  }

  closeDialog(){
    this.dialogRef.close();
  }

  create() {
    //debugger;
    this.request.workflowType = parseInt(this.request.workflowType.toString(), 0);
    this.workflowService.createWorkflow(new CreateWorkflowCommand(this.request)).subscribe(res => {
      if (this.diagramFile) {
        this.diagramService.createDiagram(res, {
          data: this.diagramFile,
          fileName: this.diagramFile.name
        }).subscribe(res => {
          this.toast.success("Workflow added!");
          this.dialogRef.close();
        }, err => {
          this.workflowService.deleteWorkflow(res, new DeleteWorkflowCommand({ id: res })).subscribe(a => {
            this.toast.danger("Please select a valid PDF file!");
          })
        });
      } else {
        this.toast.success("Workflow added!");
        this.dialogRef.close();
      }
    })
  }

}
