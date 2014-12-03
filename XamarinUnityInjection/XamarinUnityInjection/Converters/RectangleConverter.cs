#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2014.
// </copyright>
//-----------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinUnityInjection.Converters
{
    /// <summary>
    /// string を Rectangle に変換する Converter
    /// </summary>
    public class RectangleConverter : IValueConverter
    {
        /// <summary>
        /// string を Rectangle に変換する Converter
        /// </summary>
        private static readonly RectangleTypeConverter Converter = new RectangleTypeConverter(); 

        #region IValueConverter

        /// <summary>
        /// カンマ区切り文字列を Rectangle に変換します
        /// </summary>
        /// <param name="value">カンマ区切り文字列</param>
        /// <param name="targetType">変換後の型</param>
        /// <param name="parameter">パラメータ</param>
        /// <param name="culture">対象カルチャ</param>
        /// <returns>Rectangle</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is string))
            {
                return default(Rectangle);
            }

            return Converter.ConvertFrom(CultureInfo.CurrentCulture, value);
        }

        /// <summary>
        /// Rectangle をカンマ区切り文字列に変換します
        /// </summary>
        /// <param name="value">Rectangle</param>
        /// <param name="targetType">変換後の型</param>
        /// <param name="parameter">パラメータ</param>
        /// <param name="culture">対象カルチャ</param>
        /// <returns>カンマ区切り文字列</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is Rectangle))
            {
                return default(string);
            }

            var rectangle = ((Rectangle) value);
            return string.Format("{0}, {1}, {2}, {3}", rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height);
        }

        #endregion //IValueConverter
    }
}
