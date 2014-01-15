namespace TD1990.TilosRadio.WP7.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using TD1990.TilosRadio.TilosWebApi.ApiV0;
    using TD1990.TilosRadio.WP7.Interfaces;

    public class ShowViewModel : IShowViewModel
    {
        public ShowViewModel(Show show)
        {
            Name = show.name;
            Definition = show.definition;
            ContributorCollection = new ObservableCollection<IContributorViewModel>();
            show.contributors.ToList().ForEach(c => ContributorCollection.Add(new ContributorViewModel(c)));
            ContributorNickNames = string.Join(", ", show.contributors.Select(c => c.nick).ToArray());
        }

        public string Name
        {
            get;
            private set;
        }

        public ICollection<IContributorViewModel> ContributorCollection
        {
            get;
            private set;
        }

        public string Definition
        {
            get;
            private set;
        }

        public string ContributorNickNames
        {
            get;
            private set;
        }
    }
}
