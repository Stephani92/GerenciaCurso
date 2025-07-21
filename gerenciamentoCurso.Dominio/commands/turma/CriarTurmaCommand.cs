using MediatR;

namespace gerenciamentoCurso.aplicacao.Dtos.commands.turma
{
    public class CriarTurmaCommand : IRequest<Guid>
    {

        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}
