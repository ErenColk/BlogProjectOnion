using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.Models.DTOs.LikeDTOs
{
    public class DeleteLikeDTO
    {
        public Guid UserId { get; set; }
        public int PostId { get; set; }
    }
}
