import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { environment } from 'src/environments/environment';
import { TokenService } from '../_services/token.service';


@Injectable({providedIn: 'root'})
export class CoreApi{
    /**
     *
     */
    constructor(private http: HttpClient, private tokenService: TokenService) {
    }

    fetchUser() {
        return this.http.get(this.getLink('user/UserDateil', {
            userId: this.tokenService.token.userId,
            facilityId: this.tokenService.token.facilityId
        }), {headers: this.headers()});
    }

    private coreUrl = (path: string) =>environment.baseUrl + environment.coreApiPath + '/' + path;


    private headers(){
        return new HttpHeaders({
            Authorization: 'Bearer ' + this.tokenService.token.token
        });
    }

    private getLink(path: string, params: object){
        let str='';
        for(const key in params){
            if(Object.keys(params).indexOf(key)>-1){
                if(str !== ''){
                    str += '&';
                }
                str += key + '=' + encodeURIComponent(params[key]);
            }
        }
        return this.coreUrl(path) + '?' + str;
    }
}