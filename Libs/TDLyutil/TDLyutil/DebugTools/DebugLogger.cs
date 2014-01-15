namespace TD1990.Libs.TDLyutil.DebugTools
{
    using System;
    using System.Text;
    using System.Threading;
    using TD1990.Libs.TDLyutil.Interfaces.DebugTools;
    using System.Linq;

    public class DebugLogger: ILogger
    {
        private StringBuilder Messages;
        private int Step;
        private object ThisLock;

        private const string InfoType = "I";
        private const string ErrorType = "E";

        public DebugLogger()
        {
            ThisLock = new object();
            Messages = new StringBuilder();
            Step = 0;
        }

        public void Error(string callStack, string message, Exception exception)
        {
            try
            {
                string exmsg = exception != null ? exception.ToString() : "";
                Add(ErrorType, 
                    callStack 
                    + " m:" + message 
                    + " ex:" + exmsg);
            }
            catch (Exception)
            {
            }
        }

        public void Error(string callStack, Exception exception)
        {
            try
            {
                string exmsg = exception != null ? exception.ToString() : "";
                Add(ErrorType,
                    callStack
                    + " ex:" + exmsg);
            }
            catch (Exception)
            {
            }
        }

        public void Error(string message)
        {
            try
            {
                Add(ErrorType, message);
            }
            catch (Exception)
            {
            }
        }

        public void Info(string callStack, string message)
        {
            try
            {
                Add(InfoType, callStack + " " + message);
            }
            catch (Exception)
            {
            }
        }

        public void Info(string message)
        {
            try
            {
                Add(InfoType, message);
            }
            catch (Exception)
            {
            }
        }


        private void Add(string type, string message)
        {
            lock (ThisLock)
            {
                int threadid = Thread.CurrentThread.ManagedThreadId;
                string threadname;
                try
                {
                    threadname = "-" + Thread.CurrentThread.Name;                                        
                }
                catch (System.Exception)
                {
                    threadname = "";
                }
                try
                {
                    Messages.AppendLine(
                        type 
                        + Step.ToString("D3") 
                        + " [" + threadid.ToString() + threadname + "] " 
                        + message);
                }
                catch (Exception)
                {
                } 
                Step++;
            }
        }

        public override string ToString()
        {
            lock (ThisLock)
            {
                return Messages.ToString();
            }
        }

        public void Clear()
        {
            lock (ThisLock)
            {
                Messages.Clear();
            }
        }
    }
}
