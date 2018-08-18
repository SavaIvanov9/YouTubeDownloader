using System;
using System.IO;
using System.Reflection;
using System.Text;
using YD.Common.Models;
using YD.Services.Abstraction;

namespace YD.Services.ErrorLogging
{
    public class ErrorLoggingService : IErrorLoggingService
    {
        private const string ReportsFolderName = "../ErrorLogs";

        public bool IsEnabled { get; set; } = false;

        public string LogError(Error error)
        {
            if (this.IsEnabled)
            {
                this.CheckDirectory();

                string currentLog = $"{error.Date:dd-MM-yyyy hh;mm;ss tt}.txt";

                string path = Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    $@"{ReportsFolderName}\{currentLog}");

                this.CreateLogFile(path, error);

                return currentLog;
            }

            return null;
        }

        private void CheckDirectory()
        {
            string dirName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string pathString = Path.Combine(dirName, ReportsFolderName);

            if (!Directory.Exists(pathString))
            {
                Directory.CreateDirectory(pathString);
            }
        }

        private void CreateLogFile(string path, Error error)
        {
            if (!File.Exists(path))
            {
                using (StreamWriter file = File.CreateText(path))
                {
                    file.Write(this.CreateContent(error));
                }
            }
        }

        private StringBuilder CreateContent(Error error)
        {
            StringBuilder report = new StringBuilder();

            report.AppendLine("Title               : " + error.Exception.Message);
            report.AppendLine("Date                : " + DateTime.Now);
            report.AppendLine("Machine Name        : " + Environment.MachineName);
            report.AppendLine("UserDomain Name     : " + Environment.UserDomainName);
            report.AppendLine("Login User Name     : " + Environment.UserName);
            report.AppendLine("OS Version          : " + Environment.OSVersion);
            report.AppendLine("CLR Version         : " + Environment.Version);
            report.AppendLine("Assembly Name       : " + Assembly.GetEntryAssembly().GetName());
            report.AppendLine("Assembly Location   : " + Assembly.GetEntryAssembly().Location);

            report.AppendLine(Environment.NewLine +
                "-----------------<Report Content>-----------------");
            report.AppendLine(error.Exception.ToString());

            return report;
        }
    }
}