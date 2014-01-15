namespace TD1990.Libs.TDLyutil
{
    using System.Threading;

    public class ThreadUtils
    {
        public static void ThreadPoolQueueWorkItem(WaitCallback callBack, object userState = null)
        {
            ThreadPool.QueueUserWorkItem(callBack, userState);
        }
    }
}
