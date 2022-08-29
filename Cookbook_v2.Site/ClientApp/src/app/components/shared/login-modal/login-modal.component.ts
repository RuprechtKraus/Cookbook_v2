import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AccountService } from 'src/app/services/account.service';
import { ModalWindowComponent } from '../modal-window/modal-window.component';
import { ModalWindowService } from '../modal-window/modal-window.service';

@Component({
  selector: 'app-login-modal',
  templateUrl: './login-modal.component.html',
  styleUrls: ['./login-modal.component.css'],
})
export class LoginModalComponent implements OnInit {
  @ViewChild(ModalWindowComponent) modal: ModalWindowComponent;

  loginForm: FormGroup;
  loading = false;
  submitted = false;

  constructor(
    private _formBuilder: FormBuilder,
    private _accountService: AccountService,
    private _modalService: ModalWindowService
  ) {}

  ngOnInit(): void {
    this.loginForm = this._formBuilder.group({
      login: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  public get FormControls() {
    return this.loginForm.controls;
  }

  onSubmit(): void {
    this.submitted = true;
    
    if (this.loginForm.invalid) {
      return;
    }
    
    this.loading = true;
    this._accountService
      .login(this.FormControls.login.value, this.FormControls.password.value)
      .pipe(first())
      .subscribe(
        (data) => {
          this.close();
        },
        (badRequest) => {
          this.resetFormStatus();
          alert(badRequest.error.message);
        }
      );
  }

  close(): void {
    this.modal.close();
    this.clearForm();
  }

  clearForm(): void {
    this.resetFormStatus();
    this.loginForm.reset();
  }

  private resetFormStatus(): void {
    this.loading = false;
    this.submitted = false;
  }

  openRegistrationModal(): void {
    this.close();
    this._modalService.open("registration-modal");
  }
}
