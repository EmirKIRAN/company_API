using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Models
{
    public class Staff
    {
        public int staffID { get; set; }
        public string staffName { get; set; }
        public string staffSurname { get; set; }
        public int companyID { get; set; }

    }
}
