using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace AirlineTicketOffice.Main.Converters
{
    public class NullRectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            RectConverter rc = new RectConverter();

            if (value == null)
            {
                return new Rect(0, 0, 0, 0);
            }

            return rc.ConvertFromString((String)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
