namespace TD1990.Libs.TDLyutil.Shell
{
    using Microsoft.Phone.Shell;
    using System;
    using System.Linq;
    using System.Reflection;

    public static class WP8Tile
    {
        private static Version TargetedVersion = new Version(7, 10, 8858);
        public static bool IsTargetedVersion { get { return Environment.OSVersion.Version >= TargetedVersion; } }

        public static void UpdateFlipTile(ShellTile tile, WP8FlipTileData tileData)
        {
            if (IsTargetedVersion)
            {
                // Get the new FlipTileData type.
                Type flipTileDataType = Type.GetType("Microsoft.Phone.Shell.FlipTileData, Microsoft.Phone");

                // Get the constructor for the new FlipTileData class and assign it to our variable to hold the Tile properties.
                var updateTileData = flipTileDataType.GetConstructor(new Type[] { }).Invoke(null);

                // Set the properties. 
                SetProperty(updateTileData, "Title", tileData.Title);
                SetProperty(updateTileData, "Count", tileData.Count);
                SetProperty(updateTileData, "BackTitle", tileData.BackTitle);
                SetProperty(updateTileData, "BackContent", tileData.BackContent);
                SetProperty(updateTileData, "SmallBackgroundImage", tileData.SmallBackgroundImage);
                SetProperty(updateTileData, "BackgroundImage", tileData.BackgroundImage);
                SetProperty(updateTileData, "BackBackgroundImage", tileData.BackBackgroundImage);
                SetProperty(updateTileData, "WideBackgroundImage", tileData.WideBackgroundImage);
                SetProperty(updateTileData, "WideBackBackgroundImage", tileData.WideBackBackgroundImage);
                SetProperty(updateTileData, "WideBackContent", tileData.WideBackContent);

                // Get the ShellTile type so we can call the new version of "Update" that takes the new Tile templates.
                Type shellTileType = Type.GetType("Microsoft.Phone.Shell.ShellTile, Microsoft.Phone");

                // Invoke the new version of ShellTile.Update.
                shellTileType.GetMethod("Update").Invoke(tile, new Object[] { updateTileData });
            }
            else
            {
                var oldTileData = new StandardTileData();
                oldTileData.BackBackgroundImage = tileData.BackBackgroundImage;
                oldTileData.BackContent = tileData.BackContent;
                oldTileData.BackgroundImage = tileData.BackgroundImage;
                oldTileData.BackTitle = tileData.BackTitle;
                oldTileData.Count = tileData.Count;
                oldTileData.Title = tileData.Title;
                tile.Update(oldTileData);
            }
        }

        public static void UpdateCycleTile(ShellTile tile, WP8CycleTileData tileData)
        {
            if (IsTargetedVersion)
            {
                Type cycleTileDataType = Type.GetType("Microsoft.Phone.Shell.CycleTileData, Microsoft.Phone");
                var updateTileData = cycleTileDataType.GetConstructor(new Type[] { }).Invoke(null);

                SetProperty(updateTileData, "Title", tileData.Title);
                SetProperty(updateTileData, "Count", tileData.Count);
                SetProperty(updateTileData, "SmallBackgroundImage", tileData.SmallBackgroundImage);
                SetProperty(updateTileData, "CycleImages", tileData.CycleImages.ToArray());

                Type shellTileType = Type.GetType("Microsoft.Phone.Shell.ShellTile, Microsoft.Phone");
                shellTileType.GetMethod("Update").Invoke(tile, new Object[] { updateTileData });
            }
            else
            {
                var oldTileData = new StandardTileData();
                oldTileData.BackBackgroundImage = tileData.SmallBackgroundImage;
                oldTileData.BackContent = tileData.Title;
                oldTileData.BackgroundImage = tileData.SmallBackgroundImage;
                oldTileData.BackTitle = tileData.Title;
                oldTileData.Count = tileData.Count;
                oldTileData.Title = tileData.Title;
                tile.Update(oldTileData);
            }
        }


        //public static void UpdateFlipTile(
        //            string title,
        //            string backTitle,
        //            string backContent,
        //            string wideBackContent,
        //            int count,
        //            Uri tileId,
        //            Uri smallBackgroundImage,
        //            Uri backgroundImage,
        //            Uri backBackgroundImage,
        //            Uri wideBackgroundImage,
        //            Uri wideBackBackgroundImage)
        //{
        //    if (IsTargetedVersion)
        //    {
        //        // Get the new FlipTileData type.
        //        Type flipTileDataType = Type.GetType("Microsoft.Phone.Shell.FlipTileData, Microsoft.Phone");

        //        // Get the ShellTile type so we can call the new version of "Update" that takes the new Tile templates.
        //        Type shellTileType = Type.GetType("Microsoft.Phone.Shell.ShellTile, Microsoft.Phone");

