using CompanyAPI.Models;
using CompanyAPI.Models.DB;
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
    public class CompaniesController : ControllerBase
    {
        private readonly IConfiguration _confugration;
        private readonly string _connectionString;
        private readonly DbCompany db;

        public CompaniesController(IConfiguration configuration)
        {
            this._confugration = configuration;
            this._connectionString = this._confugration.GetConnectionString("CompanyAPI");
            this.db = new DbCompany(this._confugration);
        }

        [HttpGet]
        public JsonResult Get()  // get all companies
        {
            List<Company> companies = db.getAllCompanies();
            return new JsonResult(companies);
        }
    }
}
