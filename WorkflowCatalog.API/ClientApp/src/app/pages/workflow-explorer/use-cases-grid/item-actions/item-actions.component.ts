import { Component, EventEmitter, Input, OnDestroy, OnInit } from '@angular/core';
import { NbMenuService } from '@nebular/theme';
import { Subscription } from 'rxjs';
import { filter, map } from 'rxjs/operators';

@Component({
  selector: 'app-item-actions',
  templateUrl: './item-actions.component.html',
  styleUrls: ['./item-actions.component.scss']
})
export class UCItemActionsComponent implements OnInit, OnDestroy {

  @Input()
  value: string | number;
  @Input()
  rowData: any;

  @Input()
  editAuthority: boolean = true;

  items = []; 

  subscription: Subscription;
  onClick: EventEmitter<any> = new EventEmitter();

  constructor(protected nbMenuService: NbMenuService) { }

  ngOnInit(): void {
    //debugger;
    this.items = [{
      title: this.editAuthority ? 'Edit' : 'View', 
      data: {action: 'edit',
    id: this.rowData.id},
    },
    {
      title: 'Delete',
      data: {action: 'delete',
    id: this.rowData.id},
    }];

    this.subscription = this.nbMenuService.onItemClick()
      .pipe(
        filter(({tag}) => tag === 'usecases-item-actions-menu'),
        map(({item: {data}}) => data)
      )
      .subscribe(data => {
        if(data.id === this.rowData.id){
        this.onClick.emit({action: data.action, element: this.rowData});
        }
      });
  }

  ngOnDestroy(): void{
    if(this.subscription){
      this.subscription.unsubscribe();
    }
  }

  setEditAuthority(val: boolean) {
    this.editAuthority = val;
  }

}


