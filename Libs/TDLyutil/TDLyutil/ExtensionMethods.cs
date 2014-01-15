
namespace TD1990.Libs.TDLyutil
{
    using System;
    
    public static class ExtensionMethods
    {
        public static bool HasPayload(this string item)
        {
            return !string.IsNullOrWhiteSpace(item);
        }

        public static Uri ToUri(this string item, UriKind uriKind = UriKind.RelativeOrAbsolute)
        {
            return string.IsNullOrWhiteSpace(item) ? null : new Uri(item, uriKind);
        }

    }
}
