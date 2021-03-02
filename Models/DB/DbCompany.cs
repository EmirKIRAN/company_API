using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Models.DB
{
    public class DbCompany
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DbCompany(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._connectionString = _configuration.GetConnectionString("CompanyAPI");
        }

        public List<Company> getAllCompanies()
        {
            List<Company> companyList = new List<Company>();
            SqlConnection _connect = new SqlConnection(_connectionString);

            using(SqlCommand cmd = new SqlCommand("SELECT * FROM getCompanyInfo", _connect))
            {
                cmd.CommandType = CommandType.Text;
                _connect.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    Company _comp = new Company();
                    _comp.companyID = Convert.ToInt32(reader["companyID"]);
                    _comp.companyName = reader["companyName"].ToString();
                    _comp.numberOfStaff = Convert.ToInt32(reader["NUMBER OF STAFF"]);
                    companyList.Add(_comp);
                }
                _connect.Close();
            }
            return companyList;
        }
    }
}
