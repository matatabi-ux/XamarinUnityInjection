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
using Microsoft.Practices.Prism.Mvvm;
using Xamarin.Forms;

namespace XamarinUnityInjection.Views
{
    /// 最初の画面
    /// </summary>
    public partial class TopPage : ContentPage, IView
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TopPage()
        {
            this.InitializeComponent();
        }

        #region IView

        /// <summary>
        /// データソース
        /// </summary>
        public object DataContext
        {
            get { return this.BindingContext; }
            set { this.BindingContext = value; }
        }

        #endregion //IView
    }
}
