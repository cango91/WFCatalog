import { Component, OnInit, Input } from '@angular/core';
import {isNil} from 'lodash';


@Component({
  selector: 'app-workflow-item-menu',
  templateUrl: './workflow-item-menu.component.html',
  styleUrls: ['./workflow-item-menu.component.scss']
})
export class WorkflowItemMenuComponent implements OnInit {

  @Input() 
  value: string|number;
  @Input()
  rowData: any;

  @Input()
  wfId: number;

  @Input()
  editAuthority: boolean;

  @Input()
  viewDiagram: boolean;


  constructor() { 
  }

  ngOnInit(): void {
    this.wfId = this.rowData.id;
    this.viewDiagram  = !isNil(this.rowData.primaryDiagramId);
  }

  setEditAuthority(val: boolean){
    this.editAuthority = val;
  }


}
