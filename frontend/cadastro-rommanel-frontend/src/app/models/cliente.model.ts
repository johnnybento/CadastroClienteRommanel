export interface Cliente {
  id: string;
  cpfCnpj: string;
  email: string;
  telefone: string;
  cep: string;
  logradouro: string;
  numero: string;
  bairro: string;
  cidade: string;
  estado: string;
  tipo: 'PessoaFisica' | 'PessoaJuridica';
  ie: string;
  dataNascimento?: string;
  dataRegistro: string;
}
