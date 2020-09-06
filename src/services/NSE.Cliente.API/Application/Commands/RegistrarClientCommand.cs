using FluentValidation;
using NSE.Core.Messages;
using System;

namespace NSE.Cliente.API.Application.Commands
{
    public class RegistrarClientCommand : Command
    {
        public RegistrarClientCommand(Guid id, string nome, string email, string cpf)
        {
            AggregateId = id;
            Id = id;
            Nome = nome;
            Email = email;
            Cpf = cpf;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new RegistrarClienteValidator().Validate(this);
            return ValidationResult.IsValid;
        }

    }

    public class RegistrarClienteValidator : AbstractValidator<RegistrarClientCommand>
    {
        public RegistrarClienteValidator()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");
            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage("O nome do cliente não foi informado");

            RuleFor(c => c.Cpf)
                .Must(TerCpfValido)
                .WithMessage("O CPF informado não é valido");

            RuleFor(c => c.Email)
                .Must(TerEmailValido)
                .WithMessage("O Email informado não é valido");
        }

        protected static bool TerCpfValido( string cpf)
        {
            return Core.DomainObjects.Cpf.Validar(cpf);
        }

        protected static bool TerEmailValido(string email)
        {
            return Core.DomainObjects.Email.Validar(email);
        }
    }

}
