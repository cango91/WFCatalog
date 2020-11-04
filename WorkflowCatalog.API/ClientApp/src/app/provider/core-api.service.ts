import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TokenService } from '../../authentication/token/token.service';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class CoreApi {
    constructor(private http: HttpClient, private tokenService: TokenService) {

    }

    fetchUser() {
        return this.http
            .get(this.getLink('user/UserDetail', {
                userId: this.tokenService.token.userId,
                facilityId: this.tokenService.token.facilityId
            }), { headers: this.headers() });
    }

    fetchActivities() {
        return this.http
            .post(this.coreUrl('activity/list'), {
                userId: this.tokenService.token.userId,
                facilityId: this.tokenService.token.facilityId,
                langId: this.tokenService.token.langId
            }, { headers: this.headers() });
    }

    fetchNotifications() {
        return this.http.post(this.coreUrl('notification/list'), {
            userId: this.tokenService.token.userId,
            facilityId: this.tokenService.token.facilityId,
            languageId: this.tokenService.token.langId,
            OrderBy: null,
            PageIndex: 1,
            PageSize: 20
        })
    }

    private coreUrl = (path: string) => environment.baseUrl + environment.coreApiPath + '/' + path;

    private headers() {
        return new HttpHeaders({
            Authorization: 'Bearer ' + this.tokenService.token.token
        });
    }

    private getLink(path: string, params: object) {
        let str = '';
        for (const key in params) {
            if (Object.keys(params).indexOf(key) > -1) {
                if (str !== '') {
                    str += '&';
                }
                str += key + '=' + encodeURIComponent(params[key]);
            }
        }
        return this.coreUrl(path) + '?' + str
    }
}