#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2014.
// </copyright>
//-----------------------------------------------------------------------
#endregion

namespace Prism.Mvvm.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 画面遷移サービスのインタフェース
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// 画面遷移します
        /// </summary>
        /// <param name="pageType">遷移先クラスの型</param>
        /// <param name="parameter">遷移パラメータ</param>
        /// <returns>遷移に成功した場合 <code>true</code>、それ以外は<code>false</code></returns>
        Task<bool> NavigateAsync(Type pageType, object parameter = null);
        
        /// <summary>
        /// 戻る遷移
        /// </summary>
        Task GoBack();

        /// <summary>
        /// ホームへ戻る遷移
        /// </summary>
        Task GoHome();

        /// <summary>
        /// 戻る遷移可能フラグ
        /// </summary>
        /// <returns>戻り遷移可能な場合 <code>true</code>、それ以外は<code>false</code></returns>
        bool CanGoBack();
    }
}
