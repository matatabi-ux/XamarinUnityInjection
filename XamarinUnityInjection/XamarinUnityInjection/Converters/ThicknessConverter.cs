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
    /// string を Thickness に変換する Converter
    /// </summary>
    public class ThicknessConverter : IValueConverter
    {
        /// <summary>
        /// string を Thickness に変換する Converter
        /// </summary>
        private static readonly ThicknessTypeConverter Converter = new ThicknessTypeConverter(); 

        #region IValueConverter

        /// <summary>
        /// カンマ区切り文字列を Thickness に変換します
        /// </summary>
        /// <param name="value">カンマ区切り文字列</param>
        /// <param name="targetType">変換後の型</param>
        /// <param name="parameter">パラメータ</param>
        /// <param name="culture">対象カルチャ</param>
        /// <returns>Thickness</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is string))
            {
                return default(Thickness);
            }

            return Converter.ConvertFrom(CultureInfo.CurrentCulture, value);
        }

        /// <summary>
        /// Thickness をカンマ区切り文字列に変換します
        /// </summary>
        /// <param name="value">Thickness</param>
        /// <param name="targetType">変換後の型</param>
        /// <param name="parameter">パラメータ</param>
        /// <param name="culture">対象カルチャ</param>
        /// <returns>カンマ区切り文字列</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is Thickness))
            {
                return default(string);
            }

            var thickness = ((Thickness)value);
            return string.Format("{0}, {1}, {2}, {3}", thickness.Left, thickness.Top, thickness.Right, thickness.Bottom);
        }

        #endregion //IValueConverter
    }}
