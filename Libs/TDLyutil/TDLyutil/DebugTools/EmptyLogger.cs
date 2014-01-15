namespace TD1990.Libs.TDLyutil.DebugTools
{
    using System;
    using TD1990.Libs.TDLyutil.Interfaces.DebugTools;

    public class EmptyLogger: ILogger
    {
        public void Error(string callStack, string message, Exception exception)
        {
        }

        public void Error(string callStack, Exception exception)
        {
        }

        public void Info(string callStack, string message)
        {
        }

        public void Info(string message)
        {
        }

        public void Error(string message)
        {
        }

        public override string ToString()
        {
            return null;
        }

        public void Clear()
        {
        }
    }
}
