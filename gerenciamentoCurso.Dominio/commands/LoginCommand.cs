using MediatR;

namespace gerenciamentoCurso.aplicacao.Dtos.commands
{
    public class LoginCommand : IRequest<string>
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
