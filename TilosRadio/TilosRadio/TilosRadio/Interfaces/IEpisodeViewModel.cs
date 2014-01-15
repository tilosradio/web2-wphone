namespace TD1990.TilosRadio.WP7.Interfaces
{
    using System;

    public interface IEpisodeViewModel
    {
        DateTime PlannedFrom { get; }
        DateTime PlannedTo { get; }
        IShowViewModel Show { get; }
        bool IsActual { get; }
        bool CanPlay { get; }
        string M3UUrl { get; }
    }
}
