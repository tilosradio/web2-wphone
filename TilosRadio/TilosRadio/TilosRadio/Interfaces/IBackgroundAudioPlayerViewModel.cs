namespace TD1990.TilosRadio.WP7.Interfaces
{
    using TD1990.Libs.TDLyutil.Interfaces.ViewModels;

    public interface IBackgroundAudioPlayerViewModel: IWP7BackgroundAudioPlayerViewModel
    {
        void PlayLive();
        void PlayArchive(IEpisodeViewModel episode, string m3u);
    }
}
