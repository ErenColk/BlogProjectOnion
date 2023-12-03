using BlogProjectOnion.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Domain.Entities
{
    public class Follow : IBaseEntity
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public DateTime CreatedDate { get ; set ; }
        public DateTime? UpdatedDate { get ; set ; }
        public DateTime? DeletedDate { get ; set ; }
        public Status Status { get ; set ; }
    }
}
