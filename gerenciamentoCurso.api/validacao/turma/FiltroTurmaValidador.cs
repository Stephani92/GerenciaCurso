using FluentValidation;
using gerenciamentoCurso.api.validacao.@base;
using gerenciamentoCurso.aplicacao.Dtos.queries.aluno;
using gerenciamentoCurso.aplicacao.Dtos.queries.turma;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;

namespace gerenciamentoCurso.api.validacao.turma
{
    public class FiltroTurmaValidador : BaseAbstractValidator<FiltrarTurmaQuery>
    {
        public FiltroTurmaValidador(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
