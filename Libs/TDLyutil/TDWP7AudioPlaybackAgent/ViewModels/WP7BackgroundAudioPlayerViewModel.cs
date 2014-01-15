
namespace TD1990.Libs.TDLyutil.TDWP7AudioPlaybackAgent.ViewModels
{
    using Microsoft.Phone.BackgroundAudio;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using TD1990.Libs.TDLyutil.Interfaces.ViewModels;
    using TD1990.Libs.TDLyutil.ViewModels;
    using System.Linq;

    public class WP7BackgroundAudioPlayerViewModel : BaseViewModel, IWP7BackgroundAudioPlayerViewModel
    {
        public WP7BackgroundAudioPlayerViewModel()
        {
            TrackListExchange = new TrackListExchange();
            StreamPositionFormat = "{0}:{1:00}:{2:00}";
        }

        public override void Init()
        {
            AudioTrackInfoCollection = new ObservableCollection<IAudioTrackInfoViewModel>();
            NotifyPropertyChanged(() => AudioTrackInfoCollection);
            try
            {
                BackgroundAudioPlayer.Instance.PlayStateChanged += BAPInstance_PlayStateChanged;
            }
            catch
            {
            }

            base.Init();
        }

        private AudioTrack LastPlayingTrackValue;
        protected virtual AudioTrack LastPlayingTrack
        {
            get
            {
                return LastPlayingTrackValue;
            }
            set
            {
                LastPlayingTrackValue = value;
                if (LastPlayingTrackValue != null && AudioTrackInfoCollection != null)
                {
                    foreach (var item in AudioTrackInfoCollection)
                    {
                        item.IsActive = LastPlayingTrackValue.Tag == item.Tag;
                    }
                }
            }
        }

        public virtual string StreamPosition
        {
            get
            {
                string s = string.Empty;
                try
                {
                    s = string.Format(
                        StreamPositionFormat,
                        BackgroundAudioPlayer.Instance.Position.Hours,
                        BackgroundAudioPlayer.Instance.Position.Minutes,
                        BackgroundAudioPlayer.Instance.Position.Seconds
                        );
                }
                catch
                {
                }
                return s;
            }
        }

        public virtual string StreamStatus
        {
            get
            {
                string s = string.Empty;
                try
                {
                    s = BackgroundAudioPlayer.Instance.PlayerState.ToString();
                }
                catch
                {
                }

                return s;
            }
        }

        public virtual void PlayAudio()
        {
            NotifyPropertyChanged(() => IsLiveStream);
            try
            {
                if (BackgroundAudioPlayer.Instance.Track == null)
                {
                    if (TrackListExchange.IsLiveStream)
                    {
                        var track = TrackListExchange.FindTrack(TrackListExchange.LiveStreamTag);
                        if (track == null)
                        {
                            track = TrackListExchange.FirstTrack();
                            TrackListExchange.LiveStreamTag = track.Tag;
                        }

                        BackgroundAudioPlayer.Instance.Track = track;
                    }
                    else
                    {
                        BackgroundAudioPlayer.Instance.Track = TrackListExchange.FirstTrack();
                    }
                }

                BackgroundAudioPlayer.Instance.Play();
            }
            catch
            {
            }

            if (OnPlayAudio != null)
            {
                OnPlayAudio(this, null);
            }
        }

        public void PlayTrack(IAudioTrackInfoViewModel audioTrackInfo)
        {
            NotifyPropertyChanged(() => IsLiveStream);
            try
            {
                if (audioTrackInfo != null)
                {
                    var track = TrackListExchange.FindTrack(audioTrackInfo.Tag);
                    BackgroundAudioPlayer.Instance.Track = track;
                    BackgroundAudioPlayer.Instance.Position = TimeSpan.Zero;
                    BackgroundAudioPlayer.Instance.Play();
                }
            }
            catch
            {
            }

            Refresh();
            RefreshAudioTrackInfoCollection();

            if (OnPlayAudio != null)
            {
                OnPlayAudio(this, null);
            }
        }

