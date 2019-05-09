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
    }
}
