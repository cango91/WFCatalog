import { Component, OnInit } from '@angular/core';
import { NbMenuItem } from '@nebular/theme/components/menu/menu.service';

@Component({
  selector: 'app-overlay-nav',
  templateUrl: './overlay-nav.component.html',
  styleUrls: ['./overlay-nav.component.scss']
})
export class OverlayNavComponent implements OnInit {

  constructor() { }

  items: NbMenuItem[] = [{
    title: 'hello',
    link: '#'
  }]
  ngOnInit(): void {
  }

}
