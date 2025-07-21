namespace gerenciamentoCurso.Dominio.Interfaces.repositorios
{
    public interface IRepository
    {

        void Add<T>(T entity) where T : class;
        void AddAsync<T>(T entity) where T : class;
        void Atualizar<T>(T entity) where T : class;
        Task<int> SaveChangesAsync();
        Task<T[]> GetAllAsync<T>() where T : class;

        Task<T> GetAsyncById<T>(Guid Id) where T : class;

    }
}
