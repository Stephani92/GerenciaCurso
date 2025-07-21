using MediatR;

namespace gerenciamentoCurso.Dominio.commands.usuario
{
    internal class DeletarUsuarioCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string Cpf { get; set; }
    }
}

