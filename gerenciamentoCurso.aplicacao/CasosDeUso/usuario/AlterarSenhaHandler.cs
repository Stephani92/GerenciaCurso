using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using gerenciamentoCurso.aplicacao.Dtos.commands.usuario;
using gerenciamentoCurso.Dominio.Entidades.Auth;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;
using gerenciamentoCurso.Dominio.Interfaces;
using MediatR;
using gerenciamentoCurso.Dominio.commands.usuario;

namespace gerenciamentoCurso.aplicacao.CasosDeUso.usuario
{
    public class AlterarSenhaHandler : IRequestHandler<AlterarSenhaCommand, bool>
    {
        private readonly IUsuarioRepositorio _usuarioRepository;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AlterarSenhaHandler(IUsuarioRepositorio usuarioRepository, IJwtService jwtService, IUnitOfWork unitOfWork)
        {
            _usuarioRepository = usuarioRepository;
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(AlterarSenhaCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.GetUsuarioAsyncByEmailCpf(request.Email, request.Cpf);
            usuario.PasswordHash = _jwtService.Criptografar(request.Senha);

            _usuarioRepository.Atualizar(usuario);

            return await _unitOfWork.CommitAsync();
        }
    }
}
