import { Component, Input, OnInit, OnDestroy, Output } from '@angular/core';
import { NbMenuItem, NbMenuService, NbSidebarService } from '@nebular/theme';
import { of, Subject } from 'rxjs';
import { map, takeUntil } from 'rxjs/operators'

@Component({
  selector: 'app-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.scss']
})
export class SideNavComponent implements OnDestroy, OnInit {

  private destroy$ = new Subject<void>();

  private hasEditAuthority: boolean = true;

  private wfCatalogExpanded: boolean = true;

  showActorEditOptions: boolean = this.wfCatalogExpanded;
  showSetupEditOptions : boolean = this.hasEditAuthority && this.wfCatalogExpanded;

  @Input()
  items: NbMenuItem[] = [
    {
      title: 'Function Catalog',
      expanded: false,
      children: []
    },
    {
      title: 'Workflow Catalog',
      expanded: true,
      children: []
    }
  ];

  constructor(private nbMenuService: NbMenuService, private sideBarService:NbSidebarService) { 
  }

  ngOnInit(){
    this.nbMenuService.addItems(this.items);
    this.nbMenuService.onSubmenuToggle()
    .pipe(takeUntil(this.destroy$))
    .subscribe(x => this.handleOnSubmenuToggle(x));  
  }

  private handleOnSubmenuToggle(res: any){
    if(res.item.title === 'Workflow Catalog'){
      this.wfCatalogExpanded = res.item.expanded;
    }

    if(this.wfCatalogExpanded){
      this.showActorEditOptions = true;
      if(this.hasEditAuthority){
        this.showSetupEditOptions = true;
      }
    }else{
      this.showActorEditOptions = false;
      this.showSetupEditOptions = false;
    }
  }

  ngOnDestroy(){
    this.destroy$.next();
    this.destroy$.complete();
  }
}
