using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using gerenciamentoCurso.Dominio.commands.matricula;
using gerenciamentoCurso.Dominio.Entidades;
using gerenciamentoCurso.Dominio.Interfaces;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;
using MediatR;

namespace gerenciamentoCurso.aplicacao.CasosDeUso.matricula
{
    public class CancelarMatriculaHandler : IRequestHandler<CancelarMatriculaCommand, bool>
    {
        private readonly IMatriculaRepositorio _matriculaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CancelarMatriculaHandler(IMatriculaRepositorio matriculaRepository, IUnitOfWork unitOfWork)
        {
            _matriculaRepository = matriculaRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CancelarMatriculaCommand request, CancellationToken cancellationToken)
        {

            await _matriculaRepository.CancelarMatricula(request.AlunoId, request.TurmaId);

            return await _unitOfWork.CommitAsync();
        }
    }
}
