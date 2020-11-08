import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { User} from '../_models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  user: Subject<User> = new Subject<User>();
  

  constructor() { }
}
