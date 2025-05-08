import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CadastroClienteComponent } from './cadastro-cliente/cadastro-cliente.component';
import { DetalheClienteComponent } from './detalhe-cliente/detalhe-cliente.component';

const routes: Routes = [
  { path: 'cadastro', component: CadastroClienteComponent },
  { path: 'clientes/:id', component: DetalheClienteComponent },
  { path: '', redirectTo: 'cadastro', pathMatch: 'full' },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      useHash: true,
    }),
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}
