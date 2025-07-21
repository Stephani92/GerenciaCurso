using gerenciamentoCurso.Dominio.Entidades.Auth;
namespace gerenciamentoCurso.Dominio.Interfaces
{
    public interface IJwtService
    {
        string Criptografar(string password);
        string GenerationJwToken(Usuario user);
    }
}
