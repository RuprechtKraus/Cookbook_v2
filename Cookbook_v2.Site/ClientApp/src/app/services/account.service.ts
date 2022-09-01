import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { BehaviorSubject, Observable } from "rxjs";
import { map } from "rxjs/operators";
import { RegisterUserCommand } from "../commands/register-user-command";
import { AuthenticatedUserDto } from "../dtos/authenticated-user-dto";
import { UserDetailsDto } from "../interfaces/user";

@Injectable({
  providedIn: "root",
})
export class AccountService {
  private readonly apiUrl: string = "http://localhost:5010/api";
  private _userSubject: BehaviorSubject<AuthenticatedUserDto>;
  public user: Observable<AuthenticatedUserDto>;

  constructor(private _router: Router, private _http: HttpClient) {
    this._userSubject = new BehaviorSubject<AuthenticatedUserDto>(
      JSON.parse(localStorage.getItem("user"))
    );
    this.user = this._userSubject.asObservable();
  }

  public get userValue(): AuthenticatedUserDto {
    return this._userSubject.value;
  }

  login(username: string, password: string): Observable<AuthenticatedUserDto> {
    return this._http
      .post<AuthenticatedUserDto>(`${this.apiUrl}/user/authenticate`, {
        username,
        password,
      })
      .pipe(
        map((user) => {
          localStorage.setItem("user", JSON.stringify(user));
          this._userSubject.next(user);
          this._router.navigate(["/"]).then(() => window.location.reload());
          return user;
        })
      );
  }

  logout(): void {
    localStorage.removeItem("user");
    this._userSubject.next(null);
    this._router.navigate(["/"]).then(() => window.location.reload());
  }

  register(registerCommand: RegisterUserCommand): Observable<any> {
    return this._http.post(`${this.apiUrl}/user/register`, registerCommand);
  }

  getByID(id: number): Observable<UserDetailsDto> {
    return this._http.get<UserDetailsDto>(`${this.apiUrl}/user/details/${id}`);
  }
}
