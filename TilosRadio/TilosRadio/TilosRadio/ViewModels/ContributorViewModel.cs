namespace TD1990.TilosRadio.WP7.ViewModels
{
    using TD1990.TilosRadio.TilosWebApi.ApiV0;
    using TD1990.TilosRadio.WP7.Interfaces;

    public class ContributorViewModel : IContributorViewModel
    {
        public ContributorViewModel(Contributor contributor)
        {
            Nick = contributor.nick;
        }

        public string Nick
        {
            get;
            private set;
        }
    }
}
