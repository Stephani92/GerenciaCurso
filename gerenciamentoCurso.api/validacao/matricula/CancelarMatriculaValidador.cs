using FluentValidation;
using gerenciamentoCurso.api.validacao.@base;
using gerenciamentoCurso.Dominio.commands.matricula;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;

namespace gerenciamentoCurso.api.validacao.matricula
{
    public class CancelarMatriculaValidador : BaseAbstractValidator<CancelarMatriculaCommand>
    {

        public CancelarMatriculaValidador(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            var serviceMatricula = serviceProvider.GetRequiredService<IMatriculaRepositorio>();

            RuleFor(matricula => matricula.TurmaId)
                .NotEmpty().WithMessage("O Id do aluno não pode ser vazio.");

            RuleFor(matricula => matricula.AlunoId)
                .NotEmpty().WithMessage("O Id do aluno não pode ser vazio.");

            RuleFor(matricula => matricula)
                .Must(matricula => serviceMatricula.AnyMatriculaAsyncByAlunoIdETurmaId(matricula.AlunoId, matricula.TurmaId).Result)
                                                                                                .WithMessage("Aluno não encontrado");
        }
    }
}
