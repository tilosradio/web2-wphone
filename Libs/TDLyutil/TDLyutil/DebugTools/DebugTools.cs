namespace TD1990.Libs.TDLyutil.DebugTools
{
    using System.Windows;

    public class DebugTools
    {

        public static void DebugMessageBoxShow(string message, string caption)
        {
#if DEBUG
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                MessageBox.Show(message, caption, MessageBoxButton.OK);
            });
#endif
        }
    }
}
