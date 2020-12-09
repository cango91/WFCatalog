import { Injectable } from "@angular/core";
import { JwtHelperService } from '@auth0/angular-jwt';
import { CookieService } from 'ngx-cookie-service/';
import { CerebralToken } from '../_models/cerebral-token.model';

export const TOKEN_KEY = 'api_token'
export const helper = new JwtHelperService();

@Injectable({providedIn:'root'})
export class TokenService {
    /**
     *
     */
    constructor(private cookieService: CookieService) {
    }

    get token(): CerebralToken{
        if(this.cookieService.check(TOKEN_KEY)){
            const _token = this.cookieService.get(TOKEN_KEY);
            const decoded_token = helper.decodeToken(_token);
            return {
                token: _token,
                userId: decoded_token.userId,
                facilityId: decoded_token.facilityId,
                langId: decoded_token.langId
            };
        }
        return null;
    }

    isAvailable = () => this.cookieService.check(TOKEN_KEY);
}
