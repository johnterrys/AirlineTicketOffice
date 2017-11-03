using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace AirlineTicketOffice.Main.Converters
{
    public class NullBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value == null)
            {
                return new SolidColorBrush(Colors.Black);
            }

            BrushConverter bc = new BrushConverter();

            return bc.ConvertFromInvariantString((String)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}