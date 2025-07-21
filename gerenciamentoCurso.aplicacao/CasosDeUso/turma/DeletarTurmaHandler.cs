using gerenciamentoCurso.aplicacao.Dtos.commands.turma;
using gerenciamentoCurso.Dominio.Entidades;
using gerenciamentoCurso.Dominio.Interfaces;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;
using MediatR;

namespace gerenciamentoCurso.aplicacao.CasosDeUso.turma
{
    public class DeletarTurmaHandler : IRequestHandler<DeletarTurmaCommand, bool>
    {
        private readonly ITurmaRepositorio _turmaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeletarTurmaHandler(ITurmaRepositorio turmaRepository, IUnitOfWork unitOfWork)
        {
            _turmaRepository = turmaRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeletarTurmaCommand request, CancellationToken cancellationToken)
        {
            var turma = await _turmaRepository.GetAsyncById<Turma>(request.Id);
            turma.Ativo = false;

            _turmaRepository.Atualizar(turma);

            return await _unitOfWork.CommitAsync();
        }
    }
}
