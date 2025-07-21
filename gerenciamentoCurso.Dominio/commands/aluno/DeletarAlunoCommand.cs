using MediatR;

namespace gerenciamentoCurso.aplicacao.Dtos.commands.aluno
{
    public class DeletarUsuarioCommand : IRequest<bool>
    {
        public string Cpf { get; set; }
        public string Email { get; set; }
    }
}
