using AutoMapper;
using gerenciamentoCurso.aplicacao.Dtos.commands.usuario;
using gerenciamentoCurso.Dominio.Entidades.Auth;
using gerenciamentoCurso.Dominio.Interfaces;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;
using MediatR;

namespace gerenciamentoCurso.aplicacao.CasosDeUso.usuario
{
    public class AtualizarUsuarioHandler : IRequestHandler<AtualizarUsuarioCommand, Guid>
    {
        private readonly IUsuarioRepositorio _alunoRepository;
        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _unitOfWork;

        public AtualizarUsuarioHandler(IUsuarioRepositorio alunoRepository, IJwtService jwtService, IUnitOfWork unitOfWork)
        {
            _alunoRepository = alunoRepository;
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(AtualizarUsuarioCommand request, CancellationToken cancellationToken)
        {

            var usuario = await _alunoRepository.GetUsuarioAsyncByEmailCpf(request.Email, request.Cpf);


            usuario.Nome = request.Nome;
            usuario.Sobrenome = request.Sobrenome;
            usuario.DataNascimento = request.DataNascimento;
            usuario.Ativo = request.Ativo;

            _alunoRepository.Atualizar(usuario);
            return await _unitOfWork.CommitAsync() ? usuario.Id : Guid.Empty;
        }
    }
}