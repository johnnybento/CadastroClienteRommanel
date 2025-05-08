import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module'; // ← import
import { AppComponent } from './app.component';
import { CadastroClienteComponent } from './cadastro-cliente/cadastro-cliente.component';
import { DetalheClienteComponent } from './detalhe-cliente/detalhe-cliente.component';
@NgModule({
  declarations: [
    AppComponent,
    CadastroClienteComponent,
    DetalheClienteComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
