using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace Development
{
    public static class Updater
    {
        static MyLogger logger = new MyLogger("Updater");

        public static bool CheckConnect()
        {
            string UrlFTP = UiManager.managerSetting.settingFTPUpdate.Url;
            string UseLogin = UiManager.managerSetting.settingFTPUpdate.UseLogin;
            string UseName = UiManager.managerSetting.settingFTPUpdate.UserName;
            string PassWord = UiManager.managerSetting.settingFTPUpdate.PassWord;

            if (CheckFTPConnection(UrlFTP, UseLogin, UseName, PassWord))
            {
                return true;
            }
            return false;
        }
        public static void ReadFileUpdate(out string Messenger,out string Ver,out bool Update)
        {
            Messenger = "";
            Ver = "";
            Update = false;

            string UrlFTP = UiManager.managerSetting.settingFTPUpdate.Url;
            string UseLogin = UiManager.managerSetting.settingFTPUpdate.UseLogin;
            string UseName = UiManager.managerSetting.settingFTPUpdate.UserName;
            string PassWord = UiManager.managerSetting.settingFTPUpdate.PassWord;
            string updateFilePath = System.IO.Path.Combine(UrlFTP, "Update.json");
            string json = "";

            json = ReadFileFromFtp(updateFilePath ,UseLogin, UseName, PassWord);
            if(json == null)
            {
                return;
            }    
            dynamic updateInfo = JsonConvert.DeserializeObject(json);
            Version newVersion = new Version(updateInfo.version.ToString());

            Ver = newVersion.ToString();

            Messenger = updateInfo.Messenger;
            Version currentVersion = Assembly.GetExecutingAssembly().GetName().Version;

            if (newVersion <= currentVersion) return;

            Update = true;
         
           
        }
        private static string ReadFileFromFtp(string ftpFilePath, string userLogin, string userName, string password)
        {
            try
            {
                // Tạo yêu cầu FTP để tải file
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpFilePath);
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                // Sử dụng đăng nhập ẩn danh (hoặc cung cấp thông tin đăng nhập nếu cần)
                request.Credentials = new NetworkCredential("anonymous", "");

                // Đọc file từ server FTP
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    // Đọc toàn bộ nội dung file
                    string fileContent = reader.ReadToEnd();
                    return fileContent;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tải file từ FTP: {ex.Message}");
                return null;
            }
        }
        private static bool CheckFTPConnection(string ftpUrl, string userLogin, string userName, string password)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Timeout = 2000;  // ⏳ Timeout 5 giây
                request.ReadWriteTimeout = 2000; // ⏳ Timeout đọc/ghi 5 giây

                if (userLogin == "Y")
                {
                    request.Credentials = new NetworkCredential(userName, password);
                }

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                logger.Create("Không Thể Kết Nối Tới FTP Server: " + ex.Message, LogLevel.Error);
                return false;
            }
        }
        public static void DownloadFTP()
        {
            string UrlFTP = UiManager.managerSetting.settingFTPUpdate.Url;
            string UseLogin = UiManager.managerSetting.settingFTPUpdate.UseLogin;
            string UseName = UiManager.managerSetting.settingFTPUpdate.UserName;
            string PassWord = UiManager.managerSetting.settingFTPUpdate.PassWord;
            string NameAppOpen = UiManager.managerSetting.settingFTPUpdate.NameAppOpen;

            var folder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Update");
            var filePath = System.IO.Path.Combine(folder, "Auto Update.exe");

            string appFolderPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);  // Thư mục của ứng dụng hiện tại

            Process.Start(filePath, $"\"{UrlFTP}\" \"{appFolderPath}\" \"{NameAppOpen}\" \"{UseLogin}\" \"{UseName}\" \"{PassWord}\"");

            UiManager.Instance.wndMain.MainWindow_Closed(null,null);

        }
    }
}
