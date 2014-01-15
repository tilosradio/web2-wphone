namespace TD1990.TilosRadio.WP7.DesignViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using TD1990.TilosRadio.WP7.Interfaces;

    public class ShowDesignViewModel : IShowViewModel
    {
        public ShowDesignViewModel()
        {
        }

        public string Name
        {
            get { return "Show Name"; }
        }

        public ICollection<IContributorViewModel> ContributorCollection
        {
            get 
            {
                var conts = new ObservableCollection<IContributorViewModel>();
                conts.Add(new ContributorDesignViewModel());
                conts.Add(new ContributorDesignViewModel());
                conts.Add(new ContributorDesignViewModel());
                conts.Add(new ContributorDesignViewModel());
                return conts;
            }
        }

        public string Definition
        {
            get { return "Definition definition definition definition definition definition deid de de"; }
        }


        public string ContributorNickNames
        {
            get { return "buga jakab, kics csavar"; }
        }
    }
}