        public virtual void StopAudio()
        {
            try
            {
                BackgroundAudioPlayer.Instance.Stop();
                BackgroundAudioPlayer.Instance.Track = null;
            }
            catch
            {
            }

            if (OnStopAudio != null)
            {
                OnStopAudio(this, null);
            }
        }

        public virtual void NextTrack()
        {
            try
            {
                BackgroundAudioPlayer.Instance.SkipNext();
                BackgroundAudioPlayer.Instance.Position = TimeSpan.Zero;
            }
            catch
            {
            }
        }

        public virtual void PreviousTrack()
        {
            try
            {
                BackgroundAudioPlayer.Instance.SkipPrevious();
                BackgroundAudioPlayer.Instance.Position = TimeSpan.Zero;
            }
            catch
            {
            }
        }

        public virtual void Fastforward()
        {
            try
            {
                BackgroundAudioPlayer.Instance.FastForward();
            }
            catch
            {
            }
        }

        public virtual void Rewind()
        {
            try
            {
                BackgroundAudioPlayer.Instance.Rewind();
            }
            catch
            {
            }
        }

        public event System.EventHandler OnPlayAudio;

        public event System.EventHandler OnStopAudio;

        public event System.EventHandler OnPlayStateBufferingStarted;

        public event System.EventHandler OnPlayStateBufferingStopped;

        public event System.EventHandler OnPlayStateError;

        public event System.EventHandler OnPlayStateFastForwarding;

        public event System.EventHandler OnPlayStatePaused;

        public event System.EventHandler OnPlayStatePlaying;

        public event System.EventHandler OnPlayStateRewinding;

        public event System.EventHandler OnPlayStateShutdown;

        public event System.EventHandler OnPlayStateStopped;

        public event System.EventHandler OnPlayStateTrackEnded;

        public event System.EventHandler OnPlayStateTrackReady;

        public event System.EventHandler OnPlayStateUnknown;

