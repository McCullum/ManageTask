using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace ManageTask.Validations
{
    [ValueConversion(typeof(double), typeof(Brush))]
    public class RowConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double TotalWorkedHour = double.Parse(value.ToString());
            if (TotalWorkedHour > 10)
            {
                return Brushes.Red;
            }
            else {
                return Brushes.White;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
