import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';
import { environment } from 'src/environments/environment.prod';
import { User} from '../_models/user.model';
import { TokenService } from './token.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  user: BehaviorSubject<User> = new BehaviorSubject<User>(new User());
  

  constructor(private http:HttpClient,protected tokenService:TokenService) { }

  loadUser(){
    this.http.post(environment.userApiUrl,{},{headers: new HttpHeaders({
      Authorization: 'Bearer ' + this.tokenService.token.token}) }).subscribe((res:any) => {
      this.user.next(res.returnValue);
    }, err => {
      console.log("Could not get user details from external Api, redirecting to login");
      window.location.href = environment.loginUrl;
    })
  }
}
