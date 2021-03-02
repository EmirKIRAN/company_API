using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.ViewModels
{
    public class EmployeeWork
    {
        public int staffID { get; set; }
        public int departmentID { get; set; }
        public int companyID { get; set; }
        public string staffName { get; set; }
        public string staffSurname { get; set; }
    }
}
