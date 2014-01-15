
namespace TD1990.Libs.TDLyutil.Interfaces.Rss
{
    using System;
    public interface IRssItem
    {
        string Title { get; }

        string Link { get; }

        Uri LinkUri { get; }

        string Description { get; }

        string PubDate { get; }

        DateTime? PublishDate { get; }
    }
}
