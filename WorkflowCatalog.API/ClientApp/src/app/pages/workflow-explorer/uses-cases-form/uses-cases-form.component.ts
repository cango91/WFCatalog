import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder,FormGroup } from '@angular/forms';
import { NbDialogRef, NbToastrService } from '@nebular/theme';
import { map } from 'rxjs/operators';
import { ActorDto, ActorsClient, CreateUseCaseCommand, ICreateUseCaseCommand, IUpdateUseCaseDetailsCommand, UpdateUseCaseDetailsCommand, UseCasesClient } from 'src/app/web-api-client';
import { SetupService } from 'src/app/_providers/setup.service';
import { WorkflowService } from 'src/app/_providers/workflow.service';

@Component({
  selector: 'app-uses-cases-form',
  templateUrl: './uses-cases-form.component.html',
  styleUrls: ['./uses-cases-form.component.scss']
})
export class UsesCasesFormComponent implements OnInit {

  constructor(protected useCaseService: UseCasesClient, protected dialogRef: NbDialogRef<UsesCasesFormComponent>, protected toast: NbToastrService, protected setupService: SetupService, protected workflowService: WorkflowService, protected actorsClient: ActorsClient, private fb: FormBuilder) { }

  myForm:FormGroup;
  private _setupId: string;
  private actorList: ActorDto[];
  actorItems : Array<any>;

  @Input()
  mode: string = 'new';

  @Input()
  element = {
    id: null,
    name: null,
    description: null,
    preconditions: null,
    postconditions: null,
    normalCourse: null,
    altCourse: null,
    actors: null,
  }

  updateRequest: IUpdateUseCaseDetailsCommand = {
    id: null,
    name: null,
    description: null,
    preconditions: null,
    postconditions: null,
    normalCourse: null,
    altCourse: null,
    actors: new Array<string> (),
  };

  settings = {
    singleSelection: false,
    idField: 'id',
    textField: 'text',
    selectAllText: 'Select All',
    unSelectAllText: 'UnSelect All',
    itemsShowLimit: 2,
    allowSearchFilter: true
  }

  request: ICreateUseCaseCommand = {
    workflowId: null,
    name : null,
    description : null,
    preconditions: null,
    postconditions: null,
    normalCourse: null,
    altCourse: null,
    actors: new Array<string>()

  }

  

  ngOnInit(): void {
    //console.log(this.mode);
    //console.log(this.element);
    this.setupService.currentSetupId.subscribe(x => {
      this._setupId = x;
      this.actorsClient.getActors(`setup.id==${this._setupId}`,null,1,500).pipe(
        map(x => x.items)
      ).subscribe(res => {
        this.actorList = res;
        this.actorItems = this.actorList.map(a => ({id: a.id, text: a.name}))
      })
    });
    this.workflowService.selectedWorkflowId.subscribe(x => this.request.workflowId = x);
    if(this.mode==='edit'){
      this.myForm = this.fb.group({
        actors: [this.element.actors]
      })
    }
    
  }

  addActor(data: any){
    this.request.actors.push(data.id);
  }

  removeActor(data: any){
    this.request.actors.splice(this.request.actors.findIndex(x => x===data.id));
  }

  create(){
    this.useCaseService.createUseCase(new CreateUseCaseCommand(this.request)).subscribe(x => {
      this.toast.success('Use Case Added!');
      this.dialogRef.close();
    },err => {
      this.toast.danger('Could not create use case!');
    })
  }

  update(){
    this.updateRequest = {
      id: this.element.id,
      name: this.element.name,
      description: this.element.description,
      preconditions: this.element.preconditions,
      postconditions: this.element.postconditions,
      normalCourse: this.element.normalCourse,
      altCourse: this.element.altCourse,
      actors: this.myForm.get('actors').value.map(x => { return x.id}),
    }

    this.useCaseService.updateUseCaseDetails(this.updateRequest.id, new UpdateUseCaseDetailsCommand( this.updateRequest)).subscribe(x => {
      this.toast.success('Use Case updated successfully');
      this.dialogRef.close();
    }, err=> {
      this.toast.danger('Could not update use case details!');
    });
  }

  closeDialog(){
    this.dialogRef.close();
  }

}