        protected virtual void BAPInstance_PlayStateChanged(object sender, System.EventArgs e)
        {
            Refresh();
            try
            {
                switch (BackgroundAudioPlayer.Instance.PlayerState)
                {
                    case PlayState.BufferingStarted:
                        if (OnPlayStateBufferingStarted != null)
                        {
                            OnPlayStateBufferingStarted(this, e);
                        }
                        break;
                    case PlayState.BufferingStopped:
                        if (OnPlayStateBufferingStopped != null)
                        {
                            OnPlayStateBufferingStopped(this, e);
                        }
                        break;
                    case PlayState.Error:
                        if (OnPlayStateError != null)
                        {
                            OnPlayStateError(this, e);
                        }
                        break;
                    case PlayState.FastForwarding:
                        if (OnPlayStateFastForwarding != null)
                        {
                            OnPlayStateFastForwarding(this, e);
                        }
                        break;
                    case PlayState.Paused:
                        if (OnPlayStatePaused != null)
                        {
                            OnPlayStatePaused(this, e);
                        }
                        break;
                    case PlayState.Playing:
                        if (BackgroundAudioPlayer.Instance.Track != null)
                        {
                            LastPlayingTrack = BackgroundAudioPlayer.Instance.Track;
                        }

                        if (OnPlayStatePlaying != null)
                        {
                            OnPlayStatePlaying(this, e);
                        }
                        break;
                    case PlayState.Rewinding:
                        if (OnPlayStateRewinding != null)
                        {
                            OnPlayStateRewinding(this, e);
                        }
                        break;
                    case PlayState.Shutdown:
                        if (OnPlayStateShutdown != null)
                        {
                            OnPlayStateShutdown(this, e);
                        }
                        break;
                    case PlayState.Stopped:
                        if (OnPlayStateStopped != null)
                        {
                            OnPlayStateStopped(this, e);
                        }
                        break;
                    case PlayState.TrackEnded:
                        if (OnPlayStateTrackEnded != null)
                        {
                            OnPlayStateTrackEnded(this, e);
                        }
                        break;
                    case PlayState.TrackReady:
                        if (BackgroundAudioPlayer.Instance.Track != null)
                        {
                            LastPlayingTrack = BackgroundAudioPlayer.Instance.Track;
                        }

                        if (OnPlayStateTrackReady != null)
                        {
                            OnPlayStateTrackReady(this, e);
                        }
                        break;
                    case PlayState.Unknown:
                        if (OnPlayStateUnknown != null)
                        {
                            OnPlayStateUnknown(this, e);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch
            {
            }

        }

        protected virtual string StreamPositionFormat
        {
            get;
            set;
        }

        protected TrackListExchange TrackListExchange
        {
            get;
            set;
        }

        public bool IsPlaying
        {
            get
            {
                try
                {
                    return BackgroundAudioPlayer.Instance.PlayerState == PlayState.Playing;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool IsLiveStream
        {
            get
            {
                return TrackListExchange.IsLiveStream;
            }
            set
            {
                TrackListExchange.IsLiveStream = value;
                NotifyPropertyChanged(() => IsLiveStream);
            }
        }

        public void Refresh()
        {
            NotifyPropertyChanged(() => this.StreamPosition);
            NotifyPropertyChanged(() => this.StreamStatus);
            NotifyPropertyChanged(() => this.IsPlaying);
            NotifyPropertyChanged(() => this.SeekMaximum);
            NotifyPropertyChanged(() => this.SeekMinimum);
            NotifyPropertyChanged(() => this.SeekPosition);
            NotifyPropertyChanged(() => this.CanSeek);
        }

        public void ClearTrackInfo()
        {
            AudioTrackInfoCollection.Clear();
            LastPlayingTrack = null;
            Refresh();
            RefreshAudioTrackInfoCollection();
            NotifyPropertyChanged(() => IsLiveStream);
        }


        public double SeekMinimum
        {
            get
            {
                return 0.0;
            }
        }

        public double SeekMaximum
        {
            get
            {
                try
                {
                    if (BackgroundAudioPlayer.Instance.Track != null)
                    {
                        return BackgroundAudioPlayer.Instance.Track.Duration.TotalSeconds;
                    }
                }
                catch
                {
                }

                return 0.0;
            }
        }

        public double SeekPosition
        {
            get
            {
                try
                {
                    if (BackgroundAudioPlayer.Instance.Track != null)
                    {
                        return BackgroundAudioPlayer.Instance.Position.TotalSeconds;
                    }
                }
                catch
                {
                }

                return 0.0;
            }
            set
            {
                try
                {
                    if (BackgroundAudioPlayer.Instance.Track != null && BackgroundAudioPlayer.Instance.PlayerState == PlayState.Playing) // todo test
                    {
                        BackgroundAudioPlayer.Instance.Position = TimeSpan.FromSeconds(value);
                    }

                    NotifyPropertyChanged(() => SeekPosition);
                }
                catch
                {
                }
            }
        }

        public bool CanSeek
        {
            get
            {
                try
                {
                    return BackgroundAudioPlayer.Instance.Track != null && BackgroundAudioPlayer.Instance.CanSeek;
                }
                catch
                {
                    return false;
                }
            }
        }

        public ICollection<IAudioTrackInfoViewModel> AudioTrackInfoCollection
        {
            get;
            private set;
        }

        protected virtual void RefreshAudioTrackInfoCollection()
        {
            var trackList = TrackListExchange.AudioTrackInfoArray;
            AudioTrackInfoCollection.Clear();
            int tracknumber = 0;
            foreach (var item in trackList)
            {
                tracknumber += 1;
                var vm = item.ToAudioTrackInfoViewModel();
                vm.IsActive = BackgroundAudioPlayer.Instance.Track != null && BackgroundAudioPlayer.Instance.Track.Tag == vm.Tag;
                vm.TrackNumber = tracknumber;
                AudioTrackInfoCollection.Add(vm);
            }
        }

    }
}
