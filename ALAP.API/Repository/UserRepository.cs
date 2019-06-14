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
                    studObj.transcript = results.Read<Transcript>().FirstOrDefault();
                    return studObj;
                }

            }
            catch(Exception e)
            {
                return null;
            }
        }

        public async Task<List<StudentData>> GetStudentRequestData(string type)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    var param = new DynamicParameters();
                    param.Add("@Type", type, DbType.String, direction: ParameterDirection.Input);
                    Dictionary<int, StudentData> dicStudentData = new Dictionary<int, StudentData>();

                    var results = await conn.QueryAsync<StudentData, Subjects, StudentData>(
                        "usp_GetStudentRequestData",
                        (studentData, subjects) =>
                        {
                            StudentData stud;

                            if(!dicStudentData.TryGetValue(studentData.MatriculationNumber,out stud))
                            {
                                stud = studentData;
                                stud.subjects = new List<Subjects>();
                                dicStudentData.Add(stud.MatriculationNumber, stud);
                            }

                            stud.subjects.Add(subjects);
                            return stud;
                        },
                         param,
                         commandType: CommandType.StoredProcedure,
                        splitOn: "SubjectID"
                        );

                    List<StudentData> resultObj = dicStudentData.Values.ToList();
                    return resultObj;
                }

            }
            catch (Exception e)
            {
                return null;
            }

        }

        /// <summary>
        /// Save Student Data
        /// </summary>
        /// <param name="studentData"></param>
        /// <returns></returns>
        public async Task<SaveStatus> SaveStudentData(StudentData studentData)
        {
            try
            {
                DataTable dtSubjects = new DataTable();
                dtSubjects.Columns.Add("SubjectID", typeof(int));
                dtSubjects.Columns.Add("SubjectName", typeof(string));
                dtSubjects.Columns.Add("IsSelected", typeof(bool));
                dtSubjects.Columns.Add("SubjectMappingID", typeof(int));
                dtSubjects.Columns.Add("IsRejectedByAdmin", typeof(bool));

                foreach (var subject in studentData.subjects)
                {
                    dtSubjects.Rows.Add(subject.SubjectID, subject.SubjectName, subject.isSelected,subject.SubjectMappingID, subject.isRejectedByAdmin);
                }

                using (IDbConnection conn = Connection)
                {
                    var param = new DynamicParameters();
                    param.Add("@StudentID", studentData.StudentID, DbType.Int32, direction: ParameterDirection.Input);
                    param.Add("@IsUpdate", studentData.isUpdate, DbType.Boolean, direction: ParameterDirection.Input);
                    param.Add("@TranscriptFileData", studentData.transcript.FileData, DbType.String, direction: ParameterDirection.Input);
                    param.Add("@TranscriptFileName", studentData.transcript.FileName, DbType.String, direction: ParameterDirection.Input);
                    param.Add("@OcrJson", studentData.transcript.OcrJson, DbType.String, direction: ParameterDirection.Input);
                    param.Add("@subject", dtSubjects.AsTableValuedParameter("dbo.Subject"));
                    param.Add("@ReturnValue", DbType.Int32, direction: ParameterDirection.ReturnValue);

                    await conn.ExecuteAsync("usp_SaveOrUpdateLearningAgreement", param, commandType: CommandType.StoredProcedure);

                    var retValue = param.Get<int>("@ReturnValue");
                    SaveStatus status = (SaveStatus)retValue;
                    return status;
                }
            }
            catch(Exception e)
            {
                return SaveStatus.Failure;
            }

        }

        /// <summary>
        /// Method to Approve the Learning Agreement
        /// </summary>
        /// <param name="studentData"></param>
        /// <returns></returns>
        public async Task<SaveStatus> ApproveRequest(StudentData studentData)
        {
            try
            {
                DataTable dtSubjects = new DataTable();
                dtSubjects.Columns.Add("SubjectID", typeof(int));
                dtSubjects.Columns.Add("SubjectName", typeof(string));
                dtSubjects.Columns.Add("IsSelected", typeof(bool));
                dtSubjects.Columns.Add("SubjectMappingID", typeof(int));
                dtSubjects.Columns.Add("IsRejectedByAdmin", typeof(bool));

                foreach (var subject in studentData.subjects)
                {
                    dtSubjects.Rows.Add(subject.SubjectID, subject.SubjectName, subject.isSelected, subject.SubjectMappingID,subject.isRejectedByAdmin);
                }

                using (IDbConnection conn = Connection)
                {
                    var param = new DynamicParameters();

                    param.Add("@StudentID", studentData.StudentID, DbType.Int32, direction: ParameterDirection.Input );
                    param.Add("@subject", dtSubjects.AsTableValuedParameter("dbo.Subject"));
                    param.Add("@ReturnValue", DbType.Int32, direction: ParameterDirection.ReturnValue);

                    await conn.ExecuteAsync("usp_ApproveLearningAgreement", param, commandType: CommandType.StoredProcedure);

                    var retValue = param.Get<int>("@ReturnValue");
                    SaveStatus status = (SaveStatus)retValue;
                    return status;

                }
            }
            catch(Exception e)
            {
                return SaveStatus.Failure;
            }
        }
    }
}
