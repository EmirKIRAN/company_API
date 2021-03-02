using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using CompanyAPI.Models.DB;
using CompanyAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace CompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DbDepartment db_department;

        public DepartmentController(IConfiguration configuration)
        {
            this._configuration = configuration;
            db_department = new DbDepartment(_configuration);
        }

        [HttpGet]
        public JsonResult Get() // get all departments in database
        {
            List<Department> departmentList =  db_department.getAllDepartments();
            return new JsonResult(departmentList);
        }
        [HttpPost]
        public JsonResult Post() // get staff according to department
        {
            List<DataTable> departmentListForStaff = db_department.staffByDepartments();
            return new JsonResult(departmentListForStaff);
        }
    }
}
