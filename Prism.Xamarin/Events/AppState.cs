#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2014.
// </copyright>
//-----------------------------------------------------------------------
#endregion

namespace Prism.Xamarin.Events
{
    /// <summary>
    /// アプリケーション実行状態
    /// </summary>
    public enum AppState
    {
        /// <summary>
        /// 未起動
        /// </summary>
        NotRunning = 0,

        /// <summary>
        /// 実行中
        /// </summary>
        Running = 10,

        /// <summary>
        /// 中断中
        /// </summary>
        Suspended = 20,

        /// <summary>
        /// シャットダウン
        /// </summary>
        Terminated = 30,
    }
}
