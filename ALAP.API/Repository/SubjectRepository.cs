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
    public class SubjectRepository : ISubjectService
    {

        public readonly IConfiguration _config;

        public SubjectRepository(IConfiguration config)
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

        public async Task<List<Subjects>> GetAllSubjects()
        {
            try
            {
                IEnumerable<Subjects> subjects;
                //await Task.Delay(1000);
                using (IDbConnection conn = Connection)
                {
                    subjects = await conn.QueryAsync<Subjects>("usp_GetAllSubjects", commandType: CommandType.StoredProcedure);
                    List<Subjects> sub = subjects.ToList();
                    return sub;

                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<SaveStatus> AddSubject(Subjects subject)
        {
            try
            {
                //await Task.Delay(1000);
                using (IDbConnection conn = Connection)
                {
                    var param = new DynamicParameters();
                    param.Add("@SubjectName", subject.SubjectName, DbType.String, direction: ParameterDirection.Input);
                    param.Add("@Module", subject.Module, DbType.String, direction: ParameterDirection.Input);
                    param.Add("@ReturnValue", DbType.Int32, direction: ParameterDirection.ReturnValue);


                    await conn.ExecuteAsync("usp_AddSubject", param, commandType: CommandType.StoredProcedure);

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

        public async Task<SaveStatus> UpdateSubject(Subjects subject)
        {
            try
            {
                //await Task.Delay(1000);
                using (IDbConnection conn = Connection)
                {
                    var param = new DynamicParameters();
                    param.Add("@SubjectID", subject.SubjectID, DbType.Int32, direction: ParameterDirection.Input);
                    param.Add("@SubjectName", subject.SubjectName, DbType.String, direction: ParameterDirection.Input);
                    param.Add("@Module", subject.Module, DbType.String, direction: ParameterDirection.Input);
                    param.Add("@IsActive", subject.isSelected, DbType.Boolean, direction: ParameterDirection.Input);
                    param.Add("@ReturnValue", DbType.Int32, direction: ParameterDirection.ReturnValue);


                    await conn.ExecuteAsync("usp_UpdateSubject", param, commandType: CommandType.StoredProcedure);

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
