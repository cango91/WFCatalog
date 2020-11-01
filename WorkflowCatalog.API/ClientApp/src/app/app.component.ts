import { Component } from '@angular/core';
import { UserService } from './authentication/user.service';
import { SetupsClient, SetupsDto } from './web-api-client';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Catelog';

  username = '';
  setups: Array<SetupsDto> = [];

  /**
   *
   */
  constructor(protected userService: UserService, protected setupsClient: SetupsClient) {
    this.userService.loadUser();
    this.userService.user.subscribe(res => {
      this.username = res.UserDetail.FULL_NAME;

      this.setupsClient.get(null, null).subscribe(data => {
        this.setups = data.setups;
      })

    })
  }
}
