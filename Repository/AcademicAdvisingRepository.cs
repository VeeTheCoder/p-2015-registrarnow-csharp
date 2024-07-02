namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using IRepository;

    using POCO;

    public class AcademicAdvisingRepository : BaseRepository, IAcademicAdvisingRepository
    {
        private const string InsertAcademicAdvisingInfoProcedure = "spInsertAcademicAdvisingInfo";
        private const string UpdateAcademicAdvisingInfoProcedure = "spUpdateAcademicAdvisingInfo";
        private const string DeleteAcademicAdvisingInfoProcedure = "spDeleteAcademicAdvisingInfo";
        private const string GetAcademicAdvisingInfoProcedure = "spGetAcademicAdvisingInfo";
        private const string GetAcademicAdvisingListProcedure = "spGetAcademicAdvisingList";

        public void InsertAdvisingMessage(AcademicAdvisingMessage message, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(InsertAcademicAdvisingInfoProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@studentId", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@studentName", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@sendToInstructor", SqlDbType.Bit));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@messageSubject", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@message", SqlDbType.VarChar, 256));

                adapter.SelectCommand.Parameters["@studentId"].Value = message.StudentId;
                adapter.SelectCommand.Parameters["@studentName"].Value = message.StudentName;
                adapter.SelectCommand.Parameters["@sendToInstructor"].Value = message.SendToInstructor;
                adapter.SelectCommand.Parameters["@messageSubject"].Value = message.MessageSubject;
                adapter.SelectCommand.Parameters["@message"].Value = message.MessageBody;

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

        public void UpdateAdvisingMessage(AcademicAdvisingMessage message, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(UpdateAcademicAdvisingInfoProcedure, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@studentId", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@studentName", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@sendToInstructor", SqlDbType.Bit));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@messageSubject", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@message", SqlDbType.VarChar, 256));

                adapter.SelectCommand.Parameters["@id"].Value = message.AcademicAdvisingId;
                adapter.SelectCommand.Parameters["@studentId"].Value = message.StudentId;
                adapter.SelectCommand.Parameters["@studentName"].Value = message.StudentName;
                adapter.SelectCommand.Parameters["@sendToInstructor"].Value = message.SendToInstructor;
                adapter.SelectCommand.Parameters["@messageSubject"].Value = message.MessageSubject;
                adapter.SelectCommand.Parameters["@message"].Value = message.MessageBody;

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

        public void DeleteAdvisingMessage(int id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(DeleteAcademicAdvisingInfoProcedure, conn)
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

        public AcademicAdvisingMessage GetAdvisingMessageDetail(int id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            AcademicAdvisingMessage message = null;

            try
            {
                var adapter = new SqlDataAdapter(GetAcademicAdvisingInfoProcedure, conn)
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

                message = new AcademicAdvisingMessage
                {
                    AcademicAdvisingId = id,
                    StudentId = dataSet.Tables[0].Rows[0]["studentId"].ToString(),
                    StudentName = dataSet.Tables[0].Rows[0]["studentName"].ToString(),
                    SendToInstructor = (bool)dataSet.Tables[0].Rows[0]["sendtoInstructor"],
                    MessageSubject = dataSet.Tables[0].Rows[0]["messageSubject"].ToString(),
                    MessageBody = dataSet.Tables[0].Rows[0]["message"].ToString()
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

        public List<AcademicAdvisingMessage> GetAdvisingMessages(string studentId, bool isInstructor, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var messageList = new List<AcademicAdvisingMessage>();

            try
            {
                var adapter = new SqlDataAdapter(GetAcademicAdvisingListProcedure, conn)
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
                    var message = new AcademicAdvisingMessage
                    {
                        AcademicAdvisingId = (int)dataSet.Tables[0].Rows[i]["id"],
                        StudentId = dataSet.Tables[0].Rows[i]["studentId"].ToString(),
                        StudentName = dataSet.Tables[0].Rows[i]["studentName"].ToString(),
                        SendToInstructor = (bool)dataSet.Tables[0].Rows[i]["sendtoInstructor"],
                        MessageSubject = dataSet.Tables[0].Rows[i]["messageSubject"].ToString(),
                        MessageBody = dataSet.Tables[0].Rows[i]["message"].ToString()
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
