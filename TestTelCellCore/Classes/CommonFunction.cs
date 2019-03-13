using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using TestTelCellCore.Properties;

namespace TestTelCellCore
{
    public static class CommonFunction
    {
        
        public  static string LogPath
        {

            get
            {
                string _LogPath = string.Empty;



                _LogPath= Settings.Default.LogPath;

                _LogPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), ""))
                    + _LogPath;

                return _LogPath;
            }
        }

        public static void WriteLog(string MetodFrom, string Message)
        {
            string path = LogPath;



            if (!File.Exists(path))
            {

                FileInfo fileInfo = new FileInfo(path);
                if (!fileInfo.Exists)
                    Directory.CreateDirectory(fileInfo.Directory.FullName);


                File.Create(path).Close();

            }

            try
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine($"\r\nLog Time: {DateTime.Now.ToString()}");

                    string logMetodFrom = $"Log From: {MetodFrom}";
                    string log = $"Message: {Message}";
                    sw.WriteLine(logMetodFrom);
                    sw.WriteLine(log);
                    sw.WriteLine("_____________________________________________________________");
                    sw.Flush();
                    sw.Close();
                }
            }

            //catch (Exception e) { }
            catch { }
            finally { }

        }







    }
}