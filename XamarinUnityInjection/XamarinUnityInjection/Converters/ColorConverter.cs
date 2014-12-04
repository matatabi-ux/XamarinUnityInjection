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
    /// string を Color に変換する Converter
    /// </summary>
    public class ColorConverter : IValueConverter
    {
        /// <summary>
        /// string を Color に変換する Converter
        /// </summary>
        private static readonly ColorTypeConverter Converter = new ColorTypeConverter(); 

        #region IValueConverter

        /// <summary>
        /// 文字列を Color に変換します
        /// </summary>
        /// <param name="value">文字列</param>
        /// <param name="targetType">変換後の型</param>
        /// <param name="parameter">パラメータ</param>
        /// <param name="culture">対象カルチャ</param>
        /// <returns>Color</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is string))
            {
                return default(Color);
            }

            return Converter.ConvertFrom(CultureInfo.CurrentCulture, value);
        }

        /// <summary>
        /// Color を文字列に変換します
        /// </summary>
        /// <param name="value">Color</param>
        /// <param name="targetType">変換後の型</param>
        /// <param name="parameter">パラメータ</param>
        /// <param name="culture">対象カルチャ</param>
        /// <returns>文字列</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is Color))
            {
                return default(string);
            }

            var color = ((Color)value);
            return color.ToString();
        }

        #endregion //IValueConverter
    }
}