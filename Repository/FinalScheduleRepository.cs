namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using IRepository;

    using POCO;

    public class FinalScheduleRepository : BaseRepository, IFinalScheduleRepository
    {
        private const string InsertFinalsScheduleInfo = "spInsertFinalsScheduleInfo";
        private const string UpdateFinalsScheduleInfo = "spUpdateFinalsScheduleInfo";
        private const string DeleteFinalsScheduleInfo = "spDeleteFinalsScheduleInfo";
        private const string GetFinalsScheduleInfo = "spGetFinalsScheduleInfo";
        private const string GetCourseFinalScheduleInfo = "spGetCourseFinalScheduleInfo";

        public FinalSchedule GetCourseFinalSchedule(int scheduleId, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            FinalSchedule finalSchedule = null;
            try
            {
                var adapter = new SqlDataAdapter(GetCourseFinalScheduleInfo, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@schedule_id"].Value = scheduleId;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                finalSchedule = new FinalSchedule
                {
                    Schedule_id = Convert.ToInt32(dataSet.Tables[0].Rows[0]["schedule_id"].ToString()),
                    FinalLocation = dataSet.Tables[0].Rows[0]["final_location"].ToString(),
                    FinalTime = dataSet.Tables[0].Rows[0]["f_time"].ToString(),
                    Title = dataSet.Tables[0].Rows[0]["title"].ToString()
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

            return finalSchedule;
        }

        public void InsertFinalSchedule(FinalSchedule finalSchedule, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(InsertFinalsScheduleInfo, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@final_location", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@f_time", SqlDbType.Time));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@title", SqlDbType.VarChar, 50));

                adapter.SelectCommand.Parameters["@schedule_id"].Value = finalSchedule.Schedule_id;
                adapter.SelectCommand.Parameters["@final_location"].Value = finalSchedule.FinalLocation;
                adapter.SelectCommand.Parameters["@f_time"].Value = finalSchedule.FinalTime;
                adapter.SelectCommand.Parameters["@title"].Value = finalSchedule.Title;

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

        public List<FinalSchedule> GetFinalSchedule(ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var finalScheduleList = new List<FinalSchedule>();

            try
            {
                var adapter = new SqlDataAdapter(GetFinalsScheduleInfo, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var finalScheduleContainer = new FinalSchedule
                    {
                        Schedule_id = Convert.ToInt32(dataSet.Tables[0].Rows[i]["schedule_id"].ToString()),
                        FinalLocation = dataSet.Tables[0].Rows[i]["final_location"].ToString(),
                        FinalTime = dataSet.Tables[0].Rows[i]["f_time"].ToString(),
                        Title = dataSet.Tables[0].Rows[i]["title"].ToString()
                    };

                    finalScheduleList.Add(finalScheduleContainer);
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

            return finalScheduleList;
        }

        public void UpdateFinalSchedule(FinalSchedule finalSchedule, ref List<string> errors)
    {
        var conn = new SqlConnection(ConnectionString);
        try
        {
            var adapter = new SqlDataAdapter(UpdateFinalsScheduleInfo, conn)
            {
                SelectCommand = { CommandType = CommandType.StoredProcedure }
            };

            adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("@final_location", SqlDbType.VarChar, 20));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("@f_time", SqlDbType.Time));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("@title", SqlDbType.VarChar, 50));

            adapter.SelectCommand.Parameters["@schedule_id"].Value = finalSchedule.Schedule_id;
            adapter.SelectCommand.Parameters["@final_location"].Value = finalSchedule.FinalLocation;
            adapter.SelectCommand.Parameters["@f_time"].Value = finalSchedule.FinalTime;
            adapter.SelectCommand.Parameters["@title"].Value = finalSchedule.Title;

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

        public void DeleteFinalSchedule(int scheduleId, ref List<string> errors)
    {
        var conn = new SqlConnection(ConnectionString);

        try
        {
            var adapter = new SqlDataAdapter(DeleteFinalsScheduleInfo, conn)
            {
                SelectCommand =
                {
                    CommandType =
                        CommandType
                        .StoredProcedure
                }
            };

            adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));

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
