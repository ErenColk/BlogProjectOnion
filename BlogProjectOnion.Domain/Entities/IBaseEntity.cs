using BlogProjectOnion.Domain.Enums;

namespace BlogProjectOnion.Domain.Entities
{
    public interface IBaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Status Status { get; set; }
    }
}
