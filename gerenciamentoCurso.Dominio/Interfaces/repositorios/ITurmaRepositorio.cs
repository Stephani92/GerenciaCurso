using gerenciamentoCurso.Dominio.Entidades;
using gerenciamentoCurso.Dominio.Entidades.Resposta;
using gerenciamentoCurso.Dominio.Entidades.solicitar;

namespace gerenciamentoCurso.Dominio.Interfaces.repositorios
{
    public interface ITurmaRepositorio : IRepository
    {
        Task<Turma> GetTurmaAsyncById(Guid id);
        Task<List<Turma>> GetTurmaAsyncTodos();
        Task<bool> AnyTurmaAsyncById(Guid id);

        Task<bool> AnyAlunoTurmaAsyncById(Guid Id);
        Task<TurmasResponse> FiltrarAsync(FiltrarTurmaRequest filtro);

    }
}
