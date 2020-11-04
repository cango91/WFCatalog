import { Injectable } from '@angular/core';
import { Observable, Observer, Subject, Subscription } from 'rxjs';
import { CoreApi } from '../app/provider/core-api.service';

@Injectable({ providedIn: 'root' })
export class UserService {
    user: Subject<any> = new Subject();
    authList: Array<string> = [];

    constructor(private coreApi: CoreApi) {

    }

    loadUser() {
        this.coreApi.fetchUser().subscribe((res: any) => {
            this.authList = res.AuthList.map(a => a.PROPERTY_CODE);
            this.user.next(res);
        }, err => {
            window.location.href = '/Account/Login'
        })
    }

    activities() {
        return this.coreApi.fetchActivities();
    }

    notifications() {
        return this.coreApi.fetchNotifications();
    }

    hasPermission(authCode: string) {
        return this.authList.indexOf(authCode) > -1;
    }
}