﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Diagnostics;
using Development;



namespace ITM_Semiconductor
{
    class DbRead
    {
        public const String QR1_STATUS_KEY = "run";
        public const String SCANNER_STATUS_KEY = "scanner";
        private static MyLogger logger = new MyLogger("DbRead");
        public static List<AlarmInfo> GetLatestAlarms(int limit)
        {
            var ret = new List<AlarmInfo>();

            using (var conn = Dba.GetConnection())
            {
                var sql = "SELECT * FROM alarm_log ORDER BY created_time DESC LIMIT @limit";
                using (var sqlCmd = conn.CreateCommand())
                {
                    try
                    {
                        sqlCmd.CommandText = sql;
                        sqlCmd.Parameters.AddWithValue("@limit", limit);
                        conn.Open();
                        using (var reader = sqlCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var x = new AlarmInfo();
                                Object obj;

                                if ((obj = reader["id"]) != DBNull.Value)
                                {
                                    x.id = int.Parse(obj.ToString());
                                }
                                if ((obj = reader["created_time"]) != DBNull.Value)
                                {
                                    x.createdTime = DateTime.Parse(obj.ToString());
                                }
                                if ((obj = reader["alarm_code"]) != DBNull.Value)
                                {
                                    x.alarmCode = int.Parse(obj.ToString());
                                }
                                if ((obj = reader["message"]) != DBNull.Value)
                                {
                                    x.message = obj.ToString();
                                }
                                if ((obj = reader["solution"]) != DBNull.Value)
                                {
                                    x.solution = obj.ToString();
                                }
                                if ((obj = reader["mode"]) != DBNull.Value)
                                {
                                    x.mode = int.Parse(obj.ToString());
                                }
                                ret.Add(x);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Create("GetLatestAlarms error:" + ex.Message, LogLevel.Error);
                    }
                }
            }

            return ret;
        }
        public static List<AlarmInfo> GetAlarm(int pageIndex, int pageSize)
        {
            var ret = new List<AlarmInfo>();

            using (var conn = Dba.GetConnection())
            {
                var sql = "SELECT * FROM alarm_log LIMIT @limit OFFSET @offset";
                using (var sqlCmd = conn.CreateCommand())
                {
                    try
                    {
                        sqlCmd.CommandText = sql;
                        sqlCmd.Parameters.AddWithValue("@limit", pageSize);
                        sqlCmd.Parameters.AddWithValue("@offset", pageIndex * pageSize);
                        conn.Open();
                        using (var reader = sqlCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var x = new AlarmInfo();
                                Object obj;

                                if ((obj = reader["id"]) != DBNull.Value)
                                {
                                    x.id = int.Parse(obj.ToString());
                                }
                                if ((obj = reader["created_time"]) != DBNull.Value)
                                {
                                    x.createdTime = DateTime.Parse(obj.ToString());
                                }
                                if ((obj = reader["alarm_code"]) != DBNull.Value)
                                {
                                    x.alarmCode = int.Parse(obj.ToString());
                                }
                                if ((obj = reader["message"]) != DBNull.Value)
                                {
                                    x.message = obj.ToString();
                                }
                                if ((obj = reader["solution"]) != DBNull.Value)
                                {
                                    x.solution = obj.ToString();
                                }
                                if ((obj = reader["mode"]) != DBNull.Value)
                                {
                                    x.mode = int.Parse(obj.ToString());
                                }
                                ret.Add(x);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Create("GetEvents error:" + ex.Message, LogLevel.Error);
                    }
                }
            }

            return ret;
        }
        public static int CountAlarm()
        {
            int ret = 0;

            using (var conn = Dba.GetConnection())
            {
                var sql = "SELECT COUNT(*) FROM alarm_log";
                using (var sqlCmd = conn.CreateCommand())
                {
                    try
                    {
                        sqlCmd.CommandText = sql;
                        conn.Open();
                        using (var reader = sqlCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Object obj;

                                if ((obj = reader[0]) != DBNull.Value)
                                {
                                    ret = int.Parse(obj.ToString());
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Create("CountAlarm error:" + ex.Message, LogLevel.Error);
                    }
                }
            }

            return ret;
        }
        public static int CountEvents()
        {
            int ret = 0;

            using (var conn = Dba.GetConnection())
            {
                var sql = "SELECT COUNT(*) FROM event_log";
                using (var sqlCmd = conn.CreateCommand())
                {
                    try
                    {
                        sqlCmd.CommandText = sql;
                        conn.Open();
                        using (var reader = sqlCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Object obj;

                                if ((obj = reader[0]) != DBNull.Value)
                                {
                                    ret = int.Parse(obj.ToString());
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Create("CountEvents error:" + ex.Message, LogLevel.Error);
                    }
                }
            }

            return ret;
        }
        public static List<UserLog> GetUserLogs(DateTime day, int pageIndex, int pageSize)
        {
            var ret = new List<UserLog>();

            using (var conn = Dba.GetConnection())
            {
                var sql = "SELECT * FROM user_log WHERE date(created_time) = date(@date) LIMIT @limit OFFSET @offset";
                using (var sqlCmd = conn.CreateCommand())
                {
                    try
                    {
                        sqlCmd.CommandText = sql;
                        sqlCmd.Parameters.AddWithValue("@date", day.ToString("yyyy-MM-dd"));
                        sqlCmd.Parameters.AddWithValue("@limit", pageSize);
                        sqlCmd.Parameters.AddWithValue("@offset", pageIndex * pageSize);
                        conn.Open();
                        using (var reader = sqlCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var x = new UserLog();
                                Object obj;

                                if ((obj = reader["id"]) != DBNull.Value)
                                {
                                    x.Id = int.Parse(obj.ToString());
                                }
                                if ((obj = reader["username"]) != DBNull.Value)
                                {
                                    x.Username = obj.ToString();
                                }
                                if ((obj = reader["created_time"]) != DBNull.Value)
                                {
                                    x.CreatedTime = obj.ToString();
                                }
                                if ((obj = reader["action"]) != DBNull.Value)
                                {
                                    x.Action = int.Parse(obj.ToString());
                                }
                                if ((obj = reader["message"]) != DBNull.Value)
                                {
                                    x.Message = obj.ToString();
                                }
                                ret.Add(x);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Create("GetUserLogs error:" + ex.Message, LogLevel.Error);
                    }
                }
            }

            return ret;
        }
    
        public static int CountUserLogs(DateTime day)
        {
            int ret = 0;

            using (var conn = Dba.GetConnection())
            {
                var sql = "SELECT COUNT(*) FROM user_log WHERE date(created_time) = date(@date) ";
                using (var sqlCmd = conn.CreateCommand())
                {
                    try
                    {
                        sqlCmd.CommandText = sql;
                        sqlCmd.Parameters.AddWithValue("@date", day.ToString("yyyy-MM-dd"));
                        conn.Open();
                        using (var reader = sqlCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Object obj;

                                if ((obj = reader[0]) != DBNull.Value)
                                {
                                    ret = int.Parse(obj.ToString());
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Create("CountUserLogs error:" + ex.Message, LogLevel.Error);
                    }
                }
            }

            return ret;
        }

        public static List<EventLogg> GetEvents(int pageIndex, int pageSize)
        {
            var ret = new List<EventLogg>();
            using (var conn = Dba.GetConnection())
            {
                var sql = "SELECT * FROM event_log LIMIT @limit OFFSET @offset";
                using (var sqlCmd = conn.CreateCommand())
                {
                    try
                    {
                        sqlCmd.CommandText = sql;
                        sqlCmd.Parameters.AddWithValue("@limit", pageSize);
                        sqlCmd.Parameters.AddWithValue("@offset", pageIndex * pageSize);
                        conn.Open();
                        using (var reader = sqlCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var x = new EventLogg();
                                Object obj;

                                if ((obj = reader["id"]) != DBNull.Value)
                                {
                                    x.Id = int.Parse(obj.ToString());
                                }
                                if ((obj = reader["created_time"]) != DBNull.Value)
                                {
                                    x.CreatedTime = obj.ToString();
                                }
                                if ((obj = reader["event_type"]) != DBNull.Value)
                                {
                                    x.EventType = obj.ToString();
                                }
                                if ((obj = reader["message"]) != DBNull.Value)
                                {
                                    x.Message = obj.ToString();
                                }
                                ret.Add(x);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Create("GetEvents error:" + ex.Message, LogLevel.Error);
                    }
                }
            }
            return ret;
        }

    }
}
