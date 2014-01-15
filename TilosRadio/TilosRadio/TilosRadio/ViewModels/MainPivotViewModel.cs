namespace TD1990.TilosRadio.WP7.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using TD1990.Libs.TDLyutil.ViewModels;
    using TD1990.TilosRadio.WP7.Interfaces;
    using TD1990.TilosRadio.WP7.Models;

    public class MainPivotViewModel : BaseViewModel, IMainPivotViewModel
    {
        public MainPivotViewModel()
        {
            AudioPlayerViewModel = new BackgroundAudioPlayerViewModel();
            LiveEpisodeCollection = new ObservableCollection<IEpisodeViewModel>();
            ArchiveEpisodeCollection = new ObservableCollection<IEpisodeViewModel>();
        }

        public override void Init()
        {
            AudioPlayerViewModel.Init();
            AppModel.Instance.Init();
            AppModel.Instance.OnLiveEpisodesLoaded += AppModel_OnLiveEpisodesLoaded;
            AppModel.Instance.OnArchiveEpisodesLoaded += Instance_OnArchiveEpisodesLoaded;
            AppModel.Instance.OnM3ULoaded += Instance_OnM3ULoaded;
            AppModel.Instance.OnHelpLoaded += Instance_OnHelpLoaded;
            AppModel.Instance.LoadLiveEpisodes();
            ArchiveDay = DateTime.Today.AddDays(-1);
            base.Init();
        }

        void Instance_OnHelpLoaded(object sender, AppModel.TextEventArgs e)
        {
            HelpText = e.TextData.content;
            NotifyPropertyChanged(() => HelpText);
        }

        void Instance_OnM3ULoaded(object sender, AppModel.M3UEventArgs e)
        {
            AudioPlayerViewModel.PlayArchive(e.Episode, e.M3U);
        }

        void Instance_OnArchiveEpisodesLoaded(object sender, AppModel.EpisodeResponseEventArgs e)
        {
            if (e.Episodes != null)
            {
                ArchiveEpisodeCollection.Clear();
                e.Episodes.ForEach(ep => ArchiveEpisodeCollection.Add(new EpisodeViewModel(ep)));
            }
        }

        void AppModel_OnLiveEpisodesLoaded(object sender, AppModel.EpisodeResponseEventArgs e)
        {
            if (e.Episodes != null)
            {
                LiveEpisodeCollection.Clear();
                EpisodeViewModel actualEpisode = null;
                EpisodeViewModel prevEpisode = null;

                foreach (var ep in e.Episodes)
                {
                    var newEpisode = new EpisodeViewModel(ep);

                    if (actualEpisode != null)
                    {
                        LiveEpisodeCollection.Add(newEpisode);
                    }
                    else
                    {
                        if (newEpisode.IsActual)
                        {
                            actualEpisode = newEpisode;
                            if (prevEpisode != null)
                            {
                                LiveEpisodeCollection.Add(prevEpisode);
                            }

                            LiveEpisodeCollection.Add(actualEpisode);
                        }
                        else
                        {
                            prevEpisode = newEpisode;
                        }
                    }
                }
            }
        }

        public void Refresh()
        {
            AudioPlayerViewModel.Refresh();
            DateTime now = DateTime.Now;
            var actualepisode = LiveEpisodeCollection.FirstOrDefault(le => le.IsActual);
            if (actualepisode == null || actualepisode.PlannedTo < now)
            {
                AppModel.Instance.LoadLiveEpisodes();
            }
        }

        public IBackgroundAudioPlayerViewModel AudioPlayerViewModel
        {
            get;
            private set;
        }

        public ICollection<IEpisodeViewModel> LiveEpisodeCollection
        {
            get;
            private set;
        }

        public void PlayLiveStream()
        {
            AudioPlayerViewModel.StopAudio();
            AudioPlayerViewModel.ClearTrackInfo();
            AudioPlayerViewModel.PlayLive();
        }

        public void PlayArchiveStream(IEpisodeViewModel episode)
        {
            AudioPlayerViewModel.StopAudio();
            AudioPlayerViewModel.ClearTrackInfo();
            AppModel.Instance.LoadM3U(episode);
        }

        public ICollection<IEpisodeViewModel> ArchiveEpisodeCollection
        {
            get;
            private set;
        }

        private DateTime ArchiveDayValue;
        public DateTime ArchiveDay
        {
            get
            {
                return ArchiveDayValue;
            }
            set
            {
                if (ArchiveDayValue != value)
                {
                    ArchiveDayValue = value;
                    NotifyPropertyChanged(() => ArchiveDay);
                    ArchiveEpisodeCollection.Clear();
                    AppModel.Instance.LoadArchiveEpisodes(ArchiveDay.AddHours(-1), ArchiveDay.AddDays(1).AddHours(1));
                }
            }
        }

        public void LoadPrevArchiveDay()
        {
            ArchiveDay = ArchiveDay.AddDays(-1);
        }

        public void LoadNextArchiveDay()
        {
            ArchiveDay = ArchiveDay.AddDays(1);
        }

        public string CallShowPhoneNumber
        {
            get
            {
                return "(+36 1) 215 37 73";
            }
        }

        public string ContaxtText
        {
            get;
            private set;
        }

        public string HelpText
        {
            get;
            private set;
        }


        public string EmailBodyVersion
        {
            get
            {
                return typeof(App).Assembly.FullName + " " + Environment.OSVersion.ToString();
            }
        }


        public bool IsDonateCallAvailable
        {
            get 
            {
                try
                {
                    var s = Microsoft.Phone.Net.NetworkInformation.DeviceNetworkInformation.CellularMobileOperator;
                    return s.Contains("T-Mobile H")
                        || s.Contains("vodafone HU")
                        || s.Contains("pannon")
                        || s.Contains("Pannon")
                        || s.Contains("PANNON")
                        || s.Contains("djuice")
                        || s.Contains("Telenor H");
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
