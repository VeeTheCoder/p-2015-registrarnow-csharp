namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using IRepository;

    using POCO;

    public class ForeignStudentRepository : BaseRepository, IForeignStudentRepository
    {
        private const string InsertForeignStudentInfoProcedure = "spInsertForeignStudentInfo";
        private const string UpdateForeignStudentInfoProcedure = "spUpdateForeignStudentInfo";
        private const string DeleteForeignStudentInfoProcedure = "spDeleteForeignStudentInfo";
        private const string GetForeignStudentListProcedure = "spGetForeignStudentList";
        private const string GetForeignStudentInfoProcedure = "spGetForeignStudentInfo";
        private const string InsertForeignStudentScheduleProcedure = "spInsertForeignStudentSchedule";
        private const string DeleteForeignStudentScheduleProcedure = "spDeleteForeignStudentSchedule";

        // Added 8/13/2015
        private const string GetStudentEnrollmentProcedure = "spGetForeignStudentEnrollment";

        public void InsertForeignStudent(ForeignStudent foreignStudent, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(InsertForeignStudentInfoProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@sid", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@under_gpa", SqlDbType.Float));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@toefl", SqlDbType.Float));

                adapter.SelectCommand.Parameters["@sid"].Value = foreignStudent.Sid;
                adapter.SelectCommand.Parameters["@under_gpa"].Value = foreignStudent.Under_gpa;
                adapter.SelectCommand.Parameters["@toefl"].Value = foreignStudent.Toefl;

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

        public void UpdateForeignStudent(ForeignStudent foreignStudent, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(UpdateForeignStudentInfoProcedure, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@sid", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@under_gpa", SqlDbType.Float));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@toefl", SqlDbType.Float));

                adapter.SelectCommand.Parameters["@sid"].Value = foreignStudent.Sid;
                adapter.SelectCommand.Parameters["@under_gpa"].Value = foreignStudent.Under_gpa;
                adapter.SelectCommand.Parameters["@toefl"].Value = foreignStudent.Toefl;

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

        public void DeleteForeignStudent(string id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(DeleteForeignStudentInfoProcedure, conn)
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

        public ForeignStudent GetForeignStudentDetail(string id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            ForeignStudent foreignStudent = null;

            try
            {
                var adapter = new SqlDataAdapter(GetForeignStudentInfoProcedure, conn)
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

                foreignStudent = new ForeignStudent
                {
                    Sid = dataSet.Tables[0].Rows[0]["sid"].ToString(),
                    Under_gpa = (float)Convert.ToDouble(dataSet.Tables[0].Rows[0]["under_gpa"].ToString()),
                    Toefl = (float)Convert.ToDouble(dataSet.Tables[0].Rows[0]["toefl"].ToString())
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

            return foreignStudent;
        }

        public List<ForeignStudent> GetForeignStudentList(ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var foreignStudentList = new List<ForeignStudent>();

            try
            {
                var adapter = new SqlDataAdapter(GetForeignStudentListProcedure, conn)
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
                    var foreignStudent = new ForeignStudent
                    {
                        Sid = dataSet.Tables[0].Rows[i]["sid"].ToString(),
                        Under_gpa = (float)Convert.ToDouble(dataSet.Tables[0].Rows[i]["under_gpa"].ToString()),
                        Toefl = (float)Convert.ToDouble(dataSet.Tables[0].Rows[i]["toefl"].ToString())
                    };
                    foreignStudentList.Add(foreignStudent);
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

            return foreignStudentList;
        }

        public void EnrollSchedule(string studentId, int scheduleId, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(InsertForeignStudentScheduleProcedure, conn)
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
                var adapter = new SqlDataAdapter(DeleteForeignStudentScheduleProcedure, conn)
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
                var adapter = new SqlDataAdapter(GetStudentEnrollmentProcedure, conn)
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
