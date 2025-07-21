using gerenciamentoCurso.aplicacao.Dtos.commands;
using gerenciamentoCurso.Dominio.Interfaces;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;
using MediatR;

namespace gerenciamentoCurso.aplicacao.CasosDeUso
{
    public class LoginHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IUsuarioRepositorio _usuarioRepository;
        private readonly IJwtService _jwtService;

        public LoginHandler(IUsuarioRepositorio usuarioRepository, IJwtService jwtService)
        {
            _usuarioRepository = usuarioRepository;
            _jwtService = jwtService;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _usuarioRepository.GetUsuarioAsyncByEmailLogin(request.Email);
            if (user == null || _jwtService.Criptografar(request.Senha) != user.PasswordHash)
                throw new UnauthorizedAccessException("Senha inválidos.");

            return _jwtService.GenerationJwToken(user);
        }
    }
}
