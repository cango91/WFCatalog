import { Component, Input, OnInit } from '@angular/core';
import { NbDialogRef } from '@nebular/theme';

@Component({
  selector: 'app-confirmation-prompt',
  templateUrl: './confirmation-prompt.component.html',
  styleUrls: ['./confirmation-prompt.component.scss']
})
export class ConfirmationPromptComponent implements OnInit {

  @Input()
  headerText: string = "Confirm Action";

  @Input()
  bodyText: string = "Please confirm you want to proceed with this action.";

  @Input()
  buttonStyle:string = "hero";

  @Input()
  cancelButtonStatus = 'danger';

  @Input()
  confirmButtonStatus = 'success';

  @Input()
  cancelButtonContent = 'Cancel';

  @Input()
  confirmButtonContent = 'Confirm';
  constructor(protected dialogRef: NbDialogRef<ConfirmationPromptComponent>) { }

  ngOnInit(): void {
  }

  confirm(){
    this.dialogRef.close(true);
  }

  cancel(){
    this.dialogRef.close(false);
  }

}
