namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using IRepository;

    using POCO;

    public class GradeChangeRepository : BaseRepository, IGradeChangeRepository
    {
        private const string InsertGradeChangeInfoProcedure = "spInsertGradeChangeInfo";
        private const string UpdateGradeChangeInfoProcedure = "spUpdateGradeChangeInfo";
        private const string DeleteGradeChangeInfoProcedure = "spDeleteGradeChangeInfo";
        private const string GetGradeChangeInfoProcedure = "spGetGradeChangeInfo";
        private const string GetGradeChangeListProcedure = "spGetGradeChangeList";

        public void InsertGradeChangeMessage(GradeChangeMessage message, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(InsertGradeChangeInfoProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@studentName", SqlDbType.VarChar, 128));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@studentId", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@courseName", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@instructorName", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@message", SqlDbType.VarChar, 1000));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@status", SqlDbType.VarChar, 10));

                adapter.SelectCommand.Parameters["@studentName"].Value = message.StudentName;
                adapter.SelectCommand.Parameters["@studentId"].Value = message.StudentId;
                adapter.SelectCommand.Parameters["@courseName"].Value = message.CourseName;
                adapter.SelectCommand.Parameters["@instructorName"].Value = message.InstructorName;
                adapter.SelectCommand.Parameters["@message"].Value = message.MessageBody;
                adapter.SelectCommand.Parameters["@status"].Value = message.Status;

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

        public void UpdateGradeChangeMessage(GradeChangeMessage message, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(UpdateGradeChangeInfoProcedure, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@studentName", SqlDbType.VarChar, 128));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@studentId", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@courseName", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@instructorName", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@message", SqlDbType.VarChar, 1000));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@status", SqlDbType.VarChar, 10));

                adapter.SelectCommand.Parameters["@id"].Value = message.GradeChangeId;
                adapter.SelectCommand.Parameters["@studentName"].Value = message.StudentName;
                adapter.SelectCommand.Parameters["@studentId"].Value = message.StudentId;
                adapter.SelectCommand.Parameters["@courseName"].Value = message.CourseName;
                adapter.SelectCommand.Parameters["@instructorName"].Value = message.InstructorName;
                adapter.SelectCommand.Parameters["@message"].Value = message.MessageBody;
                adapter.SelectCommand.Parameters["@status"].Value = message.Status;

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

        public void DeleteGradeChangeMessage(int id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(DeleteGradeChangeInfoProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType =
                            CommandType
                            .StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
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

        public GradeChangeMessage GetGradeChangeMessageDetail(int id, List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            GradeChangeMessage message = null;

            try
            {
                var adapter = new SqlDataAdapter(GetGradeChangeInfoProcedure, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@id"].Value = id;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                message = new GradeChangeMessage
                {
                    GradeChangeId = id,
                    StudentName = dataSet.Tables[0].Rows[0]["studentName"].ToString(),
                    StudentId = dataSet.Tables[0].Rows[0]["studentId"].ToString(),
                    CourseName = dataSet.Tables[0].Rows[0]["courseName"].ToString(),
                    InstructorName = dataSet.Tables[0].Rows[0]["instructorName"].ToString(),             
                    MessageBody = dataSet.Tables[0].Rows[0]["message"].ToString(),
                    Status = dataSet.Tables[0].Rows[0]["status"].ToString()
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

            return message;
        }

        public List<GradeChangeMessage> GetGradeChangeMessages(string studentId, bool isInstructor, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var messageList = new List<GradeChangeMessage>();

            try
            {
                var adapter = new SqlDataAdapter(GetGradeChangeListProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@studentId", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@isInstructor", SqlDbType.Bit));

                adapter.SelectCommand.Parameters["@studentId"].Value = studentId;
                if (isInstructor)
                {
                    adapter.SelectCommand.Parameters["@isInstructor"].Value = 1;
                }
                else
                {
                    adapter.SelectCommand.Parameters["@isInstructor"].Value = 0;
                }

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var message = new GradeChangeMessage
                    {
                        GradeChangeId = (int)dataSet.Tables[0].Rows[i]["id"],
                        StudentName = dataSet.Tables[0].Rows[i]["studentName"].ToString(),
                        StudentId = dataSet.Tables[0].Rows[i]["studentId"].ToString(),
                        CourseName = dataSet.Tables[0].Rows[i]["courseName"].ToString(),
                        InstructorName = dataSet.Tables[0].Rows[i]["instructorName"].ToString(),
                        MessageBody = dataSet.Tables[0].Rows[i]["message"].ToString(),
                        Status = dataSet.Tables[0].Rows[i]["status"].ToString()
                    };
                    messageList.Add(message);
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

            return messageList;
        }
    }
}
