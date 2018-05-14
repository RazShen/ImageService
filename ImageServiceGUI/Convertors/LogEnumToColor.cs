using System;
using System.Globalization;
using System.Windows.Data;


namespace ImageServiceGUI.Convertors
	{
	/// <summary>
	/// This is a convertor (implements IValueConvertor) from log inum to colour.
	/// </summary>
	class LogEnumToColor: IValueConverter
		{
		/// <summary>
		/// Convert
		/// </summary>
		/// <param name="value"> log enum</param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
			{
			if (targetType.Name != "Brush")
				{
				throw new InvalidOperationException("Must convert to a brush!");
				}

			string type = (string)value;
			switch (type)
				{
				case "INFO":
					return System.Windows.Media.Brushes.LightSeaGreen;
				case "WARNING":
					return System.Windows.Media.Brushes.DarkOrange;
				case "ERROR":
					return System.Windows.Media.Brushes.PaleVioletRed;
				default:
					return System.Windows.Media.Brushes.White;
				}

			}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			{
			throw new NotImplementedException();
			}
		}
	}
