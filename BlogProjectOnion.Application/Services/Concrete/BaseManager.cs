using BlogProjectOnion.Application.Services.Abstract;
using BlogProjectOnion.Domain.Entities;
using BlogProjectOnion.Domain.Repositories;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.Services.Concrete
{
    public class BaseManager<T> : IBaseServices<T> where T : class, IBaseEntity
    {
        private readonly IBaseRepository<T> _baseRepository;

        public BaseManager(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task<bool> DefaultUpdate(T entity)
        {
            return _baseRepository.DefaultUpdate(entity);
        }

        public async Task<T> GetById(int id)
        {
            if (id <= 0)
                return null;
            return await _baseRepository.GetById(id);
        }

        public Task<bool> TAny(Expression<Func<T, bool>> expression)
        {
            return _baseRepository.Any(expression);
        }

        public async Task<bool> TCreate(T entity)
        {

            if (entity != null)
                return await _baseRepository.Create(entity);
            else
                return false;

        }

        public async Task<bool> TDelete(T entity)
        {
            if (entity != null)
                return await _baseRepository.Delete(entity);
            else
                return false;
        }

        public Task<T> TGetDefault(Expression<Func<T, bool>> expression)
        {
            return _baseRepository.GetDefault(expression);
        }

        public Task<List<T>> TGetDefaults(Expression<Func<T, bool>> expression = null)
        {
            return _baseRepository.GetDefaults(expression);
        }

        public Task<TResult> TGetFilteredFirstOrDefault<TResult>(Expression<Func<T, TResult>> select = null, Expression<Func<T, bool>> where = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return _baseRepository.GetFilteredFirstOrDefault(select,where,orderBy,include);
        }

        public Task<List<TResult>> TGetFilteredList<TResult>(Expression<Func<T, TResult>> select = null, Expression<Func<T, bool>> where = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return _baseRepository.GetFilteredList(select, where, orderBy, include);

        }

        public async Task<bool> TUpdate(T entity)
        {
            if (entity != null)
                return await _baseRepository.Update(entity);
            else
                return false;

        }
    }
}
