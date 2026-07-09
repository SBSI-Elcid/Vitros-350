using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Vitros350.Classes
{
    public static class ErrorLogger
    {
        public static void LogError(string source, Exception ex = null)
        {
            try
            {
                string logFolder = Path.Combine(Application.StartupPath, "Logs");
                string logPath = Path.Combine(logFolder, $"ErrorLog_{DateTime.Now:yyyyMMdd}.txt");
                string timeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                StringBuilder logEntry = new StringBuilder();

                if (!Directory.Exists(logFolder))
                {
                    Directory.CreateDirectory(logFolder);
                }

                logEntry.AppendLine("========================================");
                logEntry.AppendLine($"[{timeStamp}]");
                logEntry.AppendLine($"Source    : {source}");
                logEntry.AppendLine($"Message   : {ex.Message}");
                logEntry.AppendLine($"Type      : {ex.GetType().FullName}");
                logEntry.AppendLine($"StackTrace: {ex.StackTrace}");

                if (ex.InnerException != null)
                {
                    logEntry.AppendLine($"InnerException: {ex.InnerException.Message}");
                }
                logEntry.AppendLine("========================================");
                logEntry.AppendLine();

                File.AppendAllText(logPath, logEntry.ToString());
            }
            catch (Exception logEx)
            {
                Debug.WriteLine($"Failed to log error.: {logEx}");
            }

        }

    }
}
