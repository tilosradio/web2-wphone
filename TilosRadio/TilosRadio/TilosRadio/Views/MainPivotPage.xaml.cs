namespace TD1990.TilosRadio.WP7.Views
{
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Tasks;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using TD1990.Libs.TDLyutil.Interfaces.ViewModels;
    using TD1990.TilosRadio.WP7.Interfaces;
    using TD1990.TilosRadio.WP7.ViewModels;

    public partial class MainPivotPage : PhoneApplicationPage
    {
        MainPivotViewModel ViewModel { get; set; }
        public MainPivotPage()
        {
            RenderCounterMax = 60;
            RenderCounter = RenderCounterMax;

            ViewModel = new MainPivotViewModel();
            ViewModel.Init();

            InitializeComponent();
#if DEBUG
            LoggerButton.Visibility = System.Windows.Visibility.Visible;
#endif
            LayoutRoot.DataContext = ViewModel;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            CompositionTarget.Rendering += CompositionTarget_Rendering;
            base.OnNavigatedTo(e);
        }

        int RenderCounter;
        int RenderCounterMax;

        void CompositionTarget_Rendering(object sender, System.EventArgs e)
        {
            if (RenderCounter <= 0)
            {
                ViewModel.Refresh();
                RenderCounter = RenderCounterMax;
            }
            else
            {
                RenderCounter--;
            }
        }

        void PlayArchiveButton_Click(object sender, System.EventArgs e)
        {
            Button button = sender as Button;
            ViewModel.PlayArchiveStream(button.DataContext as IEpisodeViewModel);
            MainPanorama.DefaultItem = MainPanorama.Items[0];
        }

        private void LoggerButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (MessageBox.Show(App.Logger.ToString(), "Debug Log", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                App.Logger.Clear();
            }
        }

        private void PhoneLiveShowButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PhoneCallTask callTask = new PhoneCallTask();
            callTask.DisplayName = "Tilos Rárió élő adás";
            callTask.PhoneNumber = ViewModel.CallShowPhoneNumber;
            callTask.Show();
        }

        private void EmailButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            EmailComposeTask email = new EmailComposeTask();
            email.To = AppResources.RadioEmailTo;
            email.Subject = AppResources.RadioEmailSubject;
            email.Body = string.Format(AppResources.RadioEmailBody, ViewModel.EmailBodyVersion);
            email.Show();
        }

        private void FactoryEmailButton_Click(object sender, RoutedEventArgs e)
        {
            EmailComposeTask email = new EmailComposeTask();
            email.To = AppResources.FactoryEmailTo;
            email.Subject = AppResources.FactoryEmailSubject;
            email.Body = string.Format(AppResources.FactoryEmailBody, ViewModel.EmailBodyVersion);
            email.Show();
        }

        private void AudioTrackListSelectTrackButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UserControl button = sender as UserControl;
            ViewModel.AudioPlayerViewModel.StopAudio();
            ViewModel.AudioPlayerViewModel.PlayTrack(button.DataContext as IAudioTrackInfoViewModel);
        }

        private void DonateCallButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.IsDonateCallAvailable)
            {
                PhoneCallTask callTask = new PhoneCallTask();
                callTask.DisplayName = "Tilos Rádió adhatvonal";
                callTask.PhoneNumber = "13600pp91";
                callTask.Show();
            }
            else
            {
                WebBrowserTask task = new WebBrowserTask();
                task.Uri = new System.Uri(@"http://www.adhat.hu", UriKind.Absolute);
                task.Show();
            }
        }

        private void DonateBankAccountButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText("11613008-00143300-13000005");
            MessageBox.Show(@"Erste Bank Hungary Rt. ""11613008-00143300-13000005"" számlaszáma felkerült a vágólapra.", "Banki átutalással", MessageBoxButton.OK);
        }

        private void DonateWorkButton_Click(object sender, RoutedEventArgs e)
        {
            WebBrowserTask task = new WebBrowserTask();
            task.Uri = new System.Uri(@"https://tilos.hu/mailman/listinfo/onkentes", UriKind.Absolute);
            task.Show();
        }

        private void DonateTaxButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText("18002454-2-43");
            MessageBox.Show(@"A Tilos Kulturális Alapítvány adószáma ""18002454-2-43"" felkerült a vágólapra.", "1%", MessageBoxButton.OK);
        }

        private void DonateBitcoinButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText("18nERo4mUWNd3d65J7buuhRvKn7AzTpugF");
            MessageBox.Show(@"A Tilos Rádió Bitcoin címe ""18nERo4mUWNd3d65J7buuhRvKn7AzTpugF"" felkerült a vágólapra.", "1%", MessageBoxButton.OK);
        }

        private void DonateMaratonButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void TilosWebBrowserButton_Click(object sender, RoutedEventArgs e)
        {
            WebBrowserTask task = new WebBrowserTask();
            task.Uri = new System.Uri(@"http://www.tilos.hu", UriKind.Absolute);
            task.Show();
        }

        private void PlayLiveStreamButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PlayLiveStream();
            MainPanorama.DefaultItem = MainPanorama.Items[0];
        }
    }
}