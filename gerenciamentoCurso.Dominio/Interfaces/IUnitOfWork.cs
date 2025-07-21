namespace gerenciamentoCurso.Dominio.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> CommitAsync();
    }
}
