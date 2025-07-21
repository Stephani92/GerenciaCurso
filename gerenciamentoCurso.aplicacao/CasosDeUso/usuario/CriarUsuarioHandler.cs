using AutoMapper;
using gerenciamentoCurso.aplicacao.Dtos.commands.usuario;
using gerenciamentoCurso.Dominio.Entidades.Auth;
using gerenciamentoCurso.Dominio.Interfaces;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;
using MediatR;

namespace gerenciamentoCurso.aplicacao.CasosDeUso.usuario
{
    public class CriarUsuarioHandler : IRequestHandler<CriarUsuarioCommand, Guid>
    {
        private readonly IUsuarioRepositorio _usuarioRepository;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CriarUsuarioHandler(IUsuarioRepositorio usuarioRepository, IJwtService jwtService, IUnitOfWork unitOfWork)
        {
            _usuarioRepository = usuarioRepository;
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CriarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = new Usuario()
            {
                Nome = request.Nome,
                Sobrenome = request.Sobrenome,
                Cpf = request.Cpf,
                Email = request.Email,
                PasswordHash = _jwtService.Criptografar(request.Senha),
                DataNascimento = request.DataNascimento,
                Ativo = true
            };

            _usuarioRepository.Add(usuario);
            await _unitOfWork.CommitAsync();
            
            await _usuarioRepository.AddRoles(usuario.Id);

            return await _unitOfWork.CommitAsync() ? usuario.Id : Guid.Empty;
        }
    }
}

