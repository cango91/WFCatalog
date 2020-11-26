import { Component, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
//import { MatButtonModule } from '@angular/material/button';
import { NbDialogRef } from '@nebular/theme';
import { FormControl, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-confirm-delete-dialog',
  templateUrl: './confirm-delete-dialog.component.html',
  styleUrls: ['./confirm-delete-dialog.component.scss']
})
export class ConfirmDeleteDialogComponent implements OnInit {


  @Input()
  setupName: string;

  @Input()
  setupAbb: string;

  @Output()
  confirmed: boolean =false;

  enableDelete: boolean = false;

  confirmationInput = new FormControl('',{
    validators: [
    Validators.required,
    Validators.pattern(new RegExp("(?<![\w])^I understand what I am doing$(?![\w])","gi"))
  ], 
  updateOn: 'change'
});


  constructor(protected dialogRef: NbDialogRef<ConfirmDeleteDialogComponent>) { }

  ngOnInit(): void {
    this.confirmationInput.statusChanges.subscribe(x => {
      this.enableDelete = x === 'VALID';
    })
  }


  cancelDialog(){
    this.confirmed = this.enableDelete;
    this.dialogRef.close();
  }
}
