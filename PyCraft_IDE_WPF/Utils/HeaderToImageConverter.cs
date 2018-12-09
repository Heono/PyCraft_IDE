using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace PyCraft_IDE_WPF
{
    [ValueConversion(typeof(string), typeof(bool))]
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value as string).Contains(".py"))
            {
                Uri uri = new Uri("pack://application:,,,/Images/py.png");
                BitmapImage source = new BitmapImage(uri);
                return source;
            }
            else if ((value as string).Contains(".exe"))
            {
                Uri uri = new Uri("pack://application:,,,/Images/exe.png");
                BitmapImage source = new BitmapImage(uri);
                return source;
            }
            else if ((value as string).Contains(".png") || (value as string).Contains(".jpg"))
            {
                Uri uri = new Uri("pack://application:,,,/Images/img.png");
                BitmapImage source = new BitmapImage(uri);
                return source;
            }
            else if ((value as string).Contains("."))
            {
                Uri uri = new Uri("pack://application:,,,/Images/file.png");
                BitmapImage source = new BitmapImage(uri);
                return source;
            }
            else
            {
                Uri uri = new Uri("pack://application:,,,/Images/folder.png");
                BitmapImage source = new BitmapImage(uri);
                return source;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Cannot convert back");
        }
    }
}
