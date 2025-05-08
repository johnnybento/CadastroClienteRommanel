# Projeto CadastroRommanel

## Sobre

O CadastroRommanel é uma aplicação full-stack para gerenciar o cadastro de clientes (Pessoa Física e Jurídica), construída com .NET 8 no backend e Angular 16 + Tailwind CSS no frontend. O foco foi aplicar boas práticas de Clean Architecture, DDD, CQRS/MediatR e testes automatizados.

---

## Funcionalidades

- Cadastro de Cliente  
  - Pessoa Física:  
    - Campos: CPF, nome, e-mail, telefone, endereço completo e data de nascimento.  
    - Valida idade mínima de 18 anos.  
  - Pessoa Jurídica:  
    - Campos: CNPJ, razão social, IE ou indicação de “isento”, e-mail, telefone e endereço.  
- Regras de unicidade  
  - Um único cliente por CPF/CNPJ e por e-mail.  
- Validações  
  - Frontend (Angular Reactive Forms + Tailwind): máscaras, validadores de campo e feedback visual.  
  - Backend (FluentValidation + exceções de domínio): regras de negócio e consistência via DDD.  

---

## Arquitetura e Padrões

- Clean Architecture  
  - Camadas: Core, Application, Adapters (Drivers e Driven).  
- Domain-Driven Design (DDD)  
  - Entidades, Value Objects (CpfCnpj, Email, Telefone, Endereco) e Exceptions para erros de domínio.  
- CQRS + MediatR  
  - Commands e Handlers separados para operações de escrita e leitura (ainda usando um único banco de dados).  
- Event Sourcing (Preparado)  
  - Estrutura preparada para eventos, facilitando futura evolução para múltiplas fontes de verdade.

---

## Infraestrutura

- Docker Compose  
  - Containers para SQL Server, WebAPI (.NET), Frontend (Angular) e ambiente de testes.  
- Dockerfile  
  - Backend: build e publish da API .NET 8.  
  - Frontend: build do Angular e publicação via NGINX com fallback de SPA.

---

## Testes e CI/CD

- Testes Unitários  
  - Backend: xUnit + Bogus para geração de dados falsos (padrão AAA).  
  - Frontend: Jasmine/Karma.  
- Pipelines Azure DevOps  
  - Build, execução de testes e deploy automático ao branch principal.

---

## Documentação

- README.md (este arquivo) explicando escopo, arquitetura e uso.  

---

## Como Executar

1. Clone este repositório:  
   ```bash
   git clone https://seu-repositorio.git
   cd CadastroRommanel