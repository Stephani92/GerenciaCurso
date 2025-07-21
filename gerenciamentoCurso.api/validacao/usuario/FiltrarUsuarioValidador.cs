using FluentValidation;
using gerenciamentoCurso.api.validacao.@base;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;
using gerenciamentoCurso.Dominio.queries.usuario;

namespace gerenciamentoCurso.api.validacao.usuario
{
    public class FiltrarUsuarioValidador : BaseAbstractValidator<FiltrarUsuarioQuery>
    {
        public FiltrarUsuarioValidador(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            var service = serviceProvider.GetRequiredService<IAlunoRepositorio>();

            RuleFor(usuario => usuario.Nome)
                                .NotEmpty().WithMessage("O nome não pode ser vazio.")
                                .MinimumLength(3).WithMessage("O nome não pode ter menos de 3 caracteres.")
                                .MaximumLength(50).WithMessage("O nome não pode ter mais de 50 caracteres.");

            RuleFor(usuario => usuario.Sobrenome)
                                .NotEmpty().WithMessage("O sobrenome não pode ser vazio.")
                                .MinimumLength(3).WithMessage("O sobrenome não pode ter menos de 3 caracteres.")
                                .MaximumLength(50).WithMessage("O sobrenome não pode ter mais de 50 caracteres.");

            RuleFor(x => x.Cpf).CpfValido().WithMessage("O Cpf deve ser válido.");

        }
    }
}