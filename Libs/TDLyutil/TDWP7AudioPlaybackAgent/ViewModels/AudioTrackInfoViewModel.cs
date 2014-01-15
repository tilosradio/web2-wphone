namespace TD1990.Libs.TDLyutil.TDWP7AudioPlaybackAgent.ViewModels
{
    using TD1990.Libs.TDLyutil.Interfaces.ViewModels;
    using TDLyutil.ViewModels;

    public class AudioTrackInfoViewModel : BaseViewModel, IAudioTrackInfoViewModel
    {
        public AudioTrackInfoViewModel(AudioTrackInfo trackInfo)
        {
            TrackInfo = trackInfo;
        }

        private AudioTrackInfo TrackInfo;

        public string Tag
        {
            get
            {
                return TrackInfo.Tag;
            }
            set
            {
            }
        }

        public string Url
        {
            get
            {
                return TrackInfo.Url;
            }
            set
            {
            }
        }

        public System.UriKind UrlUriKind
        {
            get
            {
                return TrackInfo.UrlUriKind;
            }
            set
            {
            }
        }

        public string Title
        {
            get
            {
                return TrackInfo.Title;
            }
            set
            {
            }
        }

        public string Artist
        {
            get
            {
                return TrackInfo.Artist;
            }
            set
            {
            }
        }

        public string Album
        {
            get
            {
                return TrackInfo.Album;
            }
            set
            {
            }
        }

        public string AlbumArtUrl
        {
            get
            {
                return TrackInfo.AlbumArtUrl;
            }
            set
            {
            }
        }

        public System.UriKind AlbumArtUriKind
        {
            get
            {
                return TrackInfo.AlbumArtUriKind;
            }
            set
            {
            }
        }

        private bool IsActiveValue;
        public bool IsActive
        {
            get
            {
                return IsActiveValue;
            }
            set
            {
                IsActiveValue = value;
                NotifyPropertyChanged(() => IsActive);
            }
        }

        private int TrackNumberValue;
        public int TrackNumber
        {
            get
            {
                return TrackNumberValue;
            }
            set
            {
                TrackNumberValue = value;
                NotifyPropertyChanged(() => TrackNumber);
            }
        }
    }
}
