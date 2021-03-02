using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Models.DB
{
    public class DbDepartment
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        public DbDepartment(IConfiguration configration)
        {
            this._configuration = configration;
            this._connectionString = _configuration.GetConnectionString("CompanyAPI");
        }
        public List<Department> getAllDepartments()
        {
            List<Department> departmentsList = new List<Department>();
            SqlConnection _connect = new SqlConnection(_connectionString);

            using(SqlCommand cmd = new SqlCommand("SELECT * FROM getDepartments",_connect))
            {
                cmd.CommandType = CommandType.Text;
                _connect.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    Department department = new Department();
                    department.departmentID = Convert.ToInt32(reader["departmentID"]);
                    department.departmentName = reader["departmentName"].ToString();
                    departmentsList.Add(department);
                }
                _connect.Close();
            }
            return departmentsList;
        }
        public List<DataTable> staffByDepartments()
        {
            List<Department> _departments = getAllDepartments();
            List<DataTable> dep_list = new List<DataTable>();

            SqlConnection _connect = new SqlConnection(_connectionString);
            _connect.Open();
            foreach (Department dep in _departments)
            {
                using(SqlCommand cmd = new SqlCommand("getStaffWithCompany",_connect))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    DataTable data_table = new DataTable();
                    cmd.Parameters.AddWithValue("@departmentid", DbType.Int32).Value = dep.departmentID;
                    SqlDataReader reader = cmd.ExecuteReader();
                    data_table.Load(reader);
                    dep_list.Add(data_table);
                }
            }
            _connect.Close();
            return dep_list;
        }
    }
}
