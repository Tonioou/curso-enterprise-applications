using Microsoft.AspNetCore.Mvc;
using NSE.Cliente.API.Application.Commands;
using NSE.Core.Mediator;
using NSE.WebAPI.Core.Controllers;
using System;
using System.Threading.Tasks;

namespace NSE.Cliente.API.Controllers
{
    public class ClientesController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;
        public ClientesController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpGet("clientes")]
        public async  Task<IActionResult> Index()
        {
            var resultado = await _mediatorHandler.EnviarComando(
                new RegistrarClientCommand(Guid.NewGuid(), "Antonio", "antoniomarcos997@gmail.com", "717.525.800-36"));

            
            return CustomResponse(resultado);
        }
    }
}
