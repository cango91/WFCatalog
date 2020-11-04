import { EventEmitter, Input, OnDestroy } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { NbMenuService } from '@nebular/theme';
import { isNil } from 'lodash';
import { Subscription } from 'rxjs';
import { filter, map } from 'rxjs/operators';

@Component({
  selector: 'ngx-use-case-menuitem',
  templateUrl: './use-case-menuitem.component.html',
  styleUrls: ['./use-case-menuitem.component.scss']
})
export class UseCaseMenuitemComponent implements OnInit, OnDestroy {

  @Input()
  value: string | number;
  @Input()
  rowData: any;

  @Input()
  ucId: number;

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
    this.ucId = this.rowData.id;

    this.items = [
      { title: this.editAuthority ? "Edit" : "View", data: 'edit' },
      { title: "Delete", data: 'delete' }
    ];

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
