using FluentValidation;
using gerenciamentoCurso.api.validacao.@base;
using gerenciamentoCurso.Dominio.commands.matricula;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;

namespace gerenciamentoCurso.api.validacao.matricula
{
    public class CriarMatriculaValidador : BaseAbstractValidator<CriarMatriculaCommand>
    {

        public CriarMatriculaValidador(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            var serviceTurma = serviceProvider.GetRequiredService<ITurmaRepositorio>();
            var serviceAluno = serviceProvider.GetRequiredService<IAlunoRepositorio>();

            RuleFor(usuario => usuario.TurmaId)
                .Must(id => serviceTurma.AnyTurmaAsyncById(id).Result).WithMessage("Turma não encontrado");

            RuleFor(usuario => usuario.AlunoId)
                .NotEmpty().WithMessage("O Id do aluno não pode ser vazio.")
                .Must(email => serviceAluno.AnyAlunoAsyncById(email).Result).WithMessage("Aluno não encontrado")
                .Must(email => !serviceTurma.AnyAlunoTurmaAsyncById(email).Result).WithMessage("Aluno Matriculado");

        }
    }
}
