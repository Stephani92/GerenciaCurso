using MediatR;

namespace gerenciamentoCurso.aplicacao.Dtos.commands.turma
{
    public class DeletarTurmaCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
