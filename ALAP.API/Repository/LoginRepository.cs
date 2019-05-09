using ALAP.API.DTO;
using ALAP.API.Services;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ALAP.API.Repository
{
    public class LoginRepository : ILoginService
    {

        public readonly IConfiguration _config;

        public LoginRepository(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DBConnection"));
            }
        }

        public async Task<LoginODTO> Login(LoginIDTO loginInput)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    //Replace with stored procedure
                    string sQuery = "SELECT UserID as StudentID,Email,IsAdmin,NEWID() AS Token  FROM UserDetails WHERE Email = @Email AND Password=@Password";
                    conn.Open();
                    var result = await conn.QueryFirstOrDefaultAsync<LoginODTO>(sQuery, new { Email = loginInput.Email , Password = loginInput.Password });
                    return result;
                }
            }
            catch(Exception e)
            {
                return null;
            }
        }

    }
}
