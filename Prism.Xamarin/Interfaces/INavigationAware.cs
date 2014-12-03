#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2014.
// </copyright>
//-----------------------------------------------------------------------
#endregion

namespace Prism.Xamaron.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Prism.Xamarin;

    /// <summary>
    /// 遷移検知インタフェース
    /// </summary>
    public interface INavigationAware
    {
        /// <summary>
        /// 画面に遷移したときの処理
        /// </summary>
        /// <param name="navigationParameter">遷移パラメータ</param>
        /// <param name="navigationMode">遷移モード</param>
        void OnNavigatedTo(object navigationParameter, NavigationMode navigationMode);

        /// <summary>
        /// 画面から遷移するときの処理
        /// </summary>
        /// <param name="suspending">中断フラグ</param>
        void OnNavigatedFrom(bool suspending);
    }
}
