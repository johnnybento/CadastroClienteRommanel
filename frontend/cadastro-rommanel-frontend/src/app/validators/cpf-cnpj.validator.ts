import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function cpfCnpjValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const raw = (control.value || '').toString().replace(/\D/g, '');
    if (!raw) {
      return { cpfCnpj: 'Obrigatório' };
    }
    // CPF: 11 dígitos
    if (raw.length === 11) {
      const invalidos = [
        '00000000000',
        '11111111111',
        '22222222222',
        '33333333333',
        '44444444444',
        '55555555555',
        '66666666666',
        '77777777777',
        '88888888888',
        '99999999999',
      ];
      if (invalidos.includes(raw)) {
        return { cpfCnpj: 'CPF inválido' };
      }
      // (aqui você pode colocar algoritmo completo de dígitos verificadores)
      return null;
    }
    // CNPJ: 14 dígitos
    if (raw.length === 14) {
      // (você pode expandir com validação de dígitos verificadores)
      return null;
    }
    return { cpfCnpj: 'Deve ter 11 (CPF) ou 14 (CNPJ) dígitos' };
  };
}
