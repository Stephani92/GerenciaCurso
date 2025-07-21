
using FluentValidation;
using gerenciamentoCurso.api.validacao.@base;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;

namespace gerenciamentoCurso.validacao
{
    public class TurmaValidator : BaseAbstractValidator<string>
    {
       
        public TurmaValidator(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            var service = serviceProvider.GetRequiredService<IAlunoRepositorio>();

            RuleFor(usuario => usuario)
                .NotEmpty().WithMessage("O nome da turma não pode ser vazio.")
                .MinimumLength(3).WithMessage("O nome da turma deve ter pelo menos 3 caracteres.")
                .MaximumLength(50).WithMessage("O nome da turma não pode ter mais de 50 caracteres.");
        }
    }
}
