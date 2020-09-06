using FluentValidation.Results;
using MediatR;
using NSE.Cliente.API.Application.Events;
using NSE.Cliente.API.Models;
using NSE.Core.Messages;
using System.Threading;
using System.Threading.Tasks;

namespace NSE.Cliente.API.Application.Commands
{
    public class ClienteCommandHandler : CommandHandler, IRequestHandler<RegistrarClientCommand, ValidationResult>
    {
        private readonly IClientRepository _repository;

        public ClienteCommandHandler(IClientRepository repository)
        {
            _repository = repository;
        }
        public async  Task<ValidationResult> Handle(RegistrarClientCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var cliente = new Models.Cliente(message.Id,message.Nome,message.Email,message.Cpf);

            var clienteExistente = await _repository.ObterPorCpf(cliente.Cpf.Numero);

            if (clienteExistente != null)
            {
                AdicionarErro("Este CPF já está em uso.");
                return ValidationResult;
            }
            _repository.Adicionar(cliente);


            cliente.AdicionarEvento(new ClienteRegistradoEvent(message.Id, message.Nome, message.Email, message.Cpf));
            return await PersistirDados(_repository.UnitOfWork);
        }
    }
}
