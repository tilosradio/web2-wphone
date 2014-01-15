namespace TD1990.TilosRadio.TilosWebApi.ApiV0
{
    using System;

    public static class TilosRestApi
    {
        public static string RootUrl 
        { 
            get; 
            set; 
        }

        public static Uri GetEpisodesUri(int startTime, int endTime)
        {
            string url = string.Format("{0}/api/v0/episode?start={1}&end={2}", RootUrl, startTime, endTime);
            return new Uri(url, UriKind.Absolute);
        }

        public static Uri GetEpisodesUri()
        {
            string url = string.Format("{0}/api/v0/episode", RootUrl);
            return new Uri(url, UriKind.Absolute);
        }

        public static Uri GetHelpUri()
        {
            string url = string.Format("{0}/api/v0/text/segits", RootUrl);
            return new Uri(url, UriKind.Absolute);
        }
    }
}
