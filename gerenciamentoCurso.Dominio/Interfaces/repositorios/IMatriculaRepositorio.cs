using gerenciamentoCurso.Dominio.Entidades;

namespace gerenciamentoCurso.Dominio.Interfaces.repositorios
{
    public interface IMatriculaRepositorio : IRepository
    {
        Task CancelarMatricula(Guid alunoId, Guid turmaId);
        Task<bool> AnyMatriculaAsyncByAlunoIdETurmaId(Guid alunoId, Guid turmaId);
    }
}
