using gerenciamentoCurso.aplicacao.Dtos.commands.aluno;
using gerenciamentoCurso.Dominio.Entidades;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;
using gerenciamentoCurso.Dominio.Interfaces;
using MediatR;
using gerenciamentoCurso.aplicacao.Dtos.commands.turma;
using AutoMapper;

namespace gerenciamentoCurso.aplicacao.CasosDeUso.turma
{
    public class AtualizarTurmaHandler : IRequestHandler<AtualizarTurmaCommand, bool>
    {
        private readonly ITurmaRepositorio _turmaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AtualizarTurmaHandler(ITurmaRepositorio turmaRepository, IUnitOfWork unitOfWork)
        {
            _turmaRepository = turmaRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(AtualizarTurmaCommand request, CancellationToken cancellationToken)
        {
            var turma = await _turmaRepository.GetTurmaAsyncById(request.Id);

            turma.Nome = request.Nome;
            turma.Descricao = request.Descricao;

            _turmaRepository.Atualizar(turma);
            return await _unitOfWork.CommitAsync();
        }
    }
}

