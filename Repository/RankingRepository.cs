namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using IRepository;

    using POCO;

    public class RankingRepository : BaseRepository, IRankingRepository
    {
        private const string InsertRankingInfoProcedure = "spInsertRankingInfo"; // C
        private const string GetRankingInfoProcedure = "spGetRankingInfo"; // R
        private const string UpdateRankingInfoProcedure = "spUpdateRankingInfo"; // U
        private const string DeleteRankingInfoProcedure = "spDeleteRankingInfo"; // D
        private const string GetRankingListProcedure = "spGetRankingList"; // R

        public void InsertRanking(Ranking ranking, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(InsertRankingInfoProcedure, conn)
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
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@ranking", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@student_id"].Value = ranking.StudentId;
                adapter.SelectCommand.Parameters["@schedule_id"].Value = ranking.ScheduleId;
                adapter.SelectCommand.Parameters["@ranking"].Value = ranking.Rank;

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

        public Ranking GetRankingInfo(string studentId, int scheduleId, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            Ranking ranking = null;

            try
            {
                var adapter = new SqlDataAdapter(GetRankingInfoProcedure, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@student_id"].Value = studentId;
                adapter.SelectCommand.Parameters["@schedule_id"].Value = scheduleId;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                ranking = new Ranking
                {
                    StudentId = studentId,
                    ScheduleId = scheduleId,
                    Rank = Convert.ToInt32(dataSet.Tables[0].Rows[0]["ranking"].ToString())
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

            return ranking;
        }

        public void UpdateRanking(Ranking ranking, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(UpdateRankingInfoProcedure, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@ranking", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@student_id"].Value = ranking.StudentId;
                adapter.SelectCommand.Parameters["@schedule_id"].Value = ranking.ScheduleId;
                adapter.SelectCommand.Parameters["@ranking"].Value = ranking.Rank;

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

        public void DeleteRanking(string studentId, int scheduleId, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(DeleteRankingInfoProcedure, conn)
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

        public List<Ranking> GetRankings(int scheduleId, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var rankingList = new List<Ranking>();

            try
            {
                var adapter = new SqlDataAdapter(GetRankingListProcedure, conn)
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

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var ranking = new Ranking
                    {
                        StudentId = dataSet.Tables[0].Rows[i]["student_id"].ToString(),
                        ScheduleId = scheduleId,
                        Rank = Convert.ToInt32(dataSet.Tables[0].Rows[i]["ranking"].ToString())
                    };
                    rankingList.Add(ranking);
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

            return rankingList;
        }
    }
}