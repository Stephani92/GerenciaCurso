using System.ComponentModel.DataAnnotations;
using MediatR;

namespace gerenciamentoCurso.aplicacao.Dtos.commands.aluno
{
    public class CriarAlunoCommand : IRequest<Guid>
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Cpf { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
