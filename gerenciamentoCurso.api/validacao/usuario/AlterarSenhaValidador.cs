using FluentValidation;
using gerenciamentoCurso.api.validacao.@base;
using gerenciamentoCurso.aplicacao.Dtos.commands.usuario;
using gerenciamentoCurso.Dominio.commands.usuario;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;

namespace gerenciamentoCurso.api.validacao.usuario
{
    public class AlterarSenhaValidador : BaseAbstractValidator<AlterarSenhaCommand>
    {

        public AlterarSenhaValidador(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            var service = serviceProvider.GetRequiredService<IUsuarioRepositorio>();

            RuleFor(usuario => usuario.Senha)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(8).WithMessage("A senha deve ter no mínimo 8 caracteres.")
                .Matches("[A-Z]").WithMessage("A senha deve conter ao menos uma letra maiúscula.")
                .Matches("[a-z]").WithMessage("A senha deve conter ao menos uma letra minúscula.")
                .Matches("[0-9]").WithMessage("A senha deve conter ao menos um número.")
                .Matches("[^a-zA-Z0-9]").WithMessage("A senha deve conter ao menos um caractere especial.");

            RuleFor(x => x.Cpf).CpfValido().WithMessage("O Cpf deve ser válido.");

            RuleFor(usuario => usuario.Email).NotEmpty().WithMessage("O email não pode ser vazio.")
                                             .EmailAddress().WithMessage("O email deve ser válido.");

            RuleFor(usuario => usuario)
                .Must(usuario => service.AnyUsuarioAsyncByEmailCpf(usuario.Email, usuario.Cpf).Result).WithMessage("Registro não encontrado");

        }
    }
}