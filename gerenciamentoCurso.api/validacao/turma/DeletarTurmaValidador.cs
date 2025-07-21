using FluentValidation;
using gerenciamentoCurso.api.validacao.@base;
using gerenciamentoCurso.aplicacao.Dtos.commands.turma;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;

namespace gerenciamentoCurso.api.validacao
{
    public class DeletarTurmaValidador : BaseAbstractValidator<DeletarTurmaCommand>
    {

        public DeletarTurmaValidador(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            var service = serviceProvider.GetRequiredService<ITurmaRepositorio>();

            RuleFor(usuario => usuario.Id)
                .NotEmpty().WithMessage("O Id não pode ser vazio.")
                .Must(id => service.AnyTurmaAsyncById(id).Result).WithMessage("Turma não encontrado");
        }
    }
}