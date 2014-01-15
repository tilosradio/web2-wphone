namespace TD1990.Libs.TDLyutil.IsolatedStorage
{
    using System.IO;
    using System.IO.IsolatedStorage;

    public static class StorageFile
    {
        public static void SaveFile(string fileName, string content)
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream file = storage.CreateFile(fileName))
                {
                    using (StreamWriter swNew = new StreamWriter(file))
                    {
                        swNew.Write(content);
                    }
                }
            }
        }

        public static void SaveFile<T>(string fileName, T content)
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream file = storage.CreateFile(fileName))
                {
                    using (StreamWriter swNew = new StreamWriter(file))
                    {
                        var ser = new System.Xml.Serialization.XmlSerializer(typeof(T));
                        ser.Serialize(swNew, content);
                    }
                }
            }
        }

        public static string ReadFile(string fileName)
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream file = storage.OpenFile(fileName, FileMode.Open))
                {
                    using (StreamReader swNew = new StreamReader(file))
                    {
                        return swNew.ReadToEnd();
                    }
                }
            }
        }

        public static T ReadFile<T>(string fileName)
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream file = storage.OpenFile(fileName, FileMode.Open))
                {
                    using (StreamReader swNew = new StreamReader(file))
                    {
                        var ser = new System.Xml.Serialization.XmlSerializer(typeof(T));
                        return (T)ser.Deserialize(swNew);
                    }
                }
            }
        }


    }
}
