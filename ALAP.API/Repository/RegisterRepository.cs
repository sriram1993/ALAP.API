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
using static ALAP.API.DTO.Enum;

namespace ALAP.API.Repository
{
    public class RegisterRepository : IRegisterService
    {
        public readonly IConfiguration _config;

        public RegisterRepository(IConfiguration config)
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

        public async Task<SaveStatus> RegisterUser(RegisterIDTO objRegister)
        {
            try
            {
                //await Task.Delay(1000);
                using (IDbConnection conn = Connection)
                {
                    var param = new DynamicParameters();
                    param.Add("@FirstName", objRegister.FirstName, DbType.String, direction: ParameterDirection.Input);
                    param.Add("@LastName", objRegister.LastName, DbType.String, direction: ParameterDirection.Input);
                    param.Add("@MatriculationNo", objRegister.MatriculationNumber, DbType.Int32, direction: ParameterDirection.Input);
                    param.Add("@Email", objRegister.loginDetails.Email, DbType.String, direction: ParameterDirection.Input);
                    param.Add("@Password", objRegister.loginDetails.Password, DbType.String, direction: ParameterDirection.Input);
                    param.Add("@ReturnValue", DbType.Int32, direction: ParameterDirection.ReturnValue);


                    await conn.ExecuteAsync("usp_RegisterUser", param, commandType: CommandType.StoredProcedure);

                    var retValue = param.Get<int>("@ReturnValue");
                    SaveStatus status = (SaveStatus)retValue;
                    return status;

                }
            }
            catch (Exception e)
            {
                return SaveStatus.Failure;
            }
        }
    }
}
