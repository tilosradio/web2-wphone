//namespace TD1990.TilosRadio.WP7.Helpers
//{
//    using System;
//    using System.Globalization;
//    using System.Windows.Data;

//    public class DayToDayNameConverter : IValueConverter
//    {

//        static CultureInfo hunculture = new CultureInfo("hu-HU");

//        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            int day = (int)value;

//            if (day > -1)
//            {
//                DateTimeFormatInfo dtfi = hunculture.DateTimeFormat;
//                return dtfi.DayNames[day];
//            }
//            else
//            {
//                return "minden";
//            }

//        }

//        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
//        {
//            throw new NotImplementedException();
//        }


//    }
//}
