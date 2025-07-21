using MediatR;

namespace gerenciamentoCurso.Dominio.commands.matricula
{
    public class CriarMatriculaCommand : IRequest<Guid>
    {
        public Guid AlunoId { get; set; }
        public Guid TurmaId { get; set; }
    }
}
