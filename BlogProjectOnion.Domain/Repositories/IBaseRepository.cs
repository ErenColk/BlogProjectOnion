using BlogProjectOnion.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Domain.Repositories
{
    public interface IBaseRepository<T> where T : class , IBaseEntity // IBaseEntityden inherit alanlar T olabilir.
    {
        // Task : Asenkron çalışacak metotlar için kullanılır.

        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity); // Db'den silme işlemi yapmayacak, status'ü pasife çekecek. (Soft delete olarakda adlandırılır)
        Task<bool> Any(Expression<Func<T, bool>> expression); // Bir kayıt varsa true yoksa false dönecek.
        Task<T> GetDefault(Expression<Func<T, bool>> expression); // Dinamik olarak where filtre işlemi sağlar. Id'ye göre getir gibi...
        Task<T> GetById(int id);
        Task<List<T>> GetDefaults(Expression<Func<T, bool>> expression); // Kategorisi = 5 olan postların gelmesini sağlamak gibi...

        // Hem Select hem de orderby işlemini yapabileceğimiz, Post, Yazar, Comment, Like'ları bir arada çekebileceğimiz include işlemini yapabileceğimiz bir metot gerekir. Bir sorgunun içine birden fazla tablo girecek demektir. Eager Loading kullanacağız.

        Task<TResult> GetFilteredFirstOrDefault<TResult>(
            Expression<Func<T, TResult>> select, // select işlemi
            Expression<Func<T, bool>> where, // Where işlemi
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, // Sıralama işlemi
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null // Join işlemi            
            );

        // Backend' e bir Stored Procedure yazacakmışız gibi bir çalışma  yapıyoruz. Eager Loading'e göre Include (join) , Order By, Where, Select işlemleri yapıyoruz.

        Task<List<TResult>> GetFilteredList<TResult>(
              Expression<Func<T, TResult>> select, // select işlemi
              Expression<Func<T, bool>> where, // Where işlemi
              Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, // sırlama işlemi
              Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null // Join işlemi
              );
    }
}
