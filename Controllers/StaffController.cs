using CompanyAPI.Models;
using CompanyAPI.Models.DB;
using CompanyAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StaffController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DbStaff db;

        public StaffController(IConfiguration configuration)
        {
            this._configuration = configuration;
            db = new DbStaff(this._configuration);
        }

        [HttpGet]
        public JsonResult Get() // get all staff by reverse
        {
            List<Staff> allStaff = db.getAllStaff();
            return new JsonResult(allStaff);
        }
        [HttpPost]
        public JsonResult Post([FromBody] EmployeeWork _staff) // add staff to database
        {
            db.insertStaff(_staff);
            Staff addedStaff = db.getAllStaff()[0];
            return new JsonResult(addedStaff);
        }

        [HttpPut("{id}/{companyid}")]
        public JsonResult Put(int id, int companyid) // update staff's company
        {
            db.updateCompany(id, companyid);
            Staff updatedStaff = db.findStaff(id);
            return new JsonResult(updatedStaff);
        }
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)  // delete staff 
        {
            var deletedStaff = db.deleteStaff(id);
            return new JsonResult(deletedStaff);
        }

    }
}
