import { Component, Input, OnInit, OnDestroy, Output, ChangeDetectorRef, SimpleChanges, OnChanges } from '@angular/core';
import { ActivatedRoute, NavigationStart, Router } from '@angular/router';
import { NbMenuItem, NbMenuService, NbSidebarService } from '@nebular/theme';
import { stringify } from 'querystring';
import { of, Subject } from 'rxjs';
import { map, takeUntil } from 'rxjs/operators'
import { SetupService } from 'src/app/_providers/setup.service';

@Component({
  selector: 'app-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.scss']
})
export class SideNavComponent implements OnDestroy, OnInit, OnChanges {

  private destroy$ = new Subject<void>();

  private hasEditAuthority: boolean = true;

  private wfCatalogExpanded: boolean = true;

  showActorEditOptions: boolean = this.wfCatalogExpanded;
  showSetupEditOptions: boolean = this.hasEditAuthority && this.wfCatalogExpanded;

  @Input()
  items: NbMenuItem[] = [
    {
      title: 'Functional Catalog',
      expanded: false,
      children: [
      ]
    },
    {
      title: 'Workflow Catalog',
      expanded: true,
      children: []
    }
  ];

  itemsCompact = [];

  private _menuState: string = 'expanded';

  @Input()
  menuState: string;

  _setup: string;

  constructor(private nbMenuService: NbMenuService, protected cd: ChangeDetectorRef, protected setupService: SetupService, protected router: Router, protected nbSidebar:NbSidebarService) {
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes.items && changes.items.currentValue) {
      of(this.items).subscribe(res => {

        if (res.length > 1) {
          if (res[1].children.length > 0) {
            this.itemsCompact = [];
            res[1].children.map(x => {
              this.itemsCompact.push({
                title: x.title,
                link: x.link,
                count: x.badge.text,
                setupId: x.link.slice(x.link.indexOf('pages/wf/') + 'pages/wf/'.length),
              })

            });
            //debugger;
            this.cd.detectChanges();
          }
        }
      })

    }



  }

  editActors: boolean;
  editSetups: boolean;

  ngOnInit() {
    this.nbMenuService.addItems(this.items);
    this.nbMenuService.onSubmenuToggle()
      .pipe(takeUntil(this.destroy$))
      .subscribe(x => this.handleOnSubmenuToggle(x));

    this.setupService.currentSetupId.subscribe(x => {
      this._setup = x;
      this.cd.detectChanges();
    });


    this.router.events.subscribe(event => {
      
      if (event instanceof NavigationStart) {
        if (event.url === '/pages/editactors') {
          this.editActors = true;

        } else {
          this.editActors = false;
        }
        if (event.url === '/pages/editsetups') {
          this.editSetups = true;
        }else{
          this.editSetups = false;
        }

        this.cd.detectChanges();
      }
    })


  }

  private handleOnSubmenuToggle(res: any) {
    if (res.item.title === 'Workflow Catalog') {
      this.wfCatalogExpanded = res.item.expanded;
    }

    if (this.wfCatalogExpanded) {
      this.showActorEditOptions = true;
      if (this.hasEditAuthority) {
        this.showSetupEditOptions = true;
      }
    } else {
      this.showActorEditOptions = false;
      this.showSetupEditOptions = false;
    }
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }

  onClickMenuNavItem(event) {
    //debugger;
    if (event.currentTarget.id.indexOf('nav-button-setup-') !== -1) {
      this.setupService.currentSetupId.next(event.currentTarget.id.slice('nav-button-setup-'.length));
      this.router.navigate(['pages/wf/' + event.currentTarget.id.slice('nav-button-setup-'.length)])
    }else{
      switch(event.currentTarget.id){
        case 'nav-button-actors':
          this.setupService.currentSetupId.next(null);
          this.router.navigate(['pages/editactors']);
          break;
        case 'nav-button-setups':
          this.setupService.currentSetupId.next(null);
          this.router.navigate(['pages/editsetups']);
          break;
      }
      
    }
  }

  toggleSidebar(){
    this.nbSidebar.expand();
    this.menuState = 'expanded';
    this.cd.detectChanges();
  }
}
