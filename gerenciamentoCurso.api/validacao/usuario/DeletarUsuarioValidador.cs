using FluentValidation;
using gerenciamentoCurso.api.validacao.@base;
using gerenciamentoCurso.aplicacao.Dtos.commands.aluno;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;

namespace gerenciamentoCurso.api.validacao.usuario
{
    public class DeletarUsuarioValidador : BaseAbstractValidator<DeletarUsuarioCommand>
    {

        public DeletarUsuarioValidador(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            var service = serviceProvider.GetRequiredService<IAlunoRepositorio>();

            RuleFor(x => x.Cpf).CpfValido().WithMessage("O Cpf deve ser válido.");

            RuleFor(usuario => usuario.Email)
                .NotEmpty().WithMessage("O email não pode ser vazio.")
                .EmailAddress().WithMessage("O email deve ser válido.");

            RuleFor(usuario => usuario)
                .Must(usuario => service.AnyUsuarioAsyncByEmailCpf(usuario.Email, usuario.Cpf).Result).WithMessage("Usuario não encontrado");

        }
    }
}