import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable()
export class CorsInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const apiUrl: string = "http://localhost:5010/api";
    const isApiUrl: boolean = req.url.startsWith(apiUrl);

    if (isApiUrl) {
      req.headers.set("Access-Control-Allow-Origin", `${apiUrl}`);
    }

    return next.handle(req);
  }
}