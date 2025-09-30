#nullable enable
using System;
using System.Runtime.CompilerServices;

namespace Viewer.PoC.Core.Logging
{
    public interface ILogging
    {
        void Info(string message,
            [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0);
        void Warn(string message,
            [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0);
        void Error(string message,
            Exception? ex = null,
            [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0);
    }
}