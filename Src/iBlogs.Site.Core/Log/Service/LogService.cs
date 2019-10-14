using System;
using System.Collections.Generic;
using iBlogs.Site.Core.Common;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.Common.Request;
using iBlogs.Site.Core.Common.Response;
using iBlogs.Site.Core.Log.Dto;
using iBlogs.Site.Core.MailKit;
using iBlogs.Site.Core.Option;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog.Debugging;
using Serilog.Events;

namespace iBlogs.Site.Core.Log.Service
{
    public class LogService : ILogService
    {
        private readonly IConfiguration _configuration;
        private static IMailService _mailService;

        public LogService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static void ErrorLogEventCallBack(LogEvent logEvent)
        {
            try
            {
                if (_mailService == null)
                {
                    _mailService = ServiceFactory.GetService<IMailService>();
                }

                var adminEmail = ConfigData.Get(ConfigKey.AdminEmail);

                if(adminEmail.IsNullOrEmpty()) return;

                _mailService.Publish(new MailContext
                {
                    To = new []{adminEmail},
                    Subject = "错误日志告警",
                    Content = JsonConvert.SerializeObject(logEvent)
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
           
        }

        public Page<LogResponse> GetPage(PageParam page)
        {
            var total = 0;
            var responses = new List<LogResponse>();
            try
            {
                using (var con = GetSqlConnection())
                {
                    using (var cmd = new MySqlCommand("SELECT COUNT(1) from `Logs` where LEVEL in ('Error','Warning')", con))
                    {
                        total = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                var sql = $"select `Level`,`Message`,`Timestamp` from `Logs` where LEVEL in ('Error','Warning') ORDER BY `Timestamp` DESC limit {(page.Page - 1) * page.Limit},{page.Limit}";
                
                using (var con = GetSqlConnection())
                {
                    using (var cmd = new MySqlCommand(sql, con))
                    {
                        MySqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            responses.Add(new LogResponse
                            {
                                Level = reader.GetString("Level"),
                                Message = reader.GetString("Message"),
                                Timestamp = DateTime.Parse(reader.GetString("Timestamp"))
                            });
                        }
                    }
                }
            }
            catch 
            {
                responses.Add(new LogResponse
                {
                    Level = "Error",
                    Message = "没有找到日志表,请确认是否将日志保存在数据库中",
                    Timestamp = DateTime.Now
                });
            }
           

            return new Page<LogResponse>(total,page.Page,page.Limit,responses);
        }

        private MySqlConnection GetSqlConnection()
        {
            try
            {
                var conn = new MySqlConnection(_configuration.GetConnectionString("iBlogs"));
                conn.Open();

                return conn;
            }
            catch (Exception ex)
            {
                SelfLog.WriteLine(ex.Message);

                return null;
            }
        }
    }
}