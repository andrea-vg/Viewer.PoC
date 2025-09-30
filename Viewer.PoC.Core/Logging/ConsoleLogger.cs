#nullable enable
using System;

namespace Viewer.PoC.Core.Logging
{
    public class ConsoleLogger : ILogging
    {
        public void Info(string message, string memberName = "", string filePath = "", int lineNumber = 0)
        {
            System.Diagnostics.Debug.WriteLine($"[INFO] {DateTime.Now:HH:mm:ss} [{System.IO.Path.GetFileName(filePath)}:{memberName}:{lineNumber}] {message}");
        }

        public void Warn(string message, string memberName = "", string filePath = "", int lineNumber = 0)
        {
            System.Diagnostics.Debug.WriteLine($"[WARN] {DateTime.Now:HH:mm:ss} [{System.IO.Path.GetFileName(filePath)}:{memberName}:{lineNumber}] {message}");
        }

        public void Error(string message, Exception? ex = null, string memberName = "", string filePath = "", int lineNumber = 0)
        {
            System.Diagnostics.Debug.WriteLine($"[ERROR] {DateTime.Now:HH:mm:ss} [{System.IO.Path.GetFileName(filePath)}:{memberName}:{lineNumber}] {message}");
            if (ex != null)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }
}