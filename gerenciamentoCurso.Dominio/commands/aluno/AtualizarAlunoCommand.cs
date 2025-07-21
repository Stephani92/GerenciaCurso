using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace gerenciamentoCurso.aplicacao.Dtos.commands.aluno
{
    public class AtualizarAlunoCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
