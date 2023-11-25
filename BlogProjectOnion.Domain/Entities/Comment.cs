using BlogProjectOnion.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Domain.Entities
{
    public class Comment : IBaseEntity
    {
        public string Content { get; set; }


        //Navigation Properties
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Status Status { get; set; }
        public Guid AppUserId { get; set; } // Identityden aldığı için tipi Guid
        public AppUser AppUser { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
