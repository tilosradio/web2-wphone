namespace TD1990.TilosRadio.WP7.DesignViewModels
{
    using TD1990.Libs.TDLyutil.Interfaces.ViewModels;
    using TD1990.Libs.TDLyutil.TDWP7AudioPlaybackAgent.DesignViewModels;
    using TD1990.TilosRadio.WP7.Interfaces;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System;

    public class MainPivotDesignViewModel : IMainPivotViewModel
    {
        public MainPivotDesignViewModel()
        {
            AudioPlayerViewModel = new BackgroundAudioPlayerDesignViewModel();
            LiveEpisodeCollection = new ObservableCollection<IEpisodeViewModel>();
            LiveEpisodeCollection.Add(new EpisodeDesignViewModel());
            LiveEpisodeCollection.Add(new EpisodeDesignViewModel());
            LiveEpisodeCollection.Add(new EpisodeDesignViewModel());
            LiveEpisodeCollection.Add(new EpisodeDesignViewModel());
            ArchiveEpisodeCollection = new ObservableCollection<IEpisodeViewModel>();
            ArchiveEpisodeCollection.Add(new EpisodeDesignViewModel());
            ArchiveEpisodeCollection.Add(new EpisodeDesignViewModel());
            ArchiveEpisodeCollection.Add(new EpisodeDesignViewModel());
            ArchiveEpisodeCollection.Add(new EpisodeDesignViewModel());
        }

        public IBackgroundAudioPlayerViewModel AudioPlayerViewModel
        {
            get;
            private set;
        }

        public ICollection<IEpisodeViewModel> LiveEpisodeCollection
        {
            get;
            private set;
        }

        public void PlayLiveStream()
        {
        }

        public void PlayArchiveStream(IEpisodeViewModel episode)
        {
        }

        public ICollection<IEpisodeViewModel> ArchiveEpisodeCollection
        {
            get;
            private set;
        }

        public DateTime ArchiveDay
        {
            get
            {
                return DateTime.Today;
            }
            set
            {
            }
        }

        public void LoadPrevArchiveDay()
        {
        }

        public void LoadNextArchiveDay()
        {
        }


        public string CallShowPhoneNumber
        {
            get { return "(+36 1) 215 37 73"; }
        }

        public string ContaxtText
        {
            get
            {
                return @"adástelefon: 215 3773

e-mail: radio@tilos.hu

Minden műsorral kapcsolatos kérdésben a Tilos  szerkesztőségének írjatok levelet!

A Tilos Rádiót működtető Tilos Kulturális Alapítvány elérhetőségei

postacím: 1462 Budapest, Pf. 601.
cím: 1085 Budapest, Üllői út 32. (Mária u. 54.)

telefon: (061) 476 8491
fax:  (061) 476 8492

e-mail:  radio@tilos.hu
";
            }
        }

        public string HelpText
        {
            get
            {
                return @"Hogyan lehet segíteni a Tilos Rádiónak?


 

A Tilos Rádiót működtető Tilos Kulturális Alapítvány kiemelten közhasznú szervezet. 
A pénzbeli adományok adóalapot csökkenthetnek! 
A megfelelő igazolásokat magánszemélyeknek és társaságoknak egyaránt kiállítjuk.

 ADOMÁNYOZZ ONLINE



MOBILról hívd az adományvonalat:



Az Invitel, a T-Home és T-Mobile valamint a Telenor és Vodafone hálózatából érhető el a szolgáltatás. Más szolgáltatók hálózatából (így például a Digi és a UPC területéről) sajnos nem hívható a vonal :( 





Adományozz Bitcoin-t!

 A Tilos Rádió Bitcoin címe:

18nERo4mUWNd3d65J7buuhRvKn7AzTpugF

 Erre a címre bárki tud a Tilos Rádiónak bitcoint küldeni. 


 



Banki átutalással:

Erste Bank Hungary Rt. 
11613008-00143300-13000005 
SWIFT, BIC: GIBA HU HB 
IBAN: HU17 1161 3008 0014 3300 1300 0005 



Önkéntes munkával:

A jelentkezéshez fel kell iratkozni a segítők levelezőlistájára: 
https://tilos.hu/mailman/listinfo/onkentes



Az adó 1%-ával:

A Tilos Kulturális Alapítvány adószáma: 18002454-2-43 
Ebből 2012. novemberében 7.239.260  Ft érkezett a számlánkra, amivel a legtámogatottabb nonprofit rádió vagyunk kerek e Magyarországon. Köszönet érte! 



A Maraton idején:

A Maraton a Tilos Rádió évente megszervezett egy hetes adománygyűjtő akciója, melyben a hallgatók és szimpatizánsok kifejezhetik a rádió iránti szeretetünket. 2013-ban, az évi „rendes” Tilos Maratonon összesen 6.993.477 Ft-tal segítették hallgatóink és szimpatizánsaink a rádió működését.

Lehet bármivel, de: leginkább ezermesterre lenne szükségünk, aki előszeretettel fúr-farag, használt irodai eszközök, bútorok is mindig jól jönnek: szék, kanapé, asztal, polc, fiókos szekrény, lámpa, irodaszer stb... Találd ki, hogy Te mivel tudsz segíteni! Ha bármilyen felesleges tárgyad van, hívj fel minket a 476-8491-en! 



Amire a Rádióban mindig szükség van:

Ha van megunt, esetleg talált bakelit (fekete) lemezed, nyugodt szívvel kínáld fel nekünk, mi ugyanis alapvetően ilyen típusú hanghordozókkal dolgozunk.

 

Tilos Kulturális Alapítvány

1085 Budapest, Üllői út 32. (Mária u. 54.)
telefon: (061) 476 8491
fax: (061) 476 8492
radio@tilos.hu
postacím: 1462 Budapest, Pf. 601.";
            }
        }


        public string EmailBodyVersion
        {
            get { return "version 1.1.1.1."; }
        }


        public bool IsDonateCallAvailable
        {
            get { return true; }
        }
    }
}
