using BlogProjectOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Domain.Repositories
{
    public interface IAppUserRepository : IBaseRepository<AppUser>
    {
        //IAppUserRepository içinde, kullanmak durumunda olduğumuz ve IBaseRepository'de olmayan metot imzalarını buraya yazabiliriz.

    }
}
