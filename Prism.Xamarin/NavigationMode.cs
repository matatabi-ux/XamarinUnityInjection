#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2014.
// </copyright>
//-----------------------------------------------------------------------
#endregion

namespace Prism.Xamarin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 遷移種別
    /// </summary>
    public enum NavigationMode
    {
        /// <summary>
        /// 新規遷移
        /// </summary>
        Pushed,

        /// <summary>
        /// 戻り遷移
        /// </summary>
        Poped,

        /// <summary>
        /// トップに戻る遷移
        /// </summary>
        PopedToRoot,
    }
}
