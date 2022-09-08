import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';

import { AccountService } from '../services/account.service';

@Injectable({providedIn: "root"})
export class AuthGuard implements CanActivate {
    constructor(
        private _router: Router,
        private _accountService: AccountService
    ) {}

    canActivate(): boolean {
        const user = this._accountService.userValue;

        if (user) {
            return true;
        }

        this._router.navigate(["/"]);
        return false;
    }
}