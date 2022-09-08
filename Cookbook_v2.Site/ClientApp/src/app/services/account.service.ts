import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../interfaces/user';
import { UserLoginDTO } from '../dtos/user-login-dto';
import { UserRegisterDTO } from '../dtos/user-register-dto';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  private _userSubject: BehaviorSubject<UserLoginDTO>;
  public user: Observable<UserLoginDTO>;

  constructor(
    private _router: Router, 
    private _http: HttpClient) {
      this._userSubject = new BehaviorSubject<UserLoginDTO>(JSON.parse(localStorage.getItem("user")));
      this.user = this._userSubject.asObservable();
  }

  public get userValue(): UserLoginDTO {
    return this._userSubject.value;
  }

  login(login: string, password: string): Observable<UserLoginDTO> {
    return this._http.post<UserLoginDTO>("api/user/authenticate", { login, password })
      .pipe(map(user => {
        localStorage.setItem("user", JSON.stringify(user));
        this._userSubject.next(user);
        this._router.navigate(["/"]).then(() => window.location.reload());
        return user;
      }))
  }

  logout(): void {
    localStorage.removeItem("user");
    this._userSubject.next(null);
    this._router.navigate(["/"]).then(() => window.location.reload());
  }

  register(userDTO: UserRegisterDTO): Observable<any> {
    return this._http.post("api/user/register", userDTO);
  }

  getByID(id: number): Observable<User> {
    return this._http.get<User>(`api/user/${id}`);
  }
}
