import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export class CustomValidators {
  static MustMatch = (sourceCtrlName: string, targetCtrlName: string): ValidatorFn => {
    return (control: AbstractControl): ValidationErrors | null => {
      const sourceCtrl = control.get(sourceCtrlName);
      const targetCtrl = control.get(targetCtrlName);

      if (sourceCtrl && targetCtrl && sourceCtrl.value !== targetCtrl.value) {
        targetCtrl.setErrors({ mismatch: true });
        return { mismatch: true };
      }
      else {
        targetCtrl.setErrors(null);
      }

      return null;
    };
  };

  static NotWhiteSpaceString(control: AbstractControl) : ValidationErrors | null {
      const isWhiteSpaceOnly: boolean = (control.value || '').trim().length === 0;
      return isWhiteSpaceOnly ? { whitespace: true } : null;
    };
  };
