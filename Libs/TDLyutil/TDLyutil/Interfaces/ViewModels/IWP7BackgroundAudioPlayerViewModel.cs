namespace TD1990.Libs.TDLyutil.Interfaces.ViewModels
{
    using System;
    using System.Collections.Generic;

    public interface IWP7BackgroundAudioPlayerViewModel
    {
        ICollection<IAudioTrackInfoViewModel> AudioTrackInfoCollection { get; }
        string StreamPosition { get; }
        string StreamStatus { get; }
        bool IsPlaying { get; }
        bool IsLiveStream { get; }
        double SeekMinimum { get; }
        double SeekMaximum { get; }
        double SeekPosition { get; set; }
        bool CanSeek { get; }

        void Init();
        void PlayAudio();
        void StopAudio();
        void ClearTrackInfo();
        void NextTrack();
        void PreviousTrack();
        void Fastforward();
        void Rewind();
        void Refresh();
        void PlayTrack(IAudioTrackInfoViewModel audioTrackInfo);

        event EventHandler OnPlayAudio;
        event EventHandler OnStopAudio;

        event EventHandler OnPlayStateBufferingStarted;
        event EventHandler OnPlayStateBufferingStopped;
        event EventHandler OnPlayStateError;
        event EventHandler OnPlayStateFastForwarding;
        event EventHandler OnPlayStatePaused;
        event EventHandler OnPlayStatePlaying;
        event EventHandler OnPlayStateRewinding;
        event EventHandler OnPlayStateShutdown;
        event EventHandler OnPlayStateStopped;
        event EventHandler OnPlayStateTrackEnded;
        event EventHandler OnPlayStateTrackReady;
        event EventHandler OnPlayStateUnknown;
    }
}
