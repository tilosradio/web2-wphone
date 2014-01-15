namespace TD1990.TilosRadio.WP7.Interfaces
{
    using System.Collections.Generic;

    public interface IShowViewModel
    {
        string Name { get; }
        string Definition { get; }
        string ContributorNickNames { get; }
        ICollection<IContributorViewModel> ContributorCollection { get; }
    }
}
