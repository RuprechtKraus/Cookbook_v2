import { Component, OnInit } from '@angular/core';
import { AuthenticatedUserDto } from 'src/app/dtos/authenticated-user-dto';
import { AccountService } from 'src/app/services/account.service';
import { ModalWindowService } from '../../shared/modal-window/modal-window.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css'],
})
export class NavBarComponent implements OnInit {
  user?: AuthenticatedUserDto;
  
  constructor(
    private _modalService: ModalWindowService,
    private _accountService: AccountService
  ) {
    _accountService.user.subscribe(u => this.user = u);
  }

  ngOnInit(): void {}

  openModal(id: string) {
    this._modalService.open(id);
  }

  closeModal(id: string) {
    this._modalService.close(id);
  }

  logout(): void {
    this._accountService.logout();
  }
}
