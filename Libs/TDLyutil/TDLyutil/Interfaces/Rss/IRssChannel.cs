
namespace TD1990.Libs.TDLyutil.Interfaces.Rss
{
    public interface IRssChannel
    {
        string Title { get; }

        string Description { get; }

        IRssItem[] Items { get; }
    }
}
