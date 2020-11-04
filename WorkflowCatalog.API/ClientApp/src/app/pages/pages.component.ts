import { Component, OnInit } from '@angular/core';
import { SetupsClient } from '../web-api-client';

import { MENU_ITEMS } from './pages-menu';

@Component({
  selector: 'ngx-pages',
  styleUrls: ['pages.component.scss'],
  template: `
    <ngx-one-column-layout>
      <nb-menu [items]="menu"></nb-menu>
      <router-outlet></router-outlet>
    </ngx-one-column-layout>
  `,
})
export class PagesComponent implements OnInit {

  menu = MENU_ITEMS;

  /**
   *
   */
  constructor(protected setupClient: SetupsClient) {

  }
  ngOnInit(): void {
    this.setupClient.get(null, null).subscribe(res => {
      let menu = MENU_ITEMS;
      res.setups.forEach(setup => {
        menu[2].children.push({
          title: setup.shortName,
          link: '/pages/setup/' + setup.id,
        })
      })
    })
  }
}
