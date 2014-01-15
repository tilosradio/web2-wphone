
namespace TD1990.Libs.TDLyutil.SharedMedia
{
    using System;
    using System.IO;
    using System.IO.IsolatedStorage;
    using System.Windows;
    using System.Windows.Resources;
    using System.Windows.Media.Imaging;
    public class SharedMedia
    {
        /// <summary>
        /// Copy Resource Icon to isolated storage Shared/Media folder.
        /// </summary>
        /// <param name="resourceFileName">Resource file relative path</param>
        /// <param name="fileName">Icon file name</param>
        /// <returns>if succesed true else false</returns>
        public static bool CopyResourceIconToSharedMedia(string resourceFileName, string fileName, bool rewrite = true)
        {
            bool result = false;
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (rewrite || !storage.FileExists("Shared/Media/" + fileName))
                {
                    StreamResourceInfo iconResource = Application.GetResourceStream(
                        new Uri(resourceFileName, UriKind.Relative));

                    // The Tile images MUST be located in the Shared/Media directory in order 
                    // to get picked up by the Now Playing Tile in the Music + Videos Hub
                    using (IsolatedStorageFileStream file = storage.CreateFile("Shared/Media/" + fileName))
                    {
                        int chunkSize = 4096;
                        byte[] bytes = new byte[chunkSize];
                        int byteCount;

                        while ((byteCount = iconResource.Stream.Read(bytes, 0, chunkSize)) > 0)
                        {
                            file.Write(bytes, 0, byteCount);
                        }
                    }
                }
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Copy picture stream to shared media folder.
        /// </summary>
        /// <param name="stream">stream of picture</param>
        /// <param name="fileName">file name in shared media</param>
        /// <param name="rewrite">rewrite file if exist</param>
        /// <returns>true if success, false if error</returns>
        public static string CopyPictureStreamToSharedMedia(Stream stream, string fileName, bool rewrite = true)
        {
            string result = null;

            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                var fullname = "Shared\\Media\\" + fileName;
                if (rewrite || !storage.FileExists(fullname))
                {
                    using (IsolatedStorageFileStream file = storage.CreateFile(fullname))
                    {
                        int chunkSize = 4096;
                        byte[] bytes = new byte[chunkSize];
                        int byteCount;

                        while ((byteCount = stream.Read(bytes, 0, chunkSize)) > 0)
                        {
                            file.Write(bytes, 0, byteCount);
                        }
                    }
                }
                result = fullname;
            }
            return result;
        }

        public static string CopyPictureToSharedShellContent(BitmapImage image, string fileName, bool rewrite = true)
        {
            string result = null;

            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                // http://msdn.microsoft.com/en-us/library/ff402541(v=vs.92).aspx

                var fullname = "Shared/ShellContent/" + fileName;
                fullname = fullname.Replace(':', '.');
                if (rewrite || !storage.FileExists(fullname))
                {
                    using (IsolatedStorageFileStream file = storage.CreateFile(fullname))
                    {
                        WriteableBitmap wb = new WriteableBitmap(image);
                        wb.SaveJpeg(file, 173, 173, 0, 75);
                        file.Flush();
                    }
                }
                result = "isostore:/" + fullname;
            }
            return result;
        }
    }
}
