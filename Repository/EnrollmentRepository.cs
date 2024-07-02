namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using IRepository;

    using POCO;

    public class EnrollmentRepository : BaseRepository, IEnrollmentRepository
    {
        private const string InsertEnrollmentInfoProcedure = "spInsertEnrollmentInfo"; // C
        private const string GetEnrollmentInfoProcedure = "spGetEnrollmentInfo"; // R
        private const string UpdateEnrollmentInfoProcedure = "spUpdateEnrollmentInfo"; // U
        private const string DeleteEnrollmentInfoProcedure = "spDeleteEnrollmentInfo"; // D

        public void InsertEnrollment(Enrollment enrollment, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(InsertEnrollmentInfoProcedure, conn)
                                  {
                                      SelectCommand =
                                          {
                                              CommandType = CommandType.StoredProcedure
                                          }
                                  };
                var output = new SqlParameter("retValue", SqlDbType.Int);
                output.Direction = ParameterDirection.ReturnValue;

                adapter.SelectCommand.Parameters.Add(output);

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@grade", SqlDbType.VarChar, 10));

                adapter.SelectCommand.Parameters["@student_id"].Value = enrollment.StudentId;
                adapter.SelectCommand.Parameters["@schedule_id"].Value = enrollment.ScheduleId;
                adapter.SelectCommand.Parameters["@grade"].Value = string.IsNullOrEmpty(enrollment.Grade) ? DBNull.Value.ToString() : enrollment.Grade;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if ((int)output.Value == 0)
                {
                    throw new ArgumentException("Insert Failed: Foreign Key Broken?");
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }
       }

        /* for this, we should assume that the enrollment is filled
         * which is easier than revamping StudentRepository completely
         * WARNING: Does not check if enrollment lists agree but that
         * should be handled at the lower level by a foreign key constraint!
         */
        public List<Enrollment> GetEnrollmentInfo(string studentId, int scheduleId, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var enrollmentList = new List<Enrollment>();

            try
            {
                var adapter = new SqlDataAdapter(GetEnrollmentInfoProcedure, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };
                if (studentId.Length > 0)
                {
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));
                    adapter.SelectCommand.Parameters["@student_id"].Value = studentId;
                }

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters["@schedule_id"].Value = scheduleId;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var enrollment = new Enrollment
                    {
                        StudentId = dataSet.Tables[0].Rows[i]["student_id"].ToString(),
                        ScheduleId = scheduleId,
                        Grade = dataSet.Tables[0].Rows[i]["grade"].ToString()
                    };
                    enrollmentList.Add(enrollment);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }

            return enrollmentList;
        }

        public void UpdateEnrollment(Enrollment enrollment, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(UpdateEnrollmentInfoProcedure, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@grade", SqlDbType.VarChar, 10));
  
                adapter.SelectCommand.Parameters["@student_id"].Value = enrollment.StudentId;
                adapter.SelectCommand.Parameters["@schedule_id"].Value = enrollment.ScheduleId;
                adapter.SelectCommand.Parameters["@grade"].Value = enrollment.Grade;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }
        }

        public void DeleteEnrollment(string studentId, int scheduleId, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(DeleteEnrollmentInfoProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType =
                            CommandType
                            .StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@student_id"].Value = studentId;
                adapter.SelectCommand.Parameters["@schedule_id"].Value = scheduleId;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }
        }
    }
}