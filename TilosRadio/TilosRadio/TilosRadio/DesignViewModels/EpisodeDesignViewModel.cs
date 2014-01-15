namespace TD1990.TilosRadio.WP7.DesignViewModels
{
    using System;
    using TD1990.TilosRadio.WP7.Interfaces;

    public class EpisodeDesignViewModel : IEpisodeViewModel
    {

        public EpisodeDesignViewModel()
        {
        }

        public System.DateTime PlannedFrom
        {
            get 
            {
                return DateTime.Now.AddHours(-2);    
            }
        }

        public System.DateTime PlannedTo
        {
            get
            {
                return DateTime.Now.AddHours(-1);
            }
        }

        public IShowViewModel Show
        {
            get 
            {
                return new ShowDesignViewModel();
            }
        }

        public bool IsActual
        {
            get 
            { 
                return false; 
            }
        }

        public string M3UUrl
        {
            get 
            { 
                return "http://arhiv.tilos.hu/a/m3u"; 
            }
        }


        public bool CanPlay
        {
            get
            { 
                return true; 
            }
        }
    }
}
