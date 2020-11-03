import { Component, Input, OnInit } from '@angular/core';
import { SetupsDto } from 'src/app/web-api-client';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
import {last} from 'lodash';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {

  activeMenu = 1;
  activeSetup = null;

  @Input()
  editAuthority: boolean = false;
  
  @Input() setups : Array<SetupsDto>;
  //@Input() open : number;

  constructor(private router: Router) { }

  ngOnInit(): void {
    this.router.events.subscribe(event => {
      if(event instanceof NavigationEnd){
          this.activeSetup = parseInt(last(event.urlAfterRedirects.split('/')));
      }
    })
  }

}
