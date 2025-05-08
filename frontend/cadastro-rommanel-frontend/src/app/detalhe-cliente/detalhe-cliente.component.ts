import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Cliente } from '../models/cliente.model';
import { ClienteService } from '../services/cliente.service';

@Component({
  selector: 'app-detalhe-cliente',
  templateUrl: './detalhe-cliente.component.html',
  styleUrls: ['./detalhe-cliente.component.scss'],
})
export class DetalheClienteComponent implements OnInit {
  cliente?: Cliente;
  loading = true;
  errorMsg: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private service: ClienteService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (!id) {
      this.errorMsg = 'ID de cliente não fornecido';
      this.loading = false;
      return;
    }

    this.service.obterPorId(id).subscribe({
      next: (c) => {
        this.cliente = c;
        this.loading = false;
      },
      error: (err) => {
        this.errorMsg =
          err.status === 404
            ? 'Cliente não encontrado'
            : 'Erro ao buscar cliente';
        this.loading = false;
      },
    });
  }

  voltar(): void {
    this.router.navigate(['/cadastro']);
  }
}
