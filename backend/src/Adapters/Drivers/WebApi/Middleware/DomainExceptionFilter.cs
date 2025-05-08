using CadastroClienteRommanel.Core.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CadastroClienteRommanel.Adapters.Drivers.WebApi.Middleware;

public class DomainExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is DomainException dex)
        {
            context.Result = new BadRequestObjectResult(new { message = dex.Message });
            context.ExceptionHandled = true;
        }
    }
}