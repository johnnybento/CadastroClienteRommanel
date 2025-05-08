
using CadastroClienteRommanel.Core.DTOs;
using MediatR;

namespace CadastroClienteRommanel.Application.UseCases.ObterClientePorId;

public sealed record ObterClientePorIdQuery(Guid Id) : IRequest<ClienteDto?>;