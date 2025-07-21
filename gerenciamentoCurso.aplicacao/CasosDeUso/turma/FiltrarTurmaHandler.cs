using AutoMapper;
using gerenciamentoCurso.aplicacao.Dtos.queries.turma;
using gerenciamentoCurso.Dominio.Entidades.solicitar;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;
using MediatR;

namespace gerenciamentoCurso.aplicacao.CasosDeUso.turma
{
    public class FiltrarTurmaHandler : IRequestHandler<FiltrarTurmaQuery, object>
    {
        private readonly ITurmaRepositorio _turmaRepository;

        public FiltrarTurmaHandler(ITurmaRepositorio turmaRepository)
        {
            _turmaRepository = turmaRepository;
        }

        public async Task<object> Handle(FiltrarTurmaQuery request, CancellationToken cancellationToken)
        {
            var turma = new FiltrarTurmaRequest()
            {
                Nome = request.Nome,
                DataInicio = request.DataInicio,
                DataFinal = request.DataFinal,
                Ativo = request.Ativo,
                Paginacao = request.Paginacao
            };
            return await _turmaRepository.FiltrarAsync(turma);
        }
    }
}
