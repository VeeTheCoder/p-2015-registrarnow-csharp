namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using IRepository;

    using POCO;

    public class GradStudentRepository : BaseRepository, IGradStudentRepository
    {
        private const string InsertGradStudentInfoProcedure = "spInsertGradStudentInfo";
        private const string UpdateGradStudentInfoProcedure = "spUpdateGradStudentInfo";
        private const string DeleteGradStudentInfoProcedure = "spDeleteGradStudentInfo";
        private const string GetGradStudentListProcedure = "spGetGradStudentList";
        private const string GetGradStudentInfoProcedure = "spGetGradStudentInfo";
        private const string InsertGradStudentScheduleProcedure = "spInsertGradStudentSchedule";
        private const string DeleteGradStudentScheduleProcedure = "spDeleteGradStudentSchedule";

        // Added 8/13/2015
        private const string GetGradStudentEnrollmentProcedure = "spGetGradStudentEnrollment";

        public void InsertGradStudent(GradStudent gradStudent, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(InsertGradStudentInfoProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@sid", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@under_gpa", SqlDbType.Float));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@gre", SqlDbType.Float));

                adapter.SelectCommand.Parameters["@sid"].Value = gradStudent.Sid;
                adapter.SelectCommand.Parameters["@under_gpa"].Value = gradStudent.Under_gpa;
                adapter.SelectCommand.Parameters["@gre"].Value = gradStudent.Gre;

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

        public void UpdateGradStudent(GradStudent gradStudent, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(UpdateGradStudentInfoProcedure, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@sid", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@under_gpa", SqlDbType.Float));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@gre", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@sid"].Value = gradStudent.Sid;
                adapter.SelectCommand.Parameters["@under_gpa"].Value = gradStudent.Under_gpa;
                adapter.SelectCommand.Parameters["@gre"].Value = gradStudent.Gre;

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

        public void DeleteGradStudent(string id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(DeleteGradStudentInfoProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType =
                            CommandType
                            .StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.VarChar, 20));

                adapter.SelectCommand.Parameters["@id"].Value = id;

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

        public GradStudent GetGradStudentDetail(string id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            GradStudent gradStudent = null;

            try
            {
                var adapter = new SqlDataAdapter(GetGradStudentInfoProcedure, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@sid", SqlDbType.VarChar, 20));

                adapter.SelectCommand.Parameters["@sid"].Value = id;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                gradStudent = new GradStudent
                {
                    Sid = dataSet.Tables[0].Rows[0]["sid"].ToString(),
                    Under_gpa = (float)Convert.ToDouble(dataSet.Tables[0].Rows[0]["under_gpa"].ToString()),
                    Gre = Convert.ToInt32(dataSet.Tables[0].Rows[0]["gre"].ToString())
                };
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }

            return gradStudent;
        }

        public List<GradStudent> GetGradStudentList(ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var gradStudentList = new List<GradStudent>();

            try
            {
                var adapter = new SqlDataAdapter(GetGradStudentListProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var gradStudent = new GradStudent
                    {
                        Sid = dataSet.Tables[0].Rows[i]["sid"].ToString(),
                        Under_gpa = (float)Convert.ToDouble(dataSet.Tables[0].Rows[i]["under_gpa"].ToString()),
                        Gre = Convert.ToInt32(dataSet.Tables[0].Rows[i]["gre"].ToString())
                    };
                    gradStudentList.Add(gradStudent);
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

            return gradStudentList;
        }

        public void EnrollSchedule(string studentId, int scheduleId, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(InsertGradStudentScheduleProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType
                            =
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

        public void DropEnrolledSchedule(string studentId, int scheduleId, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(DeleteGradStudentScheduleProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType
                            =
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

        // implemented 8/13/2015
        public List<Enrollment> GetEnrollments(string studentId)
        {
            var conn = new SqlConnection(ConnectionString);
            var enrolledList = new List<Enrollment>();

            try
            {
                var adapter = new SqlDataAdapter(GetGradStudentEnrollmentProcedure, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));

                adapter.SelectCommand.Parameters["@student_id"].Value = studentId;

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
                        StudentId = studentId,
                        ScheduleId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["schedule_id"].ToString()),
                        Grade = dataSet.Tables[0].Rows[i]["grade"].ToString()
                    };
                    enrolledList.Add(enrollment);
                }
            }
            catch (Exception e)
            {
                // not sure how to deal with this yet
                System.Diagnostics.Debug.WriteLine(e);
            }
            finally
            {
                conn.Dispose();
            }

            return enrolledList;
        }
    }
}
