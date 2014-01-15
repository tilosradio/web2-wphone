namespace TD1990.Libs.TDLyutil.Shell
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class WP8FlipTileData
    {
        public Uri TileId { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
        public string BackTitle { get; set; }
        public string BackContent { get; set; }
        public string WideBackContent { get; set; }
        public Uri SmallBackgroundImage { get; set; }
        public Uri BackgroundImage { get; set; }
        public Uri BackBackgroundImage { get; set; }
        public Uri WideBackgroundImage { get; set; }
        public Uri WideBackBackgroundImage { get; set; }
    }
}
