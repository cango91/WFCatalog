import { CookieService } from 'ngx-cookie-service';
import { CerebralToken } from './cerebral-token.model';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Injectable } from '@angular/core';

const TOKEN_KEY = 'access_token';
const helper = new JwtHelperService();

@Injectable({ providedIn: 'root' })
export class TokenService {
    constructor(private cookieService: CookieService) {

    }

    get token(): CerebralToken {
        if (this.cookieService.check(TOKEN_KEY)) {
            const _token = this.cookieService.get(TOKEN_KEY);
            const decodedToken = helper.decodeToken(_token);
            return {
                token: _token,
                userId: decodedToken.userId,
                facilityId: decodedToken.facilityId,
                langId: decodedToken.langId
            }
        }
        return null;
    }

    isAvailable = () => this.cookieService.check(TOKEN_KEY);
}