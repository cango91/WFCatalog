import { Component, Input, OnInit } from '@angular/core';
import { NbDialogRef, NbToastrService } from '@nebular/theme';
import { CreateWorkflowCommand, DeleteDiagramCommand, DeleteWorkflowCommand, DiagramsClient, DiagramsMetaDto, ICreateWorkflowCommand, IUpdateActorCommand, IUpdateWorkflowDetailsCommand, WorkflowsClient } from 'src/app/web-api-client';
import { SetupService } from 'src/app/_providers/setup.service';
import { isNil } from 'lodash';
import { of } from 'rxjs';
import { mergeMap } from 'rxjs/operators';

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
    setupId: null
  };

  @Input() workflowId: string;

  editMode = false;

  uploadedFiles: DiagramsMetaDto[] = [];
  deletedFileIds: string[] = [];

  diagramFiles: File[] = [];

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
        };

        this.diagramService.getDiagramsMetaData('workflowId==' + this.workflowId, '', 1, 500).subscribe(res => {
          this.uploadedFiles = res.items;
        })
      })
    }
  }

  handleFileInput(files: File[]) {
    const fileArray = [...files];
    this.diagramFiles.push(...fileArray);

    const fileArr = this.uploadedFiles;
    fileArr.push(...fileArray.map(a => (new DiagramsMetaDto({id:null,name:a.name}))));
    this.uploadedFiles = fileArr;

  }

  closeDialog() {
    this.dialogRef.close();
  }

  onDelete(fileId) {
    const fileArr = this.uploadedFiles;
    fileArr.splice(fileArr.findIndex(s => s.id === fileId), 1);
    this.uploadedFiles = fileArr;
    this.deletedFileIds.push(fileId);
  }

  create() {
    //debugger;
    this.request.workflowType = parseInt(this.request.workflowType.toString(), 0);
    this.workflowService.createWorkflow(new CreateWorkflowCommand(this.request)).subscribe(res => {
      if (this.diagramFiles.length > 0) {
        of(...this.diagramFiles).pipe(mergeMap(a =>  this.diagramService.createDiagram(res, {
          data: a,
          fileName: a.name
        })))
       .subscribe(res => {
        
        }, err => {
          this.workflowService.deleteWorkflow(res, new DeleteWorkflowCommand({ id: res })).subscribe(a => {
            this.toast.danger("Please select a valid PDF file!");
          })
        },() => {
          this.toast.success("Workflow added!");
          this.dialogRef.close();
        });
      } else {
        this.toast.success("Workflow added!");
        this.dialogRef.close();
      }
    })
  }

  edit() {
    // silinmişse dosyaları sil

    this.request.workflowType = parseInt(this.request.workflowType.toString(), 0);
    this.request.id = this.workflowId;
    this.workflowService.updateWorkflow(this.workflowId, this.request).subscribe(res => {
      of(...this.deletedFileIds).pipe(mergeMap(s => this.diagramService.deleteDiagram(s, new DeleteDiagramCommand({ id: s })))).subscribe(() => { }, err => { }, () => {
        this.toast.success("Workflow updated!");
        this.dialogRef.close();
      })
    })
  }

}
