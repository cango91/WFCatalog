import { Component, EventEmitter, Input, OnInit } from '@angular/core';
import { NbMenuService } from '@nebular/theme';
import { Subscription } from 'rxjs';
import { filter, map } from 'rxjs/operators';

@Component({
  selector: 'ngx-workflow-menuitem',
  templateUrl: './workflow-menuitem.component.html',
  styleUrls: ['./workflow-menuitem.component.scss']
})
export class WorkflowMenuitemComponent implements OnInit {
  @Input()
  value: string | number;
  @Input()
  rowData: any;

  @Input()
  wfId: number;

  items = [
  ]

  @Input()
  editAuthority: boolean;

  onClick: EventEmitter<any> = new EventEmitter();

  subscription: Subscription;

  constructor(protected nbMenuService: NbMenuService) {
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  ngOnInit(): void {
    this.wfId = this.rowData.id;

    this.items = [
      { title: this.editAuthority ? "Edit" : "View", data: 'edit' },
      { title: "Delete", data: 'delete' }
    ];

    if(!this.rowData.primaryDiagramId){
      this.items = [{ title: 'View Diagram', data: 'diagram'}].concat(...this.items);
    }
    

    this.subscription = this.nbMenuService.onItemClick()
      .pipe(
        filter(({ tag }) => tag === 'useCaseMenu'),
        map(({ item: { data } }) => data),
      )
      .subscribe(data => {
        this.onClick.emit({ action: data, id: this.rowData.id })
      });
  }

  setEditAuthority(val: boolean) {
    this.editAuthority = val;
  }

}
