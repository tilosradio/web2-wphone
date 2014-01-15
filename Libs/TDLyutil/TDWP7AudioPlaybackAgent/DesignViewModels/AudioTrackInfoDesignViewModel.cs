
namespace TD1990.Libs.TDLyutil.TDWP7AudioPlaybackAgent.DesignViewModels
{
    using TD1990.Libs.TDLyutil.Interfaces.ViewModels;

    public class AudioTrackInfoDesignViewModel : IAudioTrackInfoViewModel
    {
        public string Tag
        {
            get
            {
                return "Track Tag";
            }
            set
            {
            }
        }

        public string Url
        {
            get
            {
                return "Track Url";
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public System.UriKind UrlUriKind
        {
            get
            {
                return System.UriKind.Absolute;
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public string Title
        {
            get
            {
                return "Track Title";
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public string Artist
        {
            get
            {
                return "Track Artist";
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public string Album
        {
            get
            {
                return "Track Album";
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public string AlbumArtUrl
        {
            get
            {
                return "Track Album Art Url";
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public System.UriKind AlbumArtUriKind
        {
            get
            {
                return System.UriKind.Absolute;
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public bool IsActive
        {
            get
            {
                return true;
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public int TrackNumber
        {
            get
            {
                return 3;
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
