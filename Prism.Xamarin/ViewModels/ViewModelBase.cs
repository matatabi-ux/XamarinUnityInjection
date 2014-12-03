#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2014.
// </copyright>
//-----------------------------------------------------------------------
#endregion

namespace Prism.Xamarin.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;
    using System.Text;
    using Microsoft.Practices.Prism.Mvvm;
    using Prism.Xamaron.Interfaces;

    /// <summary>
    /// ViewModel 基底クラス
    /// </summary>
    public class ViewModelBase : BindableBase, INavigationAware
    {
        #region INavigationAware

        /// <summary>
        /// 画面に遷移したときの処理
        /// </summary>
        /// <param name="navigationParameter">遷移パラメータ</param>
        /// <param name="navigationMode">遷移モード</param>
        public virtual void OnNavigatedTo(object navigationParameter, NavigationMode navigationMode)
        {
        }

        /// <summary>
        /// 画面から遷移するときの処理
        /// </summary>
        /// <param name="suspending">中断フラグ</param>
        public virtual void OnNavigatedFrom(bool suspending)
        {
        }

        #endregion //INavigationAware
    }
}