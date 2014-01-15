namespace TD1990.TilosRadio.TilosWebApi.ApiV0
{
    public class Show
    {
        public int id { get; set; }
        public string name { get; set; }
        public string definition { get; set; }
        public string alias { get; set; }
        public string banner { get; set; }
        public string description { get; set; }
        public int type { get; set; }
        public int status { get; set; }
        public Contributor[] contributors { get; set; }
        public Url[] urls { get; set; }
        public object[] schedulings { get; set; }
        public Episode[] episodes { get; set; }
    }
}
