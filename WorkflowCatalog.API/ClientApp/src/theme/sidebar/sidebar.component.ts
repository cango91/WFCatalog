import { Component, Input, OnInit } from '@angular/core';
import { SetupsDto } from 'src/app/web-api-client';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {

  activeMenu = 0;
  
  @Input() setups : Array<SetupsDto>

  constructor() { }

  ngOnInit(): void {
  }

}
