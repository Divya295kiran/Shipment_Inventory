using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.ExceptionHandler
{
    public static class ErrorHandler
    {
       
        private static readonly string LogDirectory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Inventory", "Logs");
        private static readonly string LogFilePath = Path.Combine(LogDirectory, "ErrorLog.txt");

        // Log error to a file
        public static void LogError(Exception ex)
        {
            try
            {

                // Ensure the Logs directory exists
                if (!Directory.Exists(LogDirectory))
                {
                    Directory.CreateDirectory(LogDirectory);
                }
                string errorMessage = $"[{DateTime.Now}] - {ex.Message}{Environment.NewLine}{ex.StackTrace}{Environment.NewLine}";
                File.AppendAllText(LogFilePath, errorMessage);
            }
            catch
            {
                // If logging fails, write to console (or handle as fallback)
                Console.WriteLine("Failed to log error.");
            }
        }

        
        public static string GetErrorMessage(Exception ex)
        {
           
            return ex.ToString();
        }
    }
}
