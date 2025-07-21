using MediatR;

namespace gerenciamentoCurso.Dominio.commands.usuario
{
    public class AlterarSenhaCommand : IRequest<bool>
    {
        public string Senha { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
    }
}
