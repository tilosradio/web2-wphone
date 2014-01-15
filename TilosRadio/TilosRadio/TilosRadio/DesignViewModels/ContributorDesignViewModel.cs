namespace TD1990.TilosRadio.WP7.DesignViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using TD1990.TilosRadio.WP7.Interfaces;

    public class ContributorDesignViewModel : IContributorViewModel
    {
        public ContributorDesignViewModel()
        {
        }

        public string Nick
        {
            get 
            {
                return "Contr Nick";
            }
        }
    }
}
