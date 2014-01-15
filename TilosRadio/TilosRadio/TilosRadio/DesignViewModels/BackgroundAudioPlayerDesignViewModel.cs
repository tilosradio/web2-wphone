namespace TD1990.TilosRadio.WP7.DesignViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using TD1990.Libs.TDLyutil.TDWP7AudioPlaybackAgent.DesignViewModels;
    using TD1990.TilosRadio.WP7.Interfaces;

    public class BackgroundAudioPlayerDesignViewModel : WP7BackgroundAudioPlayerDesignViewModel, IBackgroundAudioPlayerViewModel
    {
        public BackgroundAudioPlayerDesignViewModel()
        {
        }

        public void PlayLive()
        {
        }

        public void PlayArchive(IEpisodeViewModel episode, string m3u)
        {
        }
    }
}
