namespace TD1990.TilosRadio.TilosWebApi.ApiV0
{
    public class Episode
    {
        public int plannedFrom { get; set; }
        public int plannedTo { get; set; }
        public string m3uUrl { get; set; }
        public bool persistent { get; set; }
        public Show show { get; set; }
    }
}

