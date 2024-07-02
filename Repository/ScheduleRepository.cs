namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using IRepository;

    using POCO;

    public class ScheduleRepository : BaseRepository, IScheduleRepository
    {
        private const string InsertScheduleInfoProcedure = "spInsertScheduleInfo";
        private const string GetScheduleListProcedure = "spGetScheduleList";

        public void InsertSchedule(Schedule schedule, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(InsertScheduleInfoProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@year", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@quarter", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@session", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_day_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_time_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@instructor_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@course_id"].Value = schedule.Course.CourseId;
                adapter.SelectCommand.Parameters["@year"].Value = schedule.Year;
                adapter.SelectCommand.Parameters["@quarter"].Value = schedule.Quarter;
                adapter.SelectCommand.Parameters["@session"].Value = schedule.Session;
                adapter.SelectCommand.Parameters["@schedule_day_id"].Value = DBNull.Value;
                adapter.SelectCommand.Parameters["@schedule_time_id"].Value = DBNull.Value;
                adapter.SelectCommand.Parameters["@instructor_id"].Value = DBNull.Value;

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

        public List<Schedule> GetScheduleList(string year, string quarter, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var scheduleList = new List<Schedule>();

            try
            {
                var adapter = new SqlDataAdapter(GetScheduleListProcedure, conn);

                if (year.Length > 0)
                {
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@year", SqlDbType.Int));
                    adapter.SelectCommand.Parameters["@year"].Value = year;
                }

                if (quarter.Length > 0)
                {
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@quarter", SqlDbType.VarChar, 25));
                    adapter.SelectCommand.Parameters["@quarter"].Value = quarter;
                }

                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var schedule = new Schedule
                    {
                        ScheduleId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["schedule_id"].ToString()),
                        Year = dataSet.Tables[0].Rows[i]["year"].ToString(),
                        Quarter = dataSet.Tables[0].Rows[i]["quarter"].ToString(),
                        Session = dataSet.Tables[0].Rows[i]["session"].ToString(),
                        Course = new Course
                        {
                            CourseId = dataSet.Tables[0].Rows[i]["course_id"].ToString(),
                            Title = dataSet.Tables[0].Rows[i]["course_title"].ToString(),
                            Description = dataSet.Tables[0].Rows[i]["course_description"].ToString(),
                        }
                    };
                    scheduleList.Add(schedule);
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

            return scheduleList;
        }
    }
}
