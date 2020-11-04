/**
 * @license
 * Copyright Akveo. All Rights Reserved.
 * Licensed under the MIT License. See License.txt in the project root for license information.
 */
import { Component, OnInit } from '@angular/core';
import { UserService } from '../authentication/user.service';
import { AnalyticsService } from './@core/utils/analytics.service';

@Component({
  selector: 'ngx-app',
  template: '<router-outlet></router-outlet>',
})
export class AppComponent implements OnInit {

  username = '';
  constructor(private analytics: AnalyticsService, protected userService: UserService) {
    this.userService.loadUser();
    this.userService.user.subscribe(res => {
      this.username = res.UserDetail.FULL_NAME;
    })
  }

  ngOnInit() {
    this.analytics.trackPageViews();
  }
}
