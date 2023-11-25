﻿using BlogProjectOnion.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.Services.Abstract
{
    public interface IBaseServices <T> where T : IBaseEntity
    {

        Task<bool> TCreate(T entity);
        Task<bool> TUpdate(T entity);
        Task<bool> TDelete(T entity); 
        Task<bool> TAny(Expression<Func<T, bool>> expression); 
        Task<T> TGetDefault(Expression<Func<T, bool>> expression);

        Task<T> GetById(int id);
        Task<List<T>> TGetDefaults(Expression<Func<T, bool>> expression = null);

        Task<TResult> TGetFilteredFirstOrDefault<TResult>(
            Expression<Func<T, TResult>> select, 
            Expression<Func<T, bool>> where, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null         
            );

        Task<List<TResult>> TGetFilteredList<TResult>(
              Expression<Func<T, TResult>> select, 
              Expression<Func<T, bool>> where, 
              Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
              Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null 
              );
    }
}