import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  // troque o template inline padrão pelo nosso
  template: `
    <nav class="bg-gray-800 text-white p-4">
      <a routerLink="/cadastro" class="mr-4 hover:underline">Cadastro</a>
      <a routerLink="/clientes/SEU_ID" class="hover:underline">Detalhe</a>
    </nav>
    <main class="p-6">
      <router-outlet></router-outlet>
    </main>
  `,
  // se quiser, pode manter o styleUrls ou converter para inline styles
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {}
