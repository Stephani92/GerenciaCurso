using AutoMapper;
using gerenciamentoCurso.Dominio.commands.matricula;
using gerenciamentoCurso.Dominio.Entidades;
using gerenciamentoCurso.Dominio.Interfaces;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;
using MediatR;

namespace gerenciamentoCurso.aplicacao.CasosDeUso.matricula
{
    public class CriarMatriculaHandler : IRequestHandler<CriarMatriculaCommand, Guid>
    {
        private readonly IMatriculaRepositorio _matriculaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CriarMatriculaHandler(IMatriculaRepositorio matriculaRepository, IUnitOfWork unitOfWork)
        {
            _matriculaRepository = matriculaRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CriarMatriculaCommand request, CancellationToken cancellationToken)
        {
            var matricula = new Matricula()
            {
                AlunoId = request.AlunoId,
                TurmaId = request.TurmaId,
                DataMatricula = DateTime.Now,
                Ativo = true
            };

            _matriculaRepository.Add(matricula);

            return await _unitOfWork.CommitAsync() ? matricula.Id : Guid.Empty;
        }
    }
}
