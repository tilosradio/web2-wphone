namespace TD1990.Libs.TDLyutil.TDWP7AudioPlaybackAgent
{
    using Microsoft.Phone.BackgroundAudio;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TD1990.Libs.TDLyutil.DebugTools;
    using TD1990.Libs.TDLyutil.Interfaces.DebugTools;
    using TD1990.Libs.TDLyutil.IsolatedStorage;

    public class TrackListExchange
    {
        public TrackListExchange()
        {
            Logger = new EmptyLogger();
        }

        public ILogger Logger { get; set; }

        private const string IsLiveStreamFileName = @"IsLiveStream.txt";
        private const string AudioTrackInfoArrayFileName = @"AudioTrackInfoArray.txt";
        private const string LiveStreamTagFileName = @"LiveStreamTag.txt";

        public List<AudioTrackInfo> AudioTrackInfoArray
        {
            get
            {
                try
                {
                    var result = StorageFile.ReadFile<AudioTrackInfo[]>(AudioTrackInfoArrayFileName).ToList();
                    //DebugTools.DebugMessageBoxShow(result.ToString(), "get.AudioTrackInfoArray");
                    return result;
                }
                catch (Exception ex)
                {
                    //DebugTools.DebugMessageBoxShow(ex.ToString(), "get.AudioTrackInfoArray");
                    return new List<AudioTrackInfo>();
                }
            }
            set
            {
                try
                {
                    StorageFile.SaveFile<AudioTrackInfo[]>(AudioTrackInfoArrayFileName, value.ToArray());
                    //DebugTools.DebugMessageBoxShow(value.ToString(), "set.AudioTrackInfoArray");
                }
                catch (Exception ex)
                {
                    //DebugTools.DebugMessageBoxShow(ex.ToString(), "set.AudioTrackInfoArray");
                }
            }
        }


        public bool IsLiveStream
        {
            get
            {
                try
                {
                    var result = bool.Parse(StorageFile.ReadFile(IsLiveStreamFileName));
                    //DebugTools.DebugMessageBoxShow(result.ToString(), "get.IsLive");
                    return result;
                }
                catch(Exception ex)
                {
                    //DebugTools.DebugMessageBoxShow(ex.ToString(), "get.IsLive");
                    return true;
                }
            }
            set
            {
                try
                {
                    StorageFile.SaveFile(IsLiveStreamFileName, value.ToString());
                    //DebugTools.DebugMessageBoxShow(value.ToString(), "set.IsLive");
                }
                catch (Exception ex)
                {
                    //DebugTools.DebugMessageBoxShow(ex.ToString(), "set.IsLive");
                }
            }
        }


        public string LiveStreamTag
        {
            get
            {
                try
                {
                    var result = StorageFile.ReadFile(LiveStreamTagFileName);
                    //DebugTools.DebugMessageBoxShow(result.ToString(), "get.LiveStream");
                    return result;

                }
                catch(Exception ex)
                {
                    //DebugTools.DebugMessageBoxShow(ex.ToString(), "get.LiveStream");
                    return string.Empty;
                }
            }
            set
            {
                try
                {
                    StorageFile.SaveFile(LiveStreamTagFileName, value);
                    //DebugTools.DebugMessageBoxShow(value, "set.LiveStream");
                }
                catch (Exception ex)
                {
                    //DebugTools.DebugMessageBoxShow(ex.ToString(), "set.LiveStream");
                }
            }
        }

        public bool IsTrackListEmpty
        {
            get
            {
                var trackList = AudioTrackInfoArray;
                return trackList.Count() == 0;
            }
        }

        public void ClearTrackList()
        {
            var trackList = AudioTrackInfoArray;
            trackList.Clear();
            AudioTrackInfoArray = trackList;
        }

        public void AddTrack(AudioTrackInfo track)
        {
            var trackList = AudioTrackInfoArray;
            trackList.Add(track);
            Logger.Info("TrackListExchange.AddTrack", "added " + track.Tag + " " + trackList.Count);
            AudioTrackInfoArray = trackList;
        }

        public int FindIndex(string tag)
        {
            Logger.Info("TrackListExchange.FindIndex", tag);
            var trackInfoArray = AudioTrackInfoArray;
            for (int i = 0; i < trackInfoArray.Count(); i++)
            {
                if (trackInfoArray[i].Tag == tag)
                {
                    Logger.Info("TrackListExchange.FindIndex", i + " found");
                    return i;
                }
            }

            Logger.Info("TrackListExchange.FindIndex", "not found");
            return -1;
        }

        public AudioTrack GetNextTrack(string tag)
        {
            Logger.Info("TrackListExchange.GetNextTrack", tag);
            int index = FindIndex(tag);
            if (index == -1)
            {
                return null;
            }

            index += 1;
            var trackInfoArray = AudioTrackInfoArray;
            if (index == trackInfoArray.Count())
            {
                index = 0;
            }

            if (index < trackInfoArray.Count())
            {
                var trackInfo = trackInfoArray[index];
                Logger.Info("TrackListExchange.GetNextTrack.nexttrack", trackInfo.Tag + " " + trackInfo.Url);
                return trackInfo.ToAudioTrack();
            }

            return null;
        }

        public AudioTrack GetPrevTrack(string tag)
        {
            int index = FindIndex(tag);
            if (index == -1)
            {
                return null;
            }

            index -= 1;
            if (index >= 0)
            {
                var trackInfo = AudioTrackInfoArray[index];
                return trackInfo.ToAudioTrack();
            }

            return null;
        }

        public AudioTrack FindTrack(string tag)
        {
            Logger.Info("TrackListExchange.FindTrack", tag);
            var trackInfoArray = AudioTrackInfoArray;
            var trackInfo = trackInfoArray.FirstOrDefault(t => t.Tag == tag);
            if (trackInfo == null)
            {
                Logger.Info("TrackListExchange.FindTrack", " not found");
            }
            else
            {
                Logger.Info("TrackListExchange.FindTrack", "found " + trackInfo.Url);
            }
            return trackInfo.ToAudioTrack();
        }

        public AudioTrack FirstTrack()
        {
            var trackInfoArray = AudioTrackInfoArray;
            var trackInfo = trackInfoArray.FirstOrDefault();
            return trackInfo.ToAudioTrack();
        }
    }
}
