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
    public class UserRepository : IUserService
    {
        public readonly IConfiguration _config;

        public UserRepository(IConfiguration config)
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

        /// <summary>
        /// To get the logged in student Data
        /// </summary>
        /// <param name="studentID"></param>
        /// <returns></returns>
        public async Task<StudentData> GetStudentData(int studentID)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    var param = new DynamicParameters();
                    param.Add("@StudentID", studentID, DbType.String, direction: ParameterDirection.Input);

                    var results = await conn.QueryMultipleAsync("usp_GetStudentData", param, commandType: CommandType.StoredProcedure);

                    StudentData studObj = results.Read<StudentData>().FirstOrDefault();
                    studObj.subjects = results.Read<Subjects>().ToList();
                    return studObj;
                }

            }
            catch(Exception e)
            {
                return null;
            }
        }
    }
}
