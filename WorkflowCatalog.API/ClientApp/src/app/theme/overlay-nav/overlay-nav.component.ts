import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { NavigationStart, Router } from '@angular/router';
import { NbDialogRef, NbMenuComponent, NbMenuItem, NbMenuService } from '@nebular/theme';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { SetupService } from 'src/app/_providers/setup.service';
import { isNil} from 'lodash';

@Component({
  selector: 'app-overlay-nav',
  templateUrl: './overlay-nav.component.html',
  styleUrls: ['./overlay-nav.component.scss']
})
export class OverlayNavComponent implements OnInit {
  private destroy$ = new Subject<void>();

  private hasEditAuthority: boolean = true;

  private wfCatalogExpanded: boolean = true;

  showActorEditOptions: boolean = this.wfCatalogExpanded;
  showSetupEditOptions: boolean = this.hasEditAuthority && this.wfCatalogExpanded;

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



  constructor(private nbMenuService: NbMenuService, protected cd: ChangeDetectorRef, protected setupService: SetupService, protected router: Router,private ref: NbDialogRef<OverlayNavComponent>) { }


  
  ngOnInit(): void {
    this.nbMenuService.addItems(this.items,'overlay');
    this.nbMenuService.onSubmenuToggle()
      .pipe(takeUntil(this.destroy$))
      .subscribe(x => this.handleOnSubmenuToggle(x));
    this.router.events.subscribe(event => {
      if(event instanceof NavigationStart){
        this.ref.close();
      }
    })
  }

  closeMe(){
    this.ref.close();
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

}