        //        // Loop through any existing Tiles that are pinned to Start.
        //        foreach (var tileToUpdate in ShellTile.ActiveTiles)
        //        {
        //            // Look for a match based on the Tile's NavigationUri (tileId).
        //            if (tileToUpdate.NavigationUri.ToString() == tileId.ToString())
        //            {
        //                // Get the constructor for the new FlipTileData class and assign it to our variable to hold the Tile properties.
        //                var UpdateTileData = flipTileDataType.GetConstructor(new Type[] { }).Invoke(null);

        //                // Set the properties. 
        //                SetProperty(UpdateTileData, "Title", title);
        //                SetProperty(UpdateTileData, "Count", count);
        //                SetProperty(UpdateTileData, "BackTitle", backTitle);
        //                SetProperty(UpdateTileData, "BackContent", backContent);
        //                SetProperty(UpdateTileData, "SmallBackgroundImage", smallBackgroundImage);
        //                SetProperty(UpdateTileData, "BackgroundImage", backgroundImage);
        //                SetProperty(UpdateTileData, "BackBackgroundImage", backBackgroundImage);
        //                SetProperty(UpdateTileData, "WideBackgroundImage", wideBackgroundImage);
        //                SetProperty(UpdateTileData, "WideBackBackgroundImage", wideBackBackgroundImage);
        //                SetProperty(UpdateTileData, "WideBackContent", wideBackContent);

        //                // Invoke the new version of ShellTile.Update.
        //                shellTileType.GetMethod("Update").Invoke(tileToUpdate, new Object[] { UpdateTileData });
        //                break;
        //            }
        //        }
        //    }

        //}

        public static void CreateCycleTile(WP8CycleTileData tileData)
        {
            if (IsTargetedVersion)
            {
                Type cycleTileDataType = Type.GetType("Microsoft.Phone.Shell.CycleTileData, Microsoft.Phone");
                object newTileData = cycleTileDataType.GetConstructor(new Type[] { }).Invoke(null);
                SetProperty(newTileData, "Title", tileData.Title);
                SetProperty(newTileData, "Count", tileData.Count);
                SetProperty(newTileData, "SmallBackgroundImage", tileData.SmallBackgroundImage);
                SetProperty(newTileData, "CycleImages", tileData.CycleImages.ToArray());

                Type shellTileType = Type.GetType("Microsoft.Phone.Shell.ShellTile, Microsoft.Phone");
                MethodInfo createmethod = shellTileType.GetMethod("Create", new[] { typeof(Uri), typeof(ShellTileData), typeof(bool) });
                createmethod.Invoke(null, new object[] { tileData.TileId, newTileData, true });
            }

        }

        public static void CreateFlipTile(WP8FlipTileData tileData)
        {
            if (IsTargetedVersion)
            {
                // Get the new FlipTileData type.
                Type flipTileDataType = Type.GetType("Microsoft.Phone.Shell.FlipTileData, Microsoft.Phone");

                // Get the constructor for the new FlipTileData class and assign it to our variable to hold the Tile properties.
                var newTileData = flipTileDataType.GetConstructor(new Type[] { }).Invoke(null);

                // Set the properties. 
                SetProperty(newTileData, "Title", tileData.Title);
                SetProperty(newTileData, "Count", tileData.Count);
                SetProperty(newTileData, "BackTitle", tileData.BackTitle);
                SetProperty(newTileData, "BackContent", tileData.BackContent);
                SetProperty(newTileData, "SmallBackgroundImage", tileData.SmallBackgroundImage);
                SetProperty(newTileData, "BackgroundImage", tileData.BackgroundImage);
                SetProperty(newTileData, "BackBackgroundImage", tileData.BackBackgroundImage);
                SetProperty(newTileData, "WideBackgroundImage", tileData.WideBackgroundImage);
                SetProperty(newTileData, "WideBackBackgroundImage", tileData.WideBackBackgroundImage);
                SetProperty(newTileData, "WideBackContent", tileData.WideBackContent);

                // Get the ShellTile type so we can call the new version of "Update" that takes the new Tile templates.
                Type shellTileType = Type.GetType("Microsoft.Phone.Shell.ShellTile, Microsoft.Phone");

                // Invoke the new version of ShellTile.Create
                MethodInfo createmethod = shellTileType.GetMethod("Create", new[] { typeof(Uri), typeof(ShellTileData), typeof(bool) });
                createmethod.Invoke(null, new object[] { tileData.TileId, newTileData, true });
            }
        }

        private static void SetProperty(object instance, string name, object value)
        {
            var setMethod = instance.GetType().GetProperty(name).GetSetMethod();
            setMethod.Invoke(instance, new object[] { value });
        }

    }
}
