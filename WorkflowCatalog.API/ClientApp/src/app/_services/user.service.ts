import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { User} from '../_models/user.model';
import { CoreApi } from '../_providers/core-api.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  user: Subject<User> = new Subject<User>();
  

  constructor(private coreApi: CoreApi) { }

  loadUser(){
    this.coreApi.fetchUser().subscribe((res:any) => {
      this.user.next(res);
    }, err => {
      window.location.href = '/Account/Login';
    })
  }
}
