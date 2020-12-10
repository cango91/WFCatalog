import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { NbMenuService } from '@nebular/theme';
import { Subscription } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { User } from 'src/app/_models/user.model';
import { UserService } from 'src/app/_services/user.service';
import { environment } from 'src/environments/environment.prod';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit, OnDestroy {

  items = [
    { title: 'Settings', link: environment.userSettingsUrl },
    { title: 'Logout', link: environment.logoutUrl },
  ];

  subscription: Subscription
  user: User = new User();

  constructor(protected menuService: NbMenuService, protected userService: UserService) { }
  ngOnDestroy(): void {
    throw new Error('Method not implemented.');
  }

  ngOnInit(): void {
    this.subscription = this.menuService.onItemClick()
      .pipe(
        filter(({ tag }) => tag === 'user-menu')
      )
      .subscribe(data => {
        window.location.href = data.item.link;
      });

    this.userService.user.subscribe(x => {
      this.user = x;
    });
  }

}
