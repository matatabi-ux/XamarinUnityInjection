#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2014.
// </copyright>
//-----------------------------------------------------------------------
#endregion

using System;
using Microsoft.Practices.Prism.Mvvm;
using Xamarin.Forms;

namespace Prism.Mvvm
{
    /// <summary>
    /// ViewModel のロケータ
    /// </summary>
    public static class ViewModelLocator
    {
        /// <summary>
        /// ViewModel を自動的にバインドするか否かを表す BindableProperty
        /// </summary>
        public static readonly BindableProperty AutoWireViewModelProperty
            = BindableProperty.Create(
            "AutoWireViewModel",
            typeof(bool),
            typeof(ViewModelLocator),
            false,
            BindingMode.TwoWay,
            null,
            AutoWireViewModelChanged);

        /// <summary>
        /// AutoWireViewModelProperty の変更イベントハンドラ
        /// </summary>
        /// <param name="bindable">BindableObject</param>
        /// <param name="oldValue">変更前の値</param>
        /// <param name="newValue">変更後の値</param>
        private static void AutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as IView;

            if (view == null)
            {
                throw new Exception("Your views must implement IView");
            }

            ViewModelLocationProvider.AutoWireViewModelChanged(view);
        }

        /// <summary>
        /// AutoWireViewModel を取得します
        /// </summary>
        /// <param name="bindable">バインドされている BindingContext</param>
        /// <returns>自動的にバインドされた ViewModel が利用可能な場合 <c>true</c>  それ以外は <c>false</c></returns>
        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            if (bindable != null)
            {
                return (bool)bindable.GetValue(AutoWireViewModelProperty);
            }
            return false;
        }

        /// <summary>
        /// AutoWireViewModel を 設定します
        /// </summary>
        /// <param name="bindable">バインドされている BindingContext</param>
        /// <param name="value"><c>true</c> を設定した場合自動的に ViewModel がバインドされます</param>
        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            if (bindable != null)
            {
                bindable.SetValue(AutoWireViewModelProperty, value);
            }
        }
    }
}
