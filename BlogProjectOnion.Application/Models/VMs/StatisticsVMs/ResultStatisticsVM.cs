using BlogProjectOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.Models.VMs.StatisticsVMs
{
    public class ResultStatisticsVM
    {

        public ResultStatisticsVM()
        {
            Posts = new List<Post>();
            AppUsers = new List<AppUser>();
            Top10Posts = new List<Post>();
        }

        public List<Post> Posts { get; set; }
        public List<AppUser> AppUsers { get; set; }
        public List<Author> Authors { get; set; }
        public int TotalRead { get; set; }
        public List<Post> Top10Posts { get; set; }
    }
}
