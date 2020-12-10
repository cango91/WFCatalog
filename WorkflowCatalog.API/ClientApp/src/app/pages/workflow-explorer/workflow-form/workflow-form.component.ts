import { Component, Input, OnInit } from '@angular/core';
import { NbDialogRef, NbToastrService } from '@nebular/theme';
import { CreateWorkflowCommand, DeleteDiagramCommand, DeleteWorkflowCommand, DiagramsClient, DiagramsMetaDto, ICreateWorkflowCommand, IUpdateActorCommand, IUpdateWorkflowDetailsCommand, WorkflowsClient } from 'src/app/web-api-client';
import { SetupService } from 'src/app/_providers/setup.service';
import { isNil } from 'lodash';
import { of } from 'rxjs';
import { mergeMap } from 'rxjs/operators';
import { DiagramUploadItem } from './diagram-upload/diagram-upload-item';
import { Guid } from 'src/app/helpers/guid';

@Component({
  selector: 'app-workflow-form',
  templateUrl: './workflow-form.component.html',
  styleUrls: ['./workflow-form.component.scss']
})
export class WorkflowFormComponent implements OnInit {

  request: ICreateWorkflowCommand | IUpdateWorkflowDetailsCommand | any = {
    name: null,
    description: null,
    workflowType: null,
    setupId: null,
    primaryDiagramId: null,
  };

  loading = false;

  @Input() workflowId: string;

  editMode = false;

  diagramFiles: DiagramUploadItem[] = [];
  filesToDelete: DiagramUploadItem[] = [];
  filesToAdd: DiagramUploadItem[] = [];

  constructor(protected workflowService: WorkflowsClient, protected dialogRef: NbDialogRef<WorkflowFormComponent>, protected toast: NbToastrService, protected diagramService: DiagramsClient, protected setupService: SetupService) { }

  ngOnInit(): void {
    this.setupService.currentSetupId.subscribe(a => this.request.setupId = a);

    if (!isNil(this.workflowId)) {
      this.editMode = true;

      this.workflowService.getWorkflowById(this.workflowId).subscribe(res => {
        this.request = {
          name: res.name,
          description: res.description,
          workflowType: res.workflowType,
          primaryDiagramId: res.primaryDiagramId,
        };

        this.diagramService.getDiagramsMetaData('workflowId==' + this.workflowId, '', 1, 500).subscribe(res => {
          this.diagramFiles = res.items.map(a => ({
            id: a.id,
            name: a.name,
            generatedId: Guid.newGuid(),
            isDefault: false,
            data: null
          }));
        })
      })
    }
  }

  closeDialog() {
    this.dialogRef.close();
  }

  create() {
    this.loading = true;
    //debugger;
    this.request.workflowType = parseInt(this.request.workflowType.toString(), 0);
    this.workflowService.createWorkflow(new CreateWorkflowCommand(this.request)).subscribe(res => {
      if (this.diagramFiles.length > 0) {
        of(...this.filesToAdd).pipe(mergeMap(a => this.diagramService.createDiagram(res, {
          data: a.data,
          fileName: a.name
        })))
          .subscribe(res => {

          }, err => {
            //debugger;
            this.workflowService.deleteWorkflow(res, new DeleteWorkflowCommand({ id: res })).subscribe(a => {
              this.toast.danger("Please select a valid PDF file!");
            })
            this.loading = false;
          }, () => {
            this.loading = false;
            this.toast.success("Workflow added!");
            this.dialogRef.close();
          });
      } else {
        this.loading = false;
        this.toast.success("Workflow added!");
        this.dialogRef.close();
      }
    })
  }

  edit() {
    this.loading = true;
    this.request.workflowType = parseInt(this.request.workflowType.toString(), 0);
    this.request.id = this.workflowId;
    this.workflowService.updateWorkflow(this.workflowId, this.request).subscribe(res => {
      of(...this.filesToDelete).pipe(mergeMap(s => this.diagramService.deleteDiagram(s.id, new DeleteDiagramCommand({ id: s.id })))).subscribe(() => { }, err => { }, () => {
        of(...this.filesToAdd).pipe(mergeMap(s => this.diagramService.createDiagram(this.workflowId, {
          data: s.data,
          fileName: s.name
        }))).subscribe(res => { }, err => { }, () => {
          this.loading = false;
          this.toast.success("Workflow updated!");
          this.dialogRef.close();
        })
      })
    })
  }

}
