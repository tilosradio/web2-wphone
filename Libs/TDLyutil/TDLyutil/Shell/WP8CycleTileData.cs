namespace TD1990.Libs.TDLyutil.Shell
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class WP8CycleTileData
    {
        public Uri TileId { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
        public IList<Uri> CycleImages { get; set; }
        public Uri SmallBackgroundImage { get; set; }
    }
}
