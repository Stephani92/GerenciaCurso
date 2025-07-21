using gerenciamentoCurso.Dominio.Interfaces.repositorios;
using Microsoft.EntityFrameworkCore;

namespace gerenciamentoCurso.repositorio.Repositorio
{
    public class DefaultRepository : IRepository
    {
        public readonly AppDbContext data;
        public DefaultRepository(AppDbContext _data)
        {
            data = _data;
            data.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }


        public void Add<T>(T entity) where T : class
        {
            data.Set<T>().Add(entity);
        }
        public void AddAsync<T>(T entity) where T : class
        {
            data.Set<T>().AddAsync(entity);
        }
        public void Atualizar<T>(T entity) where T : class
        {
            data.Set<T>().Update(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await data.SaveChangesAsync();
        }
        public Task<T[]> GetAllAsync<T>() where T : class
        {
            return data.Set<T>().OrderBy(x => x).ToArrayAsync();
        }

        public async Task<T> GetAsyncById<T>(Guid Id) where T : class
        {
            return await data.Set<T>().FindAsync(Id);
        }

    }
}
