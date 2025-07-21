using AutoMapper;
using gerenciamentoCurso.aplicacao.Dtos.queries.aluno;
using gerenciamentoCurso.Dominio.Entidades.Resposta;
using gerenciamentoCurso.Dominio.Entidades.solicitar;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;
using MediatR;

namespace gerenciamentoCurso.aplicacao.CasosDeUso.aluno
{
    public class FiltraAlunoHandler : IRequestHandler<FiltrarAlunoQuery, AlunosResponse>
    {
        private readonly IAlunoRepositorio _alunoRepository;

        public FiltraAlunoHandler(IAlunoRepositorio alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        public async Task<AlunosResponse> Handle(FiltrarAlunoQuery request, CancellationToken cancellationToken)
        {
            var alunoFiltro = new FiltrarAlunoRequest()
            {
                Nome = request.Nome,
                Sobrenome = request.Sobrenome,
                Cpf = request.Cpf,
                Ativo = request.Ativo,
                Paginacao = request.Paginacao
            };
            var aluno = await _alunoRepository.FiltrarAsync(alunoFiltro);

            return aluno;
        }
    }
}
