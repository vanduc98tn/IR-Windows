using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Development
{
    class MyLogger
    {
        private static Object objLock = new Object();

        private String prefix = "";

        public MyLogger(String prefix)
        {
            this.prefix = prefix;
        }

        public void Create(String content,LogLevel logLevel)
        {
            // Get FilePath:
            var currentDate = DateTime.Now.ToString("yyyy-MM");

            var fileName = String.Format("{0}.log", DateTime.Now.ToString("yyyy-MM-dd"));
            var folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "DebugLogs", currentDate);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            var filePath = System.IO.Path.Combine(folder, fileName);

            lock (objLock)
            {
                try
                {
                    var log = String.Format("\r\n{0}-[{1}]-[{3}]:{2}", DateTime.Now.ToString("HH:mm:ss.ff"), this.prefix, content,logLevel.ToString());

                    System.Diagnostics.Debug.Write(log);

                    using (var strWriter = new StreamWriter(filePath, true))
                    {
                        strWriter.Write(log);
                        strWriter.Flush();
                    }
                }
                catch (Exception ex)
                {
                    Debug.Write("\r\nMyLoger.Create error:" + ex.Message);
                }
            }
        }

        public void CreateDataVision(String content, LogLevel logLevel)
        {
            // Get FilePath:
            var currentDate = DateTime.Now.ToString("yyyy-MM");

            var fileName = String.Format("{0}.log", DateTime.Now.ToString("yyyy-MM-dd"));
            var folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "DebugLogsVision", currentDate);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            var filePath = System.IO.Path.Combine(folder, fileName);

            lock (objLock)
            {
                try
                {
                    var log = String.Format("\r\n{0}-[{1}]-[{3}]:{2}", DateTime.Now.ToString("HH:mm:ss.ff"), this.prefix, content,logLevel.ToString());

                    System.Diagnostics.Debug.Write(log);

                    using (var strWriter = new StreamWriter(filePath, true))
                    {
                        strWriter.Write(log);
                        strWriter.Flush();
                    }
                }
                catch (Exception ex)
                {
                    Debug.Write("\r\nMyLoger.Create error:" + ex.Message);
                }
            }
        }
        public void Create01(String content, LogLevel logLevel)
        {
            // Get FilePath:
            var currentDate = DateTime.Now.ToString("yyyy-MM");

            var fileName = String.Format("{0}.log", DateTime.Now.ToString("yyyy-MM-dd"));
            var folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "DebugTester", currentDate);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            var filePath = System.IO.Path.Combine(folder, fileName);

            lock (objLock)
            {
                try
                {
                    var log = String.Format("\r\n{0}-[{1}]-[{3}]:{2}", DateTime.Now.ToString("HH:mm:ss.ff"), this.prefix, content, logLevel.ToString());

                    System.Diagnostics.Debug.Write(log);

                    using (var strWriter = new StreamWriter(filePath, true))
                    {
                        strWriter.Write(log);
                        strWriter.Flush();
                    }
                }
                catch (Exception ex)
                {
                    Debug.Write("\r\nMyLoger.Create error:" + ex.Message);
                }
            }
        }
    }
}
