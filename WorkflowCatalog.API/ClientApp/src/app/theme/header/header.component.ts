import { Component, OnDestroy, OnInit } from '@angular/core';
import { NbMenuService } from '@nebular/theme';
import { Subscription } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment.prod';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit,OnDestroy {

  items = [
    { title: 'Settings', link: environment.userSettingsUrl},
    { title: 'Logout', link: environment.logoutUrl},
  ];

  subscription: Subscription

  constructor(protected menuService: NbMenuService) { }
  ngOnDestroy(): void {
    throw new Error('Method not implemented.');
  }

  ngOnInit(): void {
    this.subscription = this.menuService.onItemClick()
      .pipe(
        filter(({tag}) => tag === 'user-menu')
      )
      .subscribe(data => {
        window.location.href = data.item.link;
        });
  }

}
