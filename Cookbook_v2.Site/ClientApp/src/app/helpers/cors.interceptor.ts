import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable()
export class CorsInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const isApiUrl = req.url.startsWith("http://localhost:5010/api");

    if (isApiUrl) {
      req.headers.set("Access-Control-Allow-Origin", "http://localhost:5010/api");
    }

    return next.handle(req);
  }
}