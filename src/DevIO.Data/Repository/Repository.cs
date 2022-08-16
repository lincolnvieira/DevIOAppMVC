using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Data.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : Entity, new()
    {
        protected readonly AppMvcContext appMvcContext;
        protected readonly DbSet<T> dbSet;

        public Repository(AppMvcContext appMvcContext)
        {
            this.appMvcContext = appMvcContext;
            this.dbSet = appMvcContext.Set<T>();
        }

        public virtual async Task Adicionar(T entity)
        {
            dbSet.Add(entity);

            await SaveChanges();
        }

        public virtual async Task Atualizar(T entity)
        {
            dbSet.Update(entity);

            await SaveChanges();
        }

        public virtual async Task Remover(Guid id)
        { 
            dbSet.Remove(new T { Id = id });

            await SaveChanges();
        }

        public async Task<IEnumerable<T>> Buscar(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<T> ObterPorId(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<List<T>> ObterTodos(T entity)
        {
            return await dbSet.ToListAsync();
        }

        public async Task<int> SaveChanges()
        {
            return await appMvcContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            appMvcContext?.Dispose();
        }
    }
}
