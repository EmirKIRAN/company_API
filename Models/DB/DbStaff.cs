using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CompanyAPI.ViewModels;

namespace CompanyAPI.Models.DB
{
    public class DbStaff
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public DbStaff(IConfiguration configration)
        {
            this._configuration = configration;
            this._connectionString = _configuration.GetConnectionString("CompanyAPI");
        }

        public Staff findStaff(int id)
        {
            string sqlCommand = @"SELECT * FROM tblStaff WHERE staffID = " + id.ToString();
            SqlConnection _connect = new SqlConnection(_connectionString);
            Staff stf = new Staff();

            using(SqlCommand cmd = new SqlCommand(sqlCommand,_connect))
            {
                cmd.CommandType = CommandType.Text;
                _connect.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                
                while(reader.Read())
                {
                    stf.staffID = Convert.ToInt32(reader["staffID"]);
                    stf.staffName = reader["staffName"].ToString();
                    stf.staffSurname = reader["staffSurname"].ToString();
                    stf.companyID = Convert.ToInt32(reader["companyID"]);
                }
                _connect.Close();
            }
            return stf;
        }
        public List<Staff> getAllStaff()
        {
            List<Staff> staffList = new List<Staff>();
            SqlConnection _connect = new SqlConnection(_connectionString);

            using(SqlCommand cmd = new SqlCommand("getStaff",_connect))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                _connect.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    Staff s = new Staff();
                    s.staffID = Convert.ToInt32(reader["staffID"]);
                    s.staffName = reader["staffName"].ToString();
                    s.staffSurname = reader["staffSurname"].ToString();
                    s.companyID = Convert.ToInt32(reader["companyID"]);
                    staffList.Add(s);
                }
                _connect.Close();
            }
            return staffList;
        }
        public void insertStaff(EmployeeWork _staff)
        {
            SqlConnection _connect = new SqlConnection(_connectionString);

            using(SqlCommand cmd = new SqlCommand("insertStaff", _connect))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                _connect.Open();

                cmd.Parameters.AddWithValue("@name", SqlDbType.NVarChar).Value = _staff.staffName;
                cmd.Parameters.AddWithValue("@surname", SqlDbType.NVarChar).Value = _staff.staffSurname;
                cmd.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = _staff.companyID;
                cmd.Parameters.AddWithValue("@departmentid", SqlDbType.Int).Value = _staff.departmentID;

                cmd.ExecuteNonQuery();
            }
            _connect.Close();
        }
        public void updateCompany(int staffid, int companyid)
        {
            SqlConnection _connect = new SqlConnection(_connectionString);

            using (SqlCommand cmd = new SqlCommand("updateStaff", _connect))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                _connect.Open();

                cmd.Parameters.AddWithValue("@staffid", SqlDbType.Int).Value = staffid;
                cmd.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid;

                cmd.ExecuteNonQuery();
            }
            _connect.Close();
        }
        public Staff deleteStaff(int staffid)
        {
            SqlConnection _connect = new SqlConnection(_connectionString);
            Staff deleteStaff = findStaff(staffid);

            using (SqlCommand cmd = new SqlCommand("deleteStaff", _connect))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                _connect.Open();

                cmd.Parameters.AddWithValue("@staffid", SqlDbType.Int).Value = staffid;
                cmd.ExecuteNonQuery();
            }
            _connect.Close();
            return deleteStaff;
        }
    }
}
