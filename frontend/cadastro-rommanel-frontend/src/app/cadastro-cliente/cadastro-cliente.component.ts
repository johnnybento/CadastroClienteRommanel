import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ClienteService } from '../services/cliente.service';
import { cpfCnpjValidator } from '../validators/cpf-cnpj.validator';

@Component({
  selector: 'app-cadastro-cliente',
  templateUrl: './cadastro-cliente.component.html',
  styleUrls: ['./cadastro-cliente.component.scss'],
})
export class CadastroClienteComponent implements OnInit {
  form!: FormGroup;
  submitting = false;

  constructor(
    private fb: FormBuilder,
    private clienteService: ClienteService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      cpfCnpj: ['', [Validators.required, cpfCnpjValidator()]],
      email: ['', [Validators.required, Validators.email]],
      telefone: [
        '',
        [Validators.required, Validators.pattern(/^\+?\d{8,15}$/)],
      ],
      cep: ['', [Validators.required, Validators.pattern(/^\d{5}-?\d{3}$/)]],
      logradouro: ['', Validators.required],
      numero: ['', Validators.required],
      bairro: ['', Validators.required],
      cidade: ['', Validators.required],
      estado: ['', [Validators.required, Validators.pattern(/^[A-Za-z]{2}$/)]],
      tipo: ['PessoaFisica', Validators.required],
      ie: [''],
      dataNascimento: [''],
    });

    this.form.get('tipo')!.valueChanges.subscribe((tipo: string) => {
      const ieCtrl = this.form.get('ie')!;
      const dtCtrl = this.form.get('dataNascimento')!;

      if (tipo === 'PessoaJuridica') {
        ieCtrl.setValidators([Validators.required]);
        dtCtrl.clearValidators();
        dtCtrl.setValue(''); // limpa o valor anterior
      } else {
        dtCtrl.setValidators([Validators.required]);
        ieCtrl.clearValidators();
        ieCtrl.setValue(''); // limpa o valor anterior
      }

      ieCtrl.updateValueAndValidity();
      dtCtrl.updateValueAndValidity();
    });

    // Força validação inicial correta
    this.form.get('tipo')!.updateValueAndValidity({ onlySelf: true });
  }

  submit(): void {
    if (this.form.invalid || this.submitting) {
      this.form.markAllAsTouched();
      return;
    }

    if (this.form.value.tipo === 'PessoaFisica') {
      const nas = new Date(this.form.value.dataNascimento);
      const idade = Math.floor(
        (Date.now() - nas.getTime()) / (365.25 * 24 * 60 * 60 * 1000)
      );
      if (idade < 18) {
        this.form
          .get('dataNascimento')!
          .setErrors({ underage: 'Deve ter no mínimo 18 anos' });
        return;
      }
    }

    const dto: any = {
      cpfCnpj: this.form.value.cpfCnpj.replace(/\D/g, ''),
      email: this.form.value.email,
      telefone: this.form.value.telefone,
      cep: this.form.value.cep.replace(/\D/g, ''),
      logradouro: this.form.value.logradouro,
      numero: this.form.value.numero,
      bairro: this.form.value.bairro,
      cidade: this.form.value.cidade,
      estado: this.form.value.estado.toUpperCase(),
      tipo: this.form.value.tipo,
      dataRegistro: new Date().toISOString(),
      dataNascimento:
        this.form.value.tipo === 'PessoaFisica'
          ? this.form.value.dataNascimento
          : null,
      ie:
        this.form.value.tipo === 'PessoaJuridica'
          ? this.form.value.ie || 'isento'
          : null,
    };
    const payload = { cmd: dto };

    this.submitting = true;
    this.clienteService.registrar(dto).subscribe({
      next: (res) => {
        alert(`Cliente cadastrado com ID ${res.id}`);
        this.form.reset({
          tipo: 'PessoaFisica',
          ie: '',
          dataNascimento: '',
        });
        this.router.navigate(['/clientes', res.id]);
        this.submitting = false;
      },
      error: (err) => {
        alert(err.error?.message || 'Erro ao cadastrar');
        this.submitting = false;
      },
    });
  }
}
