namespace TD1990.Libs.TDLyutil.Interfaces.ViewModels
{
    using System;

    public interface IAudioTrackInfoViewModel
    {
        int TrackNumber { get; set; }
        string Tag { get; set; }
        string Url { get; set; }
        UriKind UrlUriKind { get; set; }
        string Title { get; set; }
        string Artist { get; set; }
        string Album { get; set; }
        string AlbumArtUrl { get; set; }
        UriKind AlbumArtUriKind { get; set; }
        bool IsActive { get; set; }
    }
}
