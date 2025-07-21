using AutoMapper;
using gerenciamentoCurso.aplicacao.Dtos.commands.aluno;
using gerenciamentoCurso.Dominio.Entidades.Auth;
using gerenciamentoCurso.Dominio.Entidades;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;
using gerenciamentoCurso.Dominio.Interfaces;
using MediatR;
using gerenciamentoCurso.aplicacao.Dtos.commands.turma;

namespace gerenciamentoCurso.aplicacao.CasosDeUso.turma
{
    public class CriarTurmaHandler : IRequestHandler<CriarTurmaCommand, Guid>
    {
        private readonly IAlunoRepositorio _alunoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CriarTurmaHandler(IAlunoRepositorio alunoRepository, IUnitOfWork unitOfWork)
        {
            _alunoRepository = alunoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CriarTurmaCommand request, CancellationToken cancellationToken)
        {
            var turma = new Turma()
            {
                Id = Guid.NewGuid(),
                Nome = request.Nome,
                DataCriacao = DateTime.Now,
                Descricao = request.Descricao,
                Ativo = true,
            };

            _alunoRepository.Add(turma);

            return await _unitOfWork.CommitAsync() ? turma.Id : Guid.Empty;
        }
    }
}
