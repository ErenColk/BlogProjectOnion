using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.Models.DTOs.MailDtos
{
    public class ConfirmMailDTO
    {
        public Guid Id { get; set; }
        public int ConfirmCode { get; set; }
        public string Email { get; set; }
    }
}
