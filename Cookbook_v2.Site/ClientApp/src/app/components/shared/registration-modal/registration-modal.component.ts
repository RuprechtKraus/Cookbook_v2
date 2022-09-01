import { Component, OnInit, ViewChild } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { AccountService } from "src/app/services/account.service";
import { ModalWindowComponent } from "../modal-window/modal-window.component";
import { ModalWindowService } from "../modal-window/modal-window.service";
import { CustomValidators } from "src/app/helpers/validators";
import { first } from "rxjs/operators";

@Component({
  selector: "app-registration-modal",
  templateUrl: "./registration-modal.component.html",
  styleUrls: ["./registration-modal.component.css"],
})
export class RegistrationModalComponent implements OnInit {
  @ViewChild(ModalWindowComponent) modal: ModalWindowComponent;

  registrationForm: FormGroup;
  loading = false;
  submitted = false;

  constructor(
    private _formBuilder: FormBuilder,
    private _accountService: AccountService,
    private _modalService: ModalWindowService
  ) {}

  ngOnInit(): void {
    this.registrationForm = this._formBuilder.group(
      {
        name: ["", Validators.required],
        username: ["", Validators.required],
        password: ["", [Validators.required, Validators.minLength(8)]],
        repeatPassword: ["", Validators.required],
      },
      { validators: CustomValidators.MustMatch("password", "repeatPassword") }
    );
  }

  public get FormControls() {
    return this.registrationForm.controls;
  }

  onSubmit(): void {
    this.submitted = true;

    if (this.registrationForm.invalid) {
      return;
    }

    this.loading = true;
    this._accountService
      .register(this.registrationForm.value)
      .pipe(first())
      .subscribe(
        () => {
          alert("Регистрация успешна. Теперь вы можете войти");
          this.close();
        },
        (badRequest) => {
          this.resetFormStatus();
          alert(badRequest.error.Message);
        }
      );
  }

  close(): void {
    this.modal.close();
    this.clearForm();
  }

  clearForm(): void {
    this.resetFormStatus();
    this.registrationForm.reset();
  }

  private resetFormStatus(): void {
    this.loading = false;
    this.submitted = false;
  }

  openLoginModal(): void {
    this.close();
    this._modalService.open("login-modal");
  }
}
