using gerenciamentoCurso.Dominio.Entidades.Auth;
using gerenciamentoCurso.Dominio.Entidades.Resposta;
using gerenciamentoCurso.Dominio.Entidades.solicitar;

namespace gerenciamentoCurso.Dominio.Interfaces.repositorios
{
    public interface IUsuarioRepositorio: IRepository
    {
        Task<Usuario> GetUsuarioAsyncByEmailCpf(string UserEmail, string cpf);
        Task<bool> AnyUsuarioAsyncByEmailCpf(string UserEmail, string cpf);
        Task<UsuariosResponse> FiltrarAsync(FiltrarUsuarioRequest filtro);
        Task<Usuario> GetUsuarioAsyncByEmailLogin(string UserEmail);
        Task AddRoles(Guid UserEmail);
    }
}
