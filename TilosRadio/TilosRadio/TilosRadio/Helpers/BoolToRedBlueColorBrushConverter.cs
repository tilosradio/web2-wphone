namespace TD1990.TilosRadio.WP7.Helpers
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class BoolToRedBlueColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? boolval = value as bool?;

            if (boolval.HasValue && boolval.Value)
            {
                return App.Current.Resources["TilosRedBrush"];
            }
            else
            {
                return App.Current.Resources["TilosBlueBrush"];
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }


    }
}
