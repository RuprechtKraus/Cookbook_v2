import { Injectable } from "@angular/core";
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from "@angular/common/http";
import { Observable } from "rxjs";
import { AccountService } from "../services/account.service";
import { AuthenticatedUserDto } from "../dtos/authenticated-user-dto";

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private _accountService: AccountService) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const apiUrl: string = "http://localhost:5010/api";
    const user: AuthenticatedUserDto = this._accountService.userValue;
    const isLoggedIn: boolean = user != null && user.token != null;
    const isApiUrl: boolean = req.url.startsWith(apiUrl);

    if (isLoggedIn && isApiUrl) {
      req = req.clone({
        setHeaders: {
          Authorization: `Bearer ${user.token}`
        }
      });
    }

    return next.handle(req);
  }
}
