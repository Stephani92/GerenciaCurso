using gerenciamentoCurso.Dominio.Entidades;
using gerenciamentoCurso.Dominio.Entidades.Resposta;
using gerenciamentoCurso.Dominio.Entidades.solicitar;

namespace gerenciamentoCurso.Dominio.Interfaces.repositorios
{
    public interface IAlunoRepositorio : IRepository
    {
        Task<Aluno> GetUsuarioAsyncByEmailLogin(string UserEmail);
        Task<Aluno> GetAlunoAsyncByEmailCpf(string UserEmail, string cpf);
        Task<bool> AnyUsuarioAsyncByEmail(string UserEmail);
        Task<bool> AnyUsuarioAsyncByEmailCpf(string UserEmail, string cpf);
        Task<bool> AnyAlunoAsyncById(Guid Id);

        Task<AlunosResponse> FiltrarAsync(FiltrarAlunoRequest filtro);

        Task AddRoles(Guid UserEmail);

    }
}
