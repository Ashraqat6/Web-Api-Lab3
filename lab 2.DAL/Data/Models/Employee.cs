using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_2.DAL.Data.Models
{
    public class Employee: IdentityUser
    {
        public string Hobby { get; set; }=string.Empty;
    }
}
