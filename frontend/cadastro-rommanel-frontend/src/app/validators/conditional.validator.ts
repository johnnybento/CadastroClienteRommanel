import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function requiredIf(
  otherControlName: string,
  matchingValue: any
): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const form = control.parent;
    if (!form) {
      return null;
    }
    const otherValue = form.get(otherControlName)?.value;
    const isRequired = otherValue === matchingValue;
    if (isRequired && !control.value) {
      return {
        requiredIf: `Obrigatório quando ${otherControlName} for ${matchingValue}`,
      };
    }
    return null;
  };
}
