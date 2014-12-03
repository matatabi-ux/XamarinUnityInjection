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
using Xamarin.Forms;

namespace XamarinUnityInjection.Services
{
    /// <summary>
    /// 画面状態監視サービスのインタフェース
    /// </summary>
    public interface IPageStateDetectService
    {
        /// <summary>
        /// 現在の画面
        /// </summary>
        Page CurrentPage { get; set; }

        /// <summary>
        /// PageAppearing イベント
        /// </summary>
        event PageStateDetectService.PageStateChangedEventHandler PageAppearing;

        /// <summary>
        /// PageSizeChanged イベント
        /// </summary>
        event PageStateDetectService.PageStateChangedEventHandler PageSizeChanged;

        /// <summary>
        /// PageDisappearing イベント
        /// </summary>
        event PageStateDetectService.PageStateChangedEventHandler PageDisappearing;
    }
}
