import { HttpClient } from '@angular/common/http';
import { AfterViewInit, ChangeDetectorRef, Component, OnInit, SimpleChange, TemplateRef, ViewChild } from '@angular/core';
import { NavigationEnd, NavigationStart, Router } from '@angular/router';
import { NbDialogService, NbMenuItem, NbSidebarComponent, NbSidebarService, NbThemeService, NbMediaBreakpoint } from '@nebular/theme';
import { environment } from 'src/environments/environment.prod';
import { OverlayNavComponent } from './theme/overlay-nav/overlay-nav.component';
import { SetupsClient } from './web-api-client';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, AfterViewInit {
  title = 'Workflow Catalog';
  preventAbuse = false;

  items : NbMenuItem[] =[];

  products: NbMenuItem[] = [
    {
      title: 'Team',
      url : environment.teamUrl,
      pathMatch:'full',
    },
    {title: 'Cerebral Plus',
    url : environment.cpUrl,
    pathMatch:'full',
  },
    {title: 'Acıbadem Online',
    url : environment.aoUrl,
    pathMatch:'full',
    },
    {title: 'In-Ticket',
    url : environment.itUrl,
    pathMatch:'full',
    },
    
  ]

  overlayMenuOpen: boolean = false;
  sidebarState: string = 'expanded';
  sidebarDesiredState: string = 'expanded';
  @ViewChild(NbSidebarComponent) sidebar: NbSidebarComponent;

  /**
   *
   */
  constructor(private http: HttpClient, private setupsClient: SetupsClient,protected dialogService: NbDialogService,protected sidebarService: NbSidebarService, protected themeService: NbThemeService,private cd: ChangeDetectorRef,protected router: Router) {
  }
  ngAfterViewInit(): void {
  
    this.themeService.onMediaQueryChange()
      .subscribe(([prev,current]:[NbMediaBreakpoint,NbMediaBreakpoint]) => {
        const isCollapsed = this.sidebar.collapsedBreakpoints.includes(current.name);
        const isCompacted = this.sidebar.compactedBreakpoints.includes(current.name);

        if(isCompacted){
          this.sidebarState = 'compacted';
          this.sidebarDesiredState = 'compacted';
        }
        if(isCollapsed){
          this.sidebarState = 'collapsed';
          this.sidebarDesiredState = 'collapsed';
        }
        if(!isCollapsed && !isCompacted && prev.width < current.width){
          this.sidebarState = 'expanded';
          this.sidebarDesiredState = 'expanded';
        }

        this.cd.detectChanges();
      })

      this.sidebarService.onExpand().subscribe(x => {
        this.sidebarState = 'expanded';
      });
      this.sidebarService.onCompact().subscribe(x => {
        this.sidebarState = 'compacted';
      });
    
  }

  testHttp(){
    this.preventAbuse = true;
    this.http.get("/api/Setups").subscribe(res =>
      {
        console.log(res);
        setTimeout(() => {
          this.preventAbuse = false;
        }, 800)
      })
  }


  ngOnInit(){
    this.setupsClient.getSetups('status==1',null,null,null).subscribe(res =>{
      this.updateSetups(res.items);
    });

    //debugger;

    this.router.events.subscribe(event => {
      if(event instanceof NavigationEnd){
        //debugger;
        if(this.sidebarState !== this.sidebarDesiredState){
          switch(this.sidebarDesiredState){
            case 'expanded':
              if(this.sidebarState !== 'compacted'){
                this.sidebarService.expand();
                this.sidebarState = this.sidebarDesiredState;
              }
              break;
            case 'compacted':
              this.sidebarService.compact();
              this.sidebarState = this.sidebarDesiredState;
              break;
            case 'collapsed':
              this.sidebarService.collapse();
              this.sidebarState = this.sidebarDesiredState;
              break;
          }
          
        }
      }
    })
  }

  public refresh(){
    this.setupsClient.getSetups('status==1',null,1,100).subscribe(res =>{
      this.updateSetups(res.items);
    })
  }
  private updateSetups(res:any){
    let children: NbMenuItem[] = [];
    res.forEach(element => {
        if(element.status === 1){
        children.push({
          title: element.shortName.toLocaleUpperCase(),
          link: 'pages/wf/' + element.id,
          pathMatch:'full',
        badge: {
          text: element.workflowCount.toString(),
          status: 'info'
        }});
      }
    });
    this.items = [{
      title: 'Function Catalog',
      expanded: false,
      children: this.products
    },
    {
      title: 'Workflow Catalog',
      expanded: true,
      children: children
    }];
  }

  toggleMenuFab(){
    if(this.overlayMenuOpen){

    }else{
      this.dialogService.open(OverlayNavComponent, {dialogClass:'menu-overlay', context: {items:this.items}});

    }
  }

  toggleSideBar(){
    this.sidebarService.compact();
  }

}
