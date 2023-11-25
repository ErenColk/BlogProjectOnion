using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.Models.VMs
{
    public class PostVMT
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }

        public string AuthorFirstname { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorFullName => $"{AuthorFirstname} {AuthorLastName}";
        public string AuthorImagePath { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
    }
}
