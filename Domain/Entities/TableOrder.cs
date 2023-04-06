using Microsoft.AspNetCore.Identity;
using Service.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Entities
{
    public class TableOrder
    {
        public string TableID { get; set; }
        public TableBO Table { get; set; }
        //public IdentityUser User { get; set; }
        public ApplicationUser User { get; set; } // them User de thang Admin lo can xem thong tin thang User, de co yeu cau
    }
}
