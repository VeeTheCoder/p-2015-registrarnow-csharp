namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using IRepository;

    using POCO;

    public class GradeDistributionRepository : BaseRepository, IGradeDistributionRepository
    {
        private const string InsertGradeDistributionInfo = "spInsertGradeDistributionInfo";
        private const string UpdateGradeDistributionInfo = "spUpdateGradeDistributionInfo";
        private const string DeleteGradeDistributionInfo = "spDeleteGradeDistributionInfo";
        private const string GetGradeDistributionInfo = "spGetGradeDistributionInfo";
        private const string GetCourseGradeDistributionInfo = "spGetCourseGradeDistributionInfo";

        public GradeDistribution GetCourseGradeDistribution(int scheduleId, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            GradeDistribution gradeDistribution = null;
            try
            {
                var adapter = new SqlDataAdapter(GetCourseGradeDistributionInfo, conn)
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

                gradeDistribution = new GradeDistribution
                {
                    Schedule_id = Convert.ToInt32(dataSet.Tables[0].Rows[0]["schedule_id"].ToString()),
                    Grade_Distribution = dataSet.Tables[0].Rows[0]["grade_distribution"].ToString(),
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

            return gradeDistribution;
        }

        public void InsertGradeDistribution(GradeDistribution gradeDistribution, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(InsertGradeDistributionInfo, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@grade_distribution", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@title", SqlDbType.VarChar, 50));

                adapter.SelectCommand.Parameters["@schedule_id"].Value = gradeDistribution.Schedule_id;
                adapter.SelectCommand.Parameters["@grade_distribution"].Value = gradeDistribution.Grade_Distribution;
                adapter.SelectCommand.Parameters["@title"].Value = gradeDistribution.Title;

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

        public List<GradeDistribution> GetGradeDistribution(ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var gradeDistributionList = new List<GradeDistribution>();

            try
            {
                var adapter = new SqlDataAdapter(GetGradeDistributionInfo, conn)
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
                    var gradeDistributionContainer = new GradeDistribution
                    {
                        Schedule_id = Convert.ToInt32(dataSet.Tables[0].Rows[i]["schedule_id"].ToString()),
                        Grade_Distribution = dataSet.Tables[0].Rows[i]["grade_distribution"].ToString(),
                        Title = dataSet.Tables[0].Rows[i]["title"].ToString()
                    };

                    gradeDistributionList.Add(gradeDistributionContainer);
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

            return gradeDistributionList;
        }

        public void UpdateGradeDistribution(GradeDistribution gradeDistribution, ref List<string> errors)
    {
        var conn = new SqlConnection(ConnectionString);
        try
        {
            var adapter = new SqlDataAdapter(UpdateGradeDistributionInfo, conn)
            {
                SelectCommand = { CommandType = CommandType.StoredProcedure }
            };

            adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("@grade_distribution", SqlDbType.VarChar, 50));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("@title", SqlDbType.VarChar, 50));

            adapter.SelectCommand.Parameters["@schedule_id"].Value = gradeDistribution.Schedule_id;
            adapter.SelectCommand.Parameters["@grade_distribution"].Value = gradeDistribution.Grade_Distribution;
            adapter.SelectCommand.Parameters["@title"].Value = gradeDistribution.Title;

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

        public void DeleteGradeDistribution(int scheduleId, ref List<string> errors)
    {
        var conn = new SqlConnection(ConnectionString);

        try
        {
            var adapter = new SqlDataAdapter(DeleteGradeDistributionInfo, conn)
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
