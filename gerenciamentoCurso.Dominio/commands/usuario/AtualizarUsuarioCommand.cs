using MediatR;

namespace gerenciamentoCurso.aplicacao.Dtos.commands.usuario
{
    public class AtualizarUsuarioCommand : IRequest<Guid>
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}