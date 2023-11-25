using BlogProjectOnion.Domain.Entities;
using BlogProjectOnion.Domain.Repositories;
using BlogProjectOnion.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Infrastructure.Repositories
{

    public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
    {
        private readonly AppDbContext _context;
        protected DbSet<T> _table;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _table = _context.Set<T>(); // dependency injection yaptık db hangi tipe bürünmüşse T olarak buraya atıyoruz. Table'ın Dbsetine atmış olduk.
        }
        public async Task<bool> Any(Expression<Func<T, bool>> expression)
        {
            return await _table.AnyAsync(expression);

        }



        public async Task<bool> Create(T entity)
        {
            try
            {
                entity.CreatedDate = DateTime.Now;
                entity.Status = Domain.Enums.Status.Active;
                await _table.AddAsync(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                return false;
            }





        }

        public async Task<bool> Delete(T entity)
        {
            try
            {
                entity.DeletedDate = DateTime.Now;
                entity.Status = Domain.Enums.Status.Passive;
                _table.Update(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }



        }

        public async Task<T> GetById(int id)
        {
            return  await _table.FindAsync(id);
        }

        public async Task<T> GetDefault(Expression<Func<T, bool>> expression )
        {

            return await _table.FirstOrDefaultAsync(expression);

        }

        public async Task<List<T>> GetDefaults(Expression<Func<T, bool>> expression = null)
        {
            if(expression == null)
            {
                return _table.ToList();
            }
            else
            return _table.Where(expression).ToList();
        }

        public async Task<TResult> GetFilteredFirstOrDefault<TResult>(Expression<Func<T, TResult>> select, Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {

            IQueryable<T> query = _context.Set<T>();  // SELECT * FROM Post gibi...

            if (include != null)  // JOIN İŞLEMİ
            {
                query = include(query);
            }

            if (where != null) // SELECT * FOM Post WHERE Status = 1 gibi...
            {
                query = query.Where(where);
            }

            if (orderBy != null)
            {
                return await orderBy(query).Select(select).FirstOrDefaultAsync();

            }

            var result = await query.Select(select).FirstOrDefaultAsync();

            return result;

        }

        public async Task<List<TResult>> GetFilteredList<TResult>(Expression<Func<T, TResult>> select, Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {

            IQueryable<T> query = _table; // SELECT * FROM Post gibi...

            if (where != null)   // SELECT * FOM Post WHERE Status = 1 gibi...
            {
                query = query.Where(where);
            }

            if (include != null)
            {
                query = include(query); // JOIN İŞLEMİ
            }


            if (orderBy != null)  // Sıralama işlemi varsa sıralayıp return edecek yoksa sıralamadan query'i sorgulayıop sonucu liste şeklinde return edecek.
            {

                return await orderBy(query).Select(select).ToListAsync();
            }

            var result = await query.Select(select).ToListAsync();

            return result;

        }


        public async Task<bool> Update(T entity)
        {
            try
            {
                entity.UpdatedDate = DateTime.Now;
                entity.Status = Domain.Enums.Status.Modified;
                _context.Entry<T>(entity).State = EntityState.Modified; // Güncelleme işlemini Entity State'ini değiştirerek yapıyoruz
                return await _context.SaveChangesAsync() > 0;

            }
            catch (Exception)
            {

                return false;
            }

        }
    }
}
