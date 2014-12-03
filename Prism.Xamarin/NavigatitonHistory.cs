#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2014.
// </copyright>
//-----------------------------------------------------------------------
#endregion

namespace Prism.Mvvm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Prism.Xamarin;

    /// <summary>
    /// 画面遷移履歴情報
    /// </summary>
    public class NavigatitonHistory
    {
        /// <summary>
        /// 画面遷移履歴情報
        /// </summary>
        /// <param name="pageType">遷移先画面</param>
        /// <param name="parameter">遷移パラメータ</param>
        /// <param name="mode">遷移種別</param>
        public NavigatitonHistory(Type pageType, object parameter, NavigationMode mode)
        {
            this.PageType = pageType;
            this.Parameter = parameter;
            this.NavigationMode = mode;
        }

        /// <summary>
        /// 遷移先画面
        /// </summary>
        public Type PageType { get; private set; }

        /// <summary>
        /// 遷移パラメータ
        /// </summary>
        public object Parameter { get; private set; }

        /// <summary>
        /// 遷移種別
        /// </summary>
        public NavigationMode NavigationMode { get; private set; }
    }
}
