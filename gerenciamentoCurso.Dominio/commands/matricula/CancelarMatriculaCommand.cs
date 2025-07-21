using MediatR;

namespace gerenciamentoCurso.Dominio.commands.matricula
{
    public class CancelarMatriculaCommand : IRequest<bool>
    {
        public Guid AlunoId { get; set; }
        public Guid TurmaId { get; set; }
    }
}
