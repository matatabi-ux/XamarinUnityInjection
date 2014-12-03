#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2014.
// </copyright>
//-----------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Xamarin.Forms;
using XamarinUnityInjection.Services;

namespace XamarinUnityInjection.Views
{
    /// <summary>
    /// 画面状態監視用添付プロパティ付与クラス
    /// </summary>
    public static class PageStateDetect
    {
        /// <summary>
        /// Page の状態を監視するか否かを表す BindableProperty
        /// </summary>
        public static readonly BindableProperty DetectStateProperty
            = BindableProperty.Create(
            "DetectState",
            typeof(bool),
            typeof(PageStateDetect),
            false,
            BindingMode.TwoWay,
            null,
            DetectStateChanged);

        /// <summary>
        /// DetectStateProperty の変更イベントハンドラ
        /// </summary>
        /// <param name="bindable">BindableObject</param>
        /// <param name="oldValue">変更前の値</param>
        /// <param name="newValue">変更後の値</param>
        private static void DetectStateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Page;

            if (view == null)
            {
                throw new Exception("Your views isn't Page");
            }
            App.Container.Resolve<IPageStateDetectService>().CurrentPage = view;
        }
    }
}
