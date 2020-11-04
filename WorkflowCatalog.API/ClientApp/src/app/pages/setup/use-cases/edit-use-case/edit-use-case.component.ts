import { Component, Input, OnInit } from '@angular/core';
import { NbDialogRef, NbToastrService } from '@nebular/theme';
import { ActorsClient, UCActorDto, UpdateUseCaseDetailsCommand, UseCasesClient, UseCasesDto } from '../../../../web-api-client';

@Component({
  selector: 'ngx-edit-use-case',
  templateUrl: './edit-use-case.component.html',
  styleUrls: ['./edit-use-case.component.scss']
})
export class EditUseCaseComponent implements OnInit {

  useCase: UseCasesDto;

  actors: UCActorDto[];

 @Input() editAuthority: boolean = false;

  @Input() useCaseId: number;

  editableObject = {
    name: null,
    description: null,
    actors: [],
    preconditions: null,
    postconditions: null,
    normalcourse: null,
    altcourse: null
  }

  constructor(private useCaseService: UseCasesClient, private actorsService: ActorsClient, protected dialogRef: NbDialogRef<EditUseCaseComponent>, protected toast: NbToastrService) { }

  ngOnInit(): void {
    if(this.useCaseId){this.useCaseService.getSingle(this.useCaseId).subscribe(res => {
      this.useCase = res.items[0];
      this.editableObject = {
        name: this.useCase.name,
        description: this.useCase.description,
        actors: this.useCase.actors,
        preconditions: this.useCase.preconditions,
        postconditions: this.useCase.postconditions,
        normalcourse: this.useCase.normalCourse,
        altcourse: this.useCase.altCourse
      };
    })}

    this.actorsService.getAll(null, null).subscribe(s => this.actors = s);
  }

  save() {
    var request = new UpdateUseCaseDetailsCommand({
      id: this.useCaseId,
      name: this.editableObject.name,
      description: this.editableObject.description,
      actors: this.editableObject.actors,
      preconditions: this.editableObject.preconditions,
      postconditions: this.editableObject.postconditions,
      normalCourse: this.editableObject.normalcourse,
      altCourse: this.editableObject.altcourse

    });

    this.useCaseService.update(this.useCaseId, request).subscribe(res => {
      this.toast.success("Use Case Updated!");
      this.dialogRef.close();
    }, err => {
      this.toast.danger('Error!');
    })
  }

}
