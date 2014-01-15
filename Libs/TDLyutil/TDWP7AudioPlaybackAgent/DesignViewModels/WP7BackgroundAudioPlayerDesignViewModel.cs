
namespace TD1990.Libs.TDLyutil.TDWP7AudioPlaybackAgent.DesignViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using TD1990.Libs.TDLyutil.Interfaces.ViewModels;

    public class WP7BackgroundAudioPlayerDesignViewModel : IWP7BackgroundAudioPlayerViewModel
    {
        public WP7BackgroundAudioPlayerDesignViewModel()
        {
            AudioTrackInfoCollection = new ObservableCollection<IAudioTrackInfoViewModel>();
            AudioTrackInfoCollection.Add(new AudioTrackInfoDesignViewModel());
            AudioTrackInfoCollection.Add(new AudioTrackInfoDesignViewModel());
            AudioTrackInfoCollection.Add(new AudioTrackInfoDesignViewModel());
            AudioTrackInfoCollection.Add(new AudioTrackInfoDesignViewModel());
        }

        public virtual void Init()
        {
        }

        public virtual string StreamPosition
        {
            get
            {
                return "12:34:56";
            }
        }

        public virtual string StreamStatus
        {
            get
            {
                return "Stream Status";
            }
        }

        public virtual void PlayAudio()
        {
        }

        public virtual void StopAudio()
        {
        }

        public virtual void NextTrack()
        {
        }

        public virtual void PreviousTrack()
        {
        }

        public virtual void Fastforward()
        {
        }

        public virtual void Rewind()
        {
        }

#pragma warning disable 67
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
#pragma warning restore 67

        public bool IsPlaying
        {
            get { return true; }
        }

        public bool IsLiveStream
        {
            get { return true; }
            set { }
        }


        public void Refresh()
        {
        }


        public void ClearTrackInfo()
        {
        }

        public double SeekMinimum
        {
            get { return 0.0; }
        }

        public double SeekMaximum
        {
            get {return 3000.0; }
        }

        public double SeekPosition
        {
            get
            {
                return 1500.0;
            }
            set
            {
            }
        }

        public bool CanSeek
        {
            get { return true; }
        }

        public ICollection<IAudioTrackInfoViewModel> AudioTrackInfoCollection
        {
            get;
            private set;
        }


        public void PlayTrack(IAudioTrackInfoViewModel audioTrackInfo)
        {
        }
    }
}
