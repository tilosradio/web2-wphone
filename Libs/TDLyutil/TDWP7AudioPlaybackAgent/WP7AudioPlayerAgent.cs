namespace TD1990.Libs.TDLyutil.TDWP7AudioPlaybackAgent
{
    using Microsoft.Phone.BackgroundAudio;
    using System;
    using System.Windows;

    public class WP7AudioPlayerkAgent : AudioPlayerAgent
    {
        protected TrackListExchange TrackListExchangeValue;
        public virtual TrackListExchange TrackListExchange
        {
            get
            {
                if (TrackListExchangeValue == null)
                {
                    TrackListExchangeValue = new TrackListExchange();
                }

                return TrackListExchangeValue;
            }
        }

        private static volatile bool ClassInitialized;

        /// <remarks>
        /// AudioPlayer instances can share the same process. 
        /// Static fields can be used to share state between AudioPlayer instances
        /// or to communicate with the Audio Streaming agent.
        /// </remarks>
        public WP7AudioPlayerkAgent()
        {
            if (!ClassInitialized)
            {
                ClassInitialized = true;
                // Subscribe to the managed exception handler
                Deployment.Current.Dispatcher.BeginInvoke(delegate
                {
                    Application.Current.UnhandledException += AudioPlayer_UnhandledException;
                });
            }
        }

        /// Code to execute on Unhandled Exceptions
        private void AudioPlayer_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            OnAudioPlayerUnhandledException(sender, e);
        }

        /// <summary>
        /// Called when an Unhandled Exception is occured.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnAudioPlayerUnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
            // nem lehet lenyelni a hibat, e.Handled = true;

        }

        /// <summary>
        /// Called when the playstate changes, except for the Error state (see OnError)
        /// </summary>
        /// <param name="player">The BackgroundAudioPlayer</param>
        /// <param name="track">The track playing at the time the playstate changed</param>
        /// <param name="playState">The new playstate of the player</param>
        /// <remarks>
        /// Play State changes cannot be cancelled. They are raised even if the application
        /// caused the state change itself, assuming the application has opted-in to the callback.
        /// 
        /// Notable playstate events: 
        /// (a) TrackEnded: invoked when the player has no current track. The agent can set the next track.
        /// (b) TrackReady: an audio track has been set and it is now ready for playack.
        /// 
        /// Call NotifyComplete() only once, after the agent request has been completed, including async callbacks.
        /// </remarks>
        protected override void OnPlayStateChanged(BackgroundAudioPlayer player, AudioTrack track, PlayState playState)
        {
            try
            {
                switch (playState)
                {
                    case PlayState.TrackEnded:
                        OnPlayStateTrackEnded(player, track, playState);
                        break;
                    case PlayState.TrackReady:
                        OnPlayStateTrackReady(player, track, playState);
                        break;
                    case PlayState.Shutdown:
                        OnPlayStateShutdown(player, track, playState);
                        break;
                    case PlayState.Unknown:
                        OnPlayStateUnknown(player, track, playState);
                        break;
                    case PlayState.Stopped:
                        OnPlayStateStopped(player, track, playState);
                        break;
                    case PlayState.Paused:
                        OnPlayStatePaused(player, track, playState);
                        break;
                    case PlayState.Playing:
                        OnPlayStatePlaying(player, track, playState);
                        break;
                    case PlayState.BufferingStarted:
                        OnPlayStateBufferingStarted(player, track, playState);
                        break;
                    case PlayState.BufferingStopped:
                        OnPlayStateBufferingStopped(player, track, playState);
                        break;
                    case PlayState.Rewinding:
                        OnPlayStateRewinding(player, track, playState);
                        break;
                    case PlayState.FastForwarding:
                        OnPlayStateFastForwarding(player, track, playState);
                        break;
                }
            }
            catch (Exception)
            {
            }

            NotifyComplete();
        }

        protected virtual void OnPlayStateTrackEnded(BackgroundAudioPlayer player, AudioTrack track, PlayState playState)
        {
            if (TrackListExchange.IsLiveStream || track == null)
            {
                player.Track = null;
            }
            else
            {
                player.Track = TrackListExchange.GetNextTrack(track.Tag);
            }
        }

        protected virtual void SetPlayerControls(BackgroundAudioPlayer player, AudioTrack track, PlayState playState)
        {
            if (track != null && playState != PlayState.Playing)
            {
                track.BeginEdit();
                if (TrackListExchange.IsLiveStream)
                {
                    track.PlayerControls = EnabledPlayerControls.Pause | EnabledPlayerControls.SkipNext | EnabledPlayerControls.SkipPrevious;
                }
                else
                {
                    track.PlayerControls = EnabledPlayerControls.All;
                }
                track.EndEdit();
            }
        }

        protected virtual void OnPlayStateTrackReady(BackgroundAudioPlayer player, AudioTrack track, PlayState playState)
        {
            SetPlayerControls(player, track, playState);

            player.Volume = 1;
            player.Play();
        }

        protected virtual void OnPlayStateShutdown(BackgroundAudioPlayer player, AudioTrack track, PlayState playState)
        {
        }

        protected virtual void OnPlayStateUnknown(BackgroundAudioPlayer player, AudioTrack track, PlayState playState)
        {
        }

        protected virtual void OnPlayStateStopped(BackgroundAudioPlayer player, AudioTrack track, PlayState playState)
        {
        }

        protected virtual void OnPlayStatePaused(BackgroundAudioPlayer player, AudioTrack track, PlayState playState)
        {
        }

        protected virtual void OnPlayStatePlaying(BackgroundAudioPlayer player, AudioTrack track, PlayState playState)
        {
        }

        protected virtual void OnPlayStateBufferingStarted(BackgroundAudioPlayer player, AudioTrack track, PlayState playState)
        {
        }

        protected virtual void OnPlayStateBufferingStopped(BackgroundAudioPlayer player, AudioTrack track, PlayState playState)
        {
        }

        protected virtual void OnPlayStateRewinding(BackgroundAudioPlayer player, AudioTrack track, PlayState playState)
        {
        }

        protected virtual void OnPlayStateFastForwarding(BackgroundAudioPlayer player, AudioTrack track, PlayState playState)
        {
        }

        /// <summary>
        /// Called when the user requests an action using application/system provided UI
        /// </summary>
        /// <param name="player">The BackgroundAudioPlayer</param>
        /// <param name="track">The track playing at the time of the user action</param>
        /// <param name="action">The action the user has requested</param>
        /// <param name="param">The data associated with the requested action.
        /// In the current version this parameter is only for use with the Seek action,
        /// to indicate the requested position of an audio track</param>
        /// <remarks>
        /// User actions do not automatically make any changes in system state; the agent is responsible
        /// for carrying out the user actions if they are supported.
        /// 
        /// Call NotifyComplete() only once, after the agent request has been completed, including async callbacks.
        /// </remarks>
        protected override void OnUserAction(BackgroundAudioPlayer player, AudioTrack track, UserAction action, object param)
        {
            try
            {
                switch (action)
                {
                    case UserAction.Play:
                        OnUserActionPlay(player, track, action, param);
                        break;
                    case UserAction.Stop:
                        OnUserActionStop(player, track, action, param);
                        break;
                    case UserAction.Pause:
                        OnUserActionPause(player, track, action, param);
                        break;
                    case UserAction.FastForward:
                        OnUserActionFastForward(player, track, action, param);
                        break;
                    case UserAction.Rewind:
                        OnUserActionRewind(player, track, action, param);
                        break;
                    case UserAction.Seek:
                        OnUserActionSeek(player, track, action, param);
                        break;
                    case UserAction.SkipNext:
                        OnUserActionSkipNext(player, track, action, param);
                        break;
                    case UserAction.SkipPrevious:
                        OnUserActionSkipPrevious(player, track, action, param);
                        break;
                }
            }
            catch (Exception)
            {
            }

            NotifyComplete();
        }

        protected virtual void OnUserActionPlay(BackgroundAudioPlayer player, AudioTrack track, UserAction action, object param)
        {
            if (player.PlayerState != PlayState.Playing)
            {
                SetPlayerControls(player, track, player.PlayerState);

                player.Volume = 1;
                player.Play();
            }
        }

        protected virtual void OnUserActionStop(BackgroundAudioPlayer player, AudioTrack track, UserAction action, object param)
        {
            if (player.PlayerState != PlayState.Stopped)
            {
                player.Stop();
            }
        }

        protected virtual void OnUserActionPause(BackgroundAudioPlayer player, AudioTrack track, UserAction action, object param)
        {
            if (player.PlayerState == PlayState.Playing)
            {
                player.Pause();
            }
        }

        protected virtual void OnUserActionFastForward(BackgroundAudioPlayer player, AudioTrack track, UserAction action, object param)
        {
            player.FastForward();
        }

        protected virtual void OnUserActionRewind(BackgroundAudioPlayer player, AudioTrack track, UserAction action, object param)
        {
            player.Rewind();
        }

        protected virtual void OnUserActionSeek(BackgroundAudioPlayer player, AudioTrack track, UserAction action, object param)
        {
            player.Position = (TimeSpan)param;
        }

        protected virtual void OnUserActionSkipNext(BackgroundAudioPlayer player, AudioTrack track, UserAction action, object param)
        {
            player.Stop();
            player.Track = null;

            if (track != null)
            {
                player.Track = TrackListExchange.GetNextTrack(track.Tag);
                player.Position = TimeSpan.Zero;
                if (TrackListExchange.IsLiveStream && player.Track != null)
                {
                    TrackListExchange.LiveStreamTag = player.Track.Tag;
                }
            }
        }

        protected virtual void OnUserActionSkipPrevious(BackgroundAudioPlayer player, AudioTrack track, UserAction action, object param)
        {
            player.Stop();
            player.Track = null;

            if (!TrackListExchange.IsLiveStream && track != null)
            {
                player.Track = TrackListExchange.GetPrevTrack(track.Tag);
                player.Position = TimeSpan.Zero;
            }
        }

        /// <summary>
        /// Called whenever there is an error with playback, such as an AudioTrack not downloading correctly
        /// </summary>
        /// <param name="player">The BackgroundAudioPlayer</param>
        /// <param name="track">The track that had the error</param>
        /// <param name="error">The error that occured</param>
        /// <param name="isFatal">If true, playback cannot continue and playback of the track will stop</param>
        /// <remarks>
        /// This method is not guaranteed to be called in all cases. For example, if the background agent 
        /// itself has an unhandled exception, it won't get called back to handle its own errors.
        /// </remarks>
        protected override void OnError(BackgroundAudioPlayer player, AudioTrack track, Exception error, bool isFatal)
        {
            if (isFatal)
            {
                Abort();
            }
            else
            {
                NotifyComplete();
            }
        }

        /// <summary>
        /// Called when the agent request is getting cancelled
        /// </summary>
        /// <remarks>
        /// Once the request is Cancelled, the agent gets 5 seconds to finish its work,
        /// by calling NotifyComplete()/Abort().
        /// </remarks>
        protected override void OnCancel()
        {
            NotifyComplete();
        }
    }
}
