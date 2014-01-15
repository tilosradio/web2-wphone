namespace TD1990.TilosRadio.WP7.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Net;
    using System.Threading;
    using System.Windows;
    using TD1990.Libs.TDLyutil.Common;
    using TD1990.TilosRadio.TilosWebApi.ApiV0;
    using TD1990.TilosRadio.WP7.Interfaces;

    public class AppModel
    {
        public AppModel()
        {
            IsLiveEpisodesLoading = false;
        }

        public void Init()
        {
            //TilosRestApi.RootUrl = "http://tilos.anzix.net";
            TilosRestApi.RootUrl = "http://tilos.hu";
            LiveEpisodesLastLoadedTime = DateTime.Now.AddMinutes(-10);
        }

        private DateTime LiveEpisodesLastLoadedTime
        {
            get;
            set;
        }

        private bool IsLiveEpisodesLoading
        {
            get;
            set;
        }

        public class EpisodeLoaderState
        {
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public bool IsLive { get; set; }
            public AppModel Model { get; set; }
        }

        public class M3ULoaderState
        {
            public IEpisodeViewModel Episode { get; set; }
            public AppModel Model { get; set; }
        }

        public class TextLoaderState
        {
            public string Alias { get; set; }
            public AppModel Model { get; set; }
            public EventHandler<TextEventArgs> OnEvent { get; set; }
        }

        public void LoadArchiveEpisodes(DateTime startDate, DateTime endDate)
        {
            var state = new EpisodeLoaderState();
            state.StartTime = startDate;
            state.EndTime = endDate;
            state.IsLive = false;
            state.Model = this;
            try
            {
                ThreadPool.QueueUserWorkItem(LoadEpisodesWorker, state);
            }
            catch
            {
            }
        }

        public void LoadLiveEpisodes()
        {
            if (!IsLiveEpisodesLoading && (DateTime.Now.AddMinutes(-1) > LiveEpisodesLastLoadedTime))
            {
                IsLiveEpisodesLoading = true;
                LiveEpisodesLastLoadedTime = DateTime.Now;
                var state = new EpisodeLoaderState();
                state.StartTime = DateTime.Now.AddHours(-8);
                state.EndTime = DateTime.Now.AddHours(16);
                state.IsLive = true;
                state.Model = this;
                try
                {
                    ThreadPool.QueueUserWorkItem(LoadEpisodesWorker, state);
                }
                catch
                {
                }
            }
        }

        public void LoadM3U(IEpisodeViewModel episode)
        {
            if (episode != null && episode.M3UUrl != null)
            {
                var state = new M3ULoaderState();
                state.Episode = episode;
                state.Model = this;
                try
                {
                    ThreadPool.QueueUserWorkItem(LoadM3UWorker, state);
                }
                catch
                {
                }
            }
        }

        public void LoadText(string alias)
        {
            var state = new TextLoaderState();
            state.Alias = alias;
            state.Model = this;
            state.OnEvent = this.OnHelpLoaded;
            try
            {
                ThreadPool.QueueUserWorkItem(LoadTextWorker, state);
            }
            catch
            {
            }
        }

        private static void LoadTextWorker(object state)
        {
            // todo
            //TextLoaderState loadstate = state as TextLoaderState;
            //WebClient client = new WebClient();
            // client.AddHeaders();
            //client.DownloadStringCompleted += loadstate.Model.LoadTextClient_DownloadStringCompleted;
            //client.DownloadStringAsync(new Uri(loadstate.Url, UriKind.Absolute), loadstate);
        }

        private static void LoadM3UWorker(object state)
        {
            try
            {
                M3ULoaderState loadstate = state as M3ULoaderState;
                WebClient client = new WebClient();
                client.AddHeaders();
                client.DownloadStringCompleted += loadstate.Model.LoadM3UClient_DownloadStringCompleted;
                client.DownloadStringAsync(new Uri(loadstate.Episode.M3UUrl, UriKind.Absolute), loadstate);
            }
            catch
            {
            }
        }

        private static void LoadEpisodesWorker(object state)
        {
            try
            {
                EpisodeLoaderState loadstate = state as EpisodeLoaderState;
                WebClient client = new WebClient();
                client.AddHeaders();
                client.DownloadStringCompleted += loadstate.Model.LoadEpisodesClient_DownloadStringCompleted;
                int starttime = loadstate.StartTime.ToUnixTimestamp();
                int endtime = loadstate.EndTime.ToUnixTimestamp();
                client.DownloadStringAsync(TilosRestApi.GetEpisodesUri(starttime, endtime), loadstate);
            }
            catch
            {
            }
        }

        void LoadTextClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                TextEventArgs eventArgs = new TextEventArgs();
                TextLoaderState state = e.UserState as TextLoaderState;

                if (e.Cancelled)
                {
                }
                else if (e.Error != null)
                {
                }
                else
                {
                    eventArgs.TextData = JsonConvert.DeserializeObject<GetTextResponse>(e.Result);
                }

                Deployment.Current.Dispatcher.BeginInvoke(delegate
                {
                    if (state.OnEvent != null)
                    {
                        state.OnEvent(this, eventArgs);
                    }
                });
            }
            catch
            {
            }
        }

        void LoadM3UClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                M3UEventArgs eventArgs = new M3UEventArgs();
                M3ULoaderState state = e.UserState as M3ULoaderState;
                eventArgs.Episode = state.Episode;

                if (e.Cancelled)
                {
                }
                else if (e.Error != null)
                {
                }
                else
                {
                    eventArgs.M3U = e.Result;
                }

                Deployment.Current.Dispatcher.BeginInvoke(delegate
                {
                    if (OnM3ULoaded != null)
                    {
                        OnM3ULoaded(this, eventArgs);
                    }
                });

            }
            catch
            {
            }
        }

        void LoadEpisodesClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            EpisodeResponseEventArgs eventArgs = new EpisodeResponseEventArgs();
            EpisodeLoaderState state = e.UserState as EpisodeLoaderState;
            try
            {
                if (e.Cancelled)
                {
                }
                else if (e.Error != null)
                {
                }
                else
                {
                    eventArgs.Episodes = JsonConvert.DeserializeObject<GetEpisodeResponse>(e.Result);
                }
            }
            catch
            {
            }

            try
            {
                Deployment.Current.Dispatcher.BeginInvoke(delegate
                {
                    if (state.IsLive)
                    {
                        IsLiveEpisodesLoading = false;
                        if (OnLiveEpisodesLoaded != null)
                        {
                            OnLiveEpisodesLoaded(this, eventArgs);
                        }
                    }
                    else
                    {
                        if (OnArchiveEpisodesLoaded != null)
                        {
                            OnArchiveEpisodesLoaded(this, eventArgs);
                        }
                    }
                });
            }
            catch
            {
            }
        }

        public class EpisodeResponseEventArgs : EventArgs
        {
            public GetEpisodeResponse Episodes
            {
                get;
                set;
            }
        }

        public class M3UEventArgs : EventArgs
        {
            public IEpisodeViewModel Episode
            {
                get;
                set;
            }

            public string M3U
            {
                get;
                set;
            }
        }

        public class TextEventArgs : EventArgs
        {
            public Text TextData
            {
                get;
                set;
            }
        }

        public event EventHandler<EpisodeResponseEventArgs> OnLiveEpisodesLoaded;
        public event EventHandler<EpisodeResponseEventArgs> OnArchiveEpisodesLoaded;
        public event EventHandler<M3UEventArgs> OnM3ULoaded;
        public event EventHandler<TextEventArgs> OnHelpLoaded;

        private static AppModel InstanceValue = new AppModel();
        public static AppModel Instance
        {
            get
            {
                return InstanceValue;
            }
        }
    }

    public static class WebClientExtensions
    {
        public static void AddHeaders(this WebClient client)
        {
            string anid = string.Empty;
            string appversion;
            string osversion;
            string shortversion;

            try
            {
                appversion = typeof(App).Assembly.FullName;
            }
            catch (Exception ex)
            {
                appversion = "error";
                App.Logger.Error("WebClientExtensionAddHeaders", "app version1", ex);
            }

            try
            {
                shortversion = typeof(App).Assembly.FullName.Split(',')[1].Split('=')[1];
            }
            catch (Exception ex)
            {
                shortversion = "0.0.0.0";
                App.Logger.Error("WebClientExtensionAddHeaders", "app version2", ex);
            }

            try
            {
                osversion = Environment.OSVersion.ToString();
            }
            catch (Exception ex)
            {
                osversion = "error";
                App.Logger.Error("WebClientExtensionAddHeaders", "os version", ex);
            }

            string useragentformat;
            useragentformat = "TilosRadioWP7App/{0} ({1}; {2})";

            client.Headers["User-Agent"] = string.Format(useragentformat, shortversion, appversion, osversion, anid);
            //client.Headers["Referer"] = "www.tilos.hu";
        }
    }
}
