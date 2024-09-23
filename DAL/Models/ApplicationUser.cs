using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ApplicationUser:IdentityUser
    {
        public bool IsAgree { set; get; }
        public string FName { set; get; }
        public string LName { set; get; }

    }
}
