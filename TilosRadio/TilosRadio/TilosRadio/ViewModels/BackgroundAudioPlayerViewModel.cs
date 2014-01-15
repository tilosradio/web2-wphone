namespace TD1990.TilosRadio.WP7.ViewModels
{
    using Microsoft.Phone.BackgroundAudio;
    using System;
    using System.Linq;
    using TD1990.Libs.TDLyutil;
    using TD1990.Libs.TDLyutil.SharedMedia;
    using TD1990.Libs.TDLyutil.TDWP7AudioPlaybackAgent;
    using TD1990.Libs.TDLyutil.TDWP7AudioPlaybackAgent.ViewModels;
    using TD1990.TilosRadio.WP7.Interfaces;

    public class BackgroundAudioPlayerViewModel : WP7BackgroundAudioPlayerViewModel, IBackgroundAudioPlayerViewModel
    {
        public BackgroundAudioPlayerViewModel()
        {
        }

        public override void Init()
        {
            base.Init();
            TrackListExchange.Logger = App.Logger;
            SharedMedia.CopyResourceIconToSharedMedia("Icons/ZuneSharedIcon.jpg", "TilosRadioIcon.jpg");
            if (TrackListExchange.IsTrackListEmpty)
            {
                SetLiveTracks();
            }

            LastPlayingTrack = BackgroundAudioPlayer.Instance.Track ?? LastPlayingTrack;
            Refresh();
            RefreshAudioTrackInfoCollection();
        }

        private void SetLiveTracks()
        {
            TrackListExchange.IsLiveStream = true;
            TrackListExchange.ClearTrackList();

            AudioTrackInfo trackInfo = new AudioTrackInfo();
            trackInfo.Album = "Tilos Rádió élő adás, jó minőségben.";
            trackInfo.AlbumArtUriKind = UriKind.Relative;
            trackInfo.AlbumArtUrl = "Shared/Media/TilosRadioIcon.jpg";
            trackInfo.Artist = "Tilos Rádió";
            trackInfo.Tag = @"live128kbit";
            trackInfo.Title = "Tilos Rádió élő adás - 128kbit";
            trackInfo.Url = @"http://stream.tilos.hu:80/tilos_128.mp3";
            trackInfo.UrlUriKind = UriKind.Absolute;
            TrackListExchange.AddTrack(trackInfo);


            // new Uri("Shared/Media/TilosRadioIcon.jpg", UriKind.Relative)

            trackInfo = new AudioTrackInfo();
            trackInfo.Album = "Tilos Rádió élő adás, kiváló minőségben.";
            trackInfo.AlbumArtUriKind = UriKind.Relative;
            trackInfo.AlbumArtUrl = "Shared/Media/TilosRadioIcon.jpg";
            trackInfo.Artist = "Tilos Rádió";
            trackInfo.Tag = @"live256kbit";
            trackInfo.Title = "Tilos Rádió élő adás - 256kbit";
            trackInfo.Url = @"http://stream.tilos.hu:80/tilos; stream.mp3";
            trackInfo.UrlUriKind = UriKind.Absolute;
            TrackListExchange.AddTrack(trackInfo);
        }

        public void PlayLive()
        {
            SetLiveTracks();
            PlayAudio();
            Refresh();
            RefreshAudioTrackInfoCollection();
        }

        public void PlayArchive(IEpisodeViewModel episode, string m3u)
        {
            if (m3u.HasPayload())
            {
                TrackListExchange.IsLiveStream = false;
                TrackListExchange.ClearTrackList();

                m3u = m3u.Replace('\r', '\n');
                int part = 0;
                foreach (string line in m3u.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (!line.StartsWith("#") && line.EndsWith(".mp3"))
                    {
                        try
                        {
                            part += 1;
                            string title = line.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).Last();
                            AudioTrackInfo trackInfo = new AudioTrackInfo();
                            trackInfo.Album = episode.Show.Name + " " + episode.Show.Definition;
                            trackInfo.AlbumArtUriKind = UriKind.Relative;
                            trackInfo.AlbumArtUrl = "Shared/Media/TilosRadioIcon.jpg";
                            trackInfo.Artist = "Tilos Rádió - " + episode.Show.ContributorNickNames;
                            trackInfo.Tag = line;
                            trackInfo.Title = string.Format("{0} {1:g} - {2:t} {3}.", episode.Show.Name, episode.PlannedFrom, episode.PlannedTo, part);
                            trackInfo.Url = line;
                            trackInfo.UrlUriKind = UriKind.Absolute;
                            TrackListExchange.AddTrack(trackInfo);
                            App.Logger.Info("BackgroundAgentWM.PlayArchiveM3U.TrackAdded", trackInfo.Tag + " " + trackInfo.Url);
                        }
                        catch
                        {
                        }
                    }
                }

                LastPlayingTrack = TrackListExchange.FirstTrack();
                PlayAudio();
            }

            Refresh();
            RefreshAudioTrackInfoCollection();
        }

        public override void PreviousTrack()
        {
            base.PreviousTrack();
            try
            {
                BackgroundAudioPlayer.Instance.Play();
            }
            catch
            {
            }
        }

        public override void NextTrack()
        {
            base.NextTrack();
            try
            {
                BackgroundAudioPlayer.Instance.Play();
            }
            catch
            {
            }
        }

        public override string StreamStatus
        {
            get
            {
                try
                {
                    switch (BackgroundAudioPlayer.Instance.PlayerState)
                    {
                        case PlayState.BufferingStarted:
                            return AppResources.StreamBufferingStarted;
                        case PlayState.BufferingStopped:
                            return AppResources.StreamBufferingStopped;
                        case PlayState.Error:
                            return AppResources.StreamError;
                        case PlayState.FastForwarding:
                            return AppResources.StreamFastForwarding;
                        case PlayState.Paused:
                            return AppResources.StreamPaused;
                        case PlayState.Playing:
                            return AppResources.StreamPlaying;
                        case PlayState.Rewinding:
                            return AppResources.StreamRewinding;
                        case PlayState.Shutdown:
                            return AppResources.StreamShutdown;
                        case PlayState.Stopped:
                            return AppResources.StreamStopped;
                        case PlayState.TrackEnded:
                            return AppResources.StreamTrackEnded;
                        case PlayState.TrackReady:
                            return AppResources.StreamTrackReady;
                        case PlayState.Unknown:
                            return AppResources.StreamUnknown;
                        default:
                            return AppResources.StreamDefault;
                    }
                }
                catch
                {
                    return AppResources.StreamException;
                }
            }
        }
    }
}
