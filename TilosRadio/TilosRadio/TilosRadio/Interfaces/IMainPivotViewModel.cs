namespace TD1990.TilosRadio.WP7.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IMainPivotViewModel
    {
        IBackgroundAudioPlayerViewModel AudioPlayerViewModel { get; }

        ICollection<IEpisodeViewModel> LiveEpisodeCollection { get; }

        ICollection<IEpisodeViewModel> ArchiveEpisodeCollection { get; }

        DateTime ArchiveDay { get; set; }

        string CallShowPhoneNumber { get; }

        string ContaxtText { get; }

        string HelpText { get; }

        string EmailBodyVersion { get; }

        bool IsDonateCallAvailable { get; }

        void PlayLiveStream();

        void PlayArchiveStream(IEpisodeViewModel episode);


        void LoadPrevArchiveDay();
        void LoadNextArchiveDay();
    }
}
