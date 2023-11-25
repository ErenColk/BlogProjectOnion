using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Domain.Enums
{
    public enum Status
    {
        [Description("Aktif")]
        Active= 1,
        [Description("Güncellendi")]
        Modified= 2,
        [Description("Pasif")]
        Passive = 3,
    }
}
