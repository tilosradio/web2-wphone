namespace TD1990.Libs.TDLyutil.Interfaces.DebugTools
{
    using System;

    public interface ILogger
    {
        void Error(string callStack, string message, Exception exception);
        void Error(string callStack, Exception exception);
        void Info(string callStack, string message);
        void Info(string message);
        void Error(string message);
        void Clear();
        string ToString();
    }
}
