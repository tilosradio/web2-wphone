
namespace TD1990.Libs.TDLyutil.Interfaces.Rss
{
    public interface IRssRoot
    {
        IRssChannel[] Channels { get; }
        string Version { get; }
    }
}
