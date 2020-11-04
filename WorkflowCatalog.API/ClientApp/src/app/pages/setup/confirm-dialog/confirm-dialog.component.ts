import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'ngx-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
  styleUrls: ['./confirm-dialog.component.scss']
})
export class ConfirmDialogComponent implements OnInit {

  @Input() title: string;
  @Input() content: string;
  
  constructor() { }

  ngOnInit(): void {
  }

}
