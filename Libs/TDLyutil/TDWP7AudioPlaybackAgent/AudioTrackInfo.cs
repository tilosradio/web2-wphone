namespace TD1990.Libs.TDLyutil.TDWP7AudioPlaybackAgent
{
    using Microsoft.Phone.BackgroundAudio;
    using System;
    using TD1990.Libs.TDLyutil.TDWP7AudioPlaybackAgent.ViewModels;
            
    public class AudioTrackInfo
    {
        public string Tag { get; set; }
        public string Url { get; set; }
        public UriKind UrlUriKind { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string AlbumArtUrl { get; set; }
        public UriKind AlbumArtUriKind { get; set; }
    }

    public static class AudioTrackInfoExtender
    {
        public static AudioTrack ToAudioTrack(this AudioTrackInfo trackInfo)
        {
            if (trackInfo == null)
            {
                return null;
            }

            AudioTrack track = new AudioTrack(
                trackInfo.Url.ToUri(trackInfo.UrlUriKind),
                trackInfo.Title,
                trackInfo.Artist,
                trackInfo.Album,
                trackInfo.AlbumArtUrl.ToUri(trackInfo.AlbumArtUriKind),
                trackInfo.Tag,
                EnabledPlayerControls.All
                );

            return track;
        }

        public static AudioTrackInfoViewModel ToAudioTrackInfoViewModel(this AudioTrackInfo trackInfo)
        {
            if (trackInfo == null)
            {
                return null;
            }

            AudioTrackInfoViewModel track = new AudioTrackInfoViewModel(trackInfo);
            return track;
        }
    }
}
