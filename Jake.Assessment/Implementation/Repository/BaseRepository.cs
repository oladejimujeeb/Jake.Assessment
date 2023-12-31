﻿using Jake.Assessment.Context;
using Jake.Assessment.Interface.IRepository;
using Jake.Assessment.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jake.Assessment.Implementation.Repository
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T :  BaseEntity, new()
    {
        protected ApplicationContext Context;
        public async Task<T?> Get(int id) => await Context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<T?> Get(Expression<Func<T, bool>> expression) => await Context.Set<T>().FirstOrDefaultAsync(expression);

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllWhere(Expression<Func<T, bool>> expression)
        {
            return await Context.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<IList<T>> GetList(IList<int> ids)
        {
            return await Context.Set<T>().Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public async Task<bool> Exists(Expression<Func<User, bool>> expression)
        {
            return await Context.Set<User>().AnyAsync(expression);
        }

        public IQueryable<T> Query()
        {
            return Context.Set<T>();
        }

        public async Task<T> Add(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }
        public async Task<List<T>> AddRange(List<T> entity)
        {
            await Context.Set<T>().AddRangeAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            Context.Set<T>().Update(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<int> SaveChanges()
        {
            return await Context.SaveChangesAsync();
        }

        public IQueryable<T> QueryWhere(Expression<Func<T, bool>> expression)
        {
            return Context.Set<T>().Where(expression).ToList().AsQueryable();
        }
    }
}