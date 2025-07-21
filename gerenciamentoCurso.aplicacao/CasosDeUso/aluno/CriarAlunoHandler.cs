using AutoMapper;
using gerenciamentoCurso.aplicacao.Dtos.commands.aluno;
using gerenciamentoCurso.Dominio.Entidades;
using gerenciamentoCurso.Dominio.Entidades.Auth;
using gerenciamentoCurso.Dominio.Interfaces;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;
using MediatR;

namespace gerenciamentoCurso.aplicacao.CasosDeUso.aluno
{
    public class CriarAlunoHandler : IRequestHandler<CriarAlunoCommand, Guid>
    {
        private readonly IAlunoRepositorio _alunoRepository;
        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _unitOfWork;

        public CriarAlunoHandler(IAlunoRepositorio alunoRepository, IJwtService jwtService, IUnitOfWork unitOfWork)
        {
            _alunoRepository = alunoRepository;
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CriarAlunoCommand request, CancellationToken cancellationToken)
        {
            var aluno = new Aluno()
            {
                Email = request.Email,
                Cpf = request.Cpf,
                PasswordHash = _jwtService.Criptografar(request.Senha),
                Nome = request.Nome,
                Sobrenome = request.Sobrenome,
                DataNascimento = request.DataNascimento,
                Ativo = true
            };

            aluno.PasswordHash = _jwtService.Criptografar(request.Senha);

            _alunoRepository.Add(aluno);
            await _unitOfWork.CommitAsync();

            await _alunoRepository.AddRoles(aluno.Id);

            return await _unitOfWork.CommitAsync() ? aluno.Id : Guid.Empty ;
        }
    }
}
