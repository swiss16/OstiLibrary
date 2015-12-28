using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OstiLibrary.Helper.Configuration;

namespace OstiLibrary.Helper.Logging
{
    public static class Logger
    {
        private static int _ALL = 3;
        private static int _WARNING = 2;
        private static int _ERROR = 1;
        private static int _OFF = 0;
        private static int logLevel = ConfigWrapper.LogLevel;
        private static string logDirectory = ConfigWrapper.LogDirectory;

        public static int LogLevel
        {
            get { return logLevel; }
            set { logLevel = value; }
        }
        public static string LogDirectory
        {
            get { return logDirectory; }
            set { logDirectory = value; }
        }

        internal static int ALL
        {
            get { return _ALL; }
        }
        internal static int ERROR
        {
            get { return _ERROR; }
        }
        internal static int INFO
        {
            get { return _WARNING; }
        }
        internal static int OFF
        {
            get { return _OFF; }
        }


        public static void Append(string message, int level)
        {
            try
            {


                if (logLevel >= level)
                {
                    DateTime dt = DateTime.Now;
                    string filePath = Path.Combine(logDirectory, dt.ToString("yyyyMMdd") + ".log");
                    if (!Directory.Exists(logDirectory))
                    {
                        Directory.CreateDirectory(logDirectory);
                    }
                    if (!File.Exists(filePath))
                    {
                        FileStream fs = File.Create(filePath);
                        fs.Close();
                    }
                    try
                    {
                        StreamWriter sw = File.AppendText(filePath);
                        sw.WriteLine(dt.ToString("hh:mm:ss") + " | " + message);
                        sw.Flush();
                        sw.Close();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Fehler beim Logger");
                        Debug.WriteLine(e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Fehler beim Logger");
                Debug.WriteLine(e.Message);
            }
        }

        public static void Append(string message, LogLevel level)
        {
            Append(message, (int)level);
        }
    }

    public enum LogLevel
    {
        Error = 1,
        Warning = 2,
        Information = 3
    }
}

