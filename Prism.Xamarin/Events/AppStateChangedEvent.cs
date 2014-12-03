#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2014.
// </copyright>
//-----------------------------------------------------------------------
#endregion

namespace Prism.Xamarin.Events
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Practices.Prism.PubSubEvents;

    /// <summary>
    /// アプリケーション実行状態の変更イベント
    /// </summary>
    public class AppStateChangedEvent : PubSubEvent<ChangedAppState>
    {
    }

    /// <summary>
    /// アプリケーション実行状態の変更イベント引数
    /// </summary>
    public class ChangedAppState : EventArgs
    {
        /// <summary>
        /// 新しい実行状態
        /// </summary>
        public AppState AppState { get; protected set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="state">アプリケーション実行状態</param>
        public ChangedAppState(AppState state)
        {
            this.AppState = state;
        }
    }
}
