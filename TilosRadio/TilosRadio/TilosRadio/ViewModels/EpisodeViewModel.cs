namespace TD1990.TilosRadio.WP7.ViewModels
{
    using System;
    using TD1990.Libs.TDLyutil.Common;
    using TD1990.TilosRadio.TilosWebApi.ApiV0;
    using TD1990.TilosRadio.WP7.Interfaces;

    public class EpisodeViewModel : IEpisodeViewModel
    {
        public EpisodeViewModel(Episode episode)
        {
            PlannedFrom = episode.plannedFrom.FromUnixTimestamp().ToLocalTime();
            PlannedTo = episode.plannedTo.FromUnixTimestamp().ToLocalTime();
            DateTime now = DateTime.Now;
            IsActual = (now >= PlannedFrom && now < PlannedTo);
            M3UUrl = episode.m3uUrl;
            CanPlay = !string.IsNullOrWhiteSpace(M3UUrl) && PlannedTo < now;
            Show = new ShowViewModel(episode.show);
        }

        public System.DateTime PlannedFrom
        {
            get;
            private set;
        }

        public System.DateTime PlannedTo
        {
            get;
            private set;
        }

        public IShowViewModel Show
        {
            get;
            private set;
        }


        public bool IsActual
        {
            get;
            private set;
        }

        public string M3UUrl
        {
            get;
            private set;
        }

        public bool CanPlay
        {
            get;
            private set;
        }
    }
}
