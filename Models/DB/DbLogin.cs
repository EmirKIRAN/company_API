using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Models.DB
{
    public class DbLogin
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DbLogin(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._connectionString = this._configuration.GetConnectionString("CompanyAPI");
        }

        public bool isUser(User usr)
        {
            SqlConnection _connect = new SqlConnection(this._connectionString);
            bool isUser = false;

            using(SqlCommand cmd = new SqlCommand("SELECT * FROM tblUser",_connect))
            {
                cmd.CommandType = CommandType.Text;
                _connect.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    if(reader["userName"].ToString() == usr.userName && reader["password"].ToString() == usr.password)
                    {
                        isUser = true;
                        break;
                    }
                }
                _connect.Close();  
            }
            return isUser;
        }
    }
}
