namespace TD1990.Libs.TDLyutil.IsolatedStorage
{
    using System;
    using System.Collections.Generic;
    using System.IO.IsolatedStorage;
    using TD1990.Libs.TDLyutil.DebugTools;
    using TD1990.Libs.TDLyutil.Interfaces.DebugTools;

    /// <summary>
    /// IsolatedStorageSettings.ApplicationSettings
    /// 
    /// usage:
    ///     AppSetting<int> CountSetting;
    ///     CountSetting=new AppSetting<int>("Count", 2);
    ///     
    ///     if (CountSetting.HasValue)
    ///     {
    ///         var i=CountSetting.Value;
    ///     }
    ///     
    ///     CountSetting.Value=6;
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AppSetting<T>
    {
        /// <summary>
        /// Key of the setting.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Default Value
        /// </summary>
        public T DefValue { get; private set; }

        /// <summary>
        /// Use cache for value
        /// </summary>
        public bool UseCache { get; private set; }

        /// <summary>
        /// Value in cache
        /// </summary>
        public T CachedValue { get; private set; }

        /// <summary>
        /// Has value in the cache
        /// </summary>
        public bool HasCachedValue { get; private set; }

        /// <summary>
        /// Does every set write to isolated storage?
        /// </summary>
        public bool AutoSave { get; set; }

        private ILogger EmptyLogger;

        private ILogger LoggerValue;
        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        public ILogger Logger
        {
            get
            {
                return LoggerValue ?? EmptyLogger;
            }
            set
            {
                LoggerValue = value;
            }

        }

        /// <summary>
        /// Create AppSetting
        /// </summary>
        /// <param name="key">Key name</param>
        /// <param name="defvalue">Default value if no value in the isolated storage</param>
        /// <param name="useCache">Store value in cache</param>
        /// <param name="autoSave">Does every set write to isolated storage?</param>
        public AppSetting(string key, T defvalue, bool useCache = true, bool autoSave = true, ILogger logger = null)
        {
            Logger = logger;
            ThisLock = new object();
            EmptyLogger = new EmptyLogger();
            Key = key;
            DefValue = defvalue;
            UseCache = !autoSave || useCache;
            HasCachedValue = false;
            AutoSave = autoSave;
        }

        public void Save()
        {
            lock (ThisLock)
            {
                T v = Value;
                TrySave(v);
            }
        }

        /// <summary>
        /// Value in IsolatedStorageSettings
        /// </summary>
        public bool HasValue
        {
            get
            {
                lock (ThisLock)
                {
                    if (UseCache && HasCachedValue)
                    {
                        return true;
                    }
                    return IsolatedStorageSettings.ApplicationSettings.Contains(Key);
                }
            }
        }

        /// <summary>
        /// Value
        /// </summary>
        public T Value
        {
            get
            {
                lock (ThisLock)
                {
                    if (UseCache && HasCachedValue)
                    {
                        return CachedValue;
                    }

                    // http://msdn.microsoft.com/en-us/library/system.io.isolatedstorage.isolatedstoragesettings(v=vs.105).aspx

                    T d;
                    try
                    {
                        d = (T)IsolatedStorageSettings.ApplicationSettings[Key];
                    }
                    catch (KeyNotFoundException)
                    {
                        Logger.Info("AppSetting.Value.get", Key + " key not found");
                        d = DefValue;
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("AppSetting.Value.get", Key, ex);
                        d = DefValue;
                    }

                    if (UseCache)
                    {
                        CachedValue = d;
                        HasCachedValue = true;
                    }
                    return d;
                }
            }
            set
            {
                lock (ThisLock)
                {
                    if (UseCache)
                    {
                        CachedValue = value;
                        HasCachedValue = true;
                    }
                    if (AutoSave)
                    {
                        TrySave(value);
                    }
                }
            }
        }

        private void TrySave(T value)
        {
            try
            {
                IsolatedStorageSettings.ApplicationSettings[Key] = value;
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
            catch (Exception ex)
            {
                Logger.Error("AppSetting.TrySave", Key, ex);
            }
        }

        private object ThisLock;
    }
}
