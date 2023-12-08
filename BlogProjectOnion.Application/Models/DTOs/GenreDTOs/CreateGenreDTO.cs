using BlogProjectOnion.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.Models.DTOs.GenreDTOs
{
    public class CreateGenreDTO
    {

        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public Status Status { get; set; }
    }
}
