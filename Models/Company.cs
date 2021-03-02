using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Models
{
    public class Company
    {
        public int companyID { get; set; }
        public string companyName { get; set; }
        public int numberOfStaff { get; set; }
    }
}
