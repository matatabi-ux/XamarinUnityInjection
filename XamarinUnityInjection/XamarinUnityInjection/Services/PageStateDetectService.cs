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
    /// 画面状態監視サービス
    /// </summary>
    public class PageStateDetectService : IPageStateDetectService
    {
        /// <summary>
        /// 現在の画面
        /// </summary>
        private Page currentPage = null;

        /// <summary>
        /// 現在の画面
        /// </summary>
        public Page CurrentPage
        {
            get { return this.currentPage; }

            set
            {
                if (this.currentPage != null)
                {
                    this.currentPage.Appearing -= this.OnPageAppearing;
                    this.currentPage.SizeChanged -= this.OnPageSizeChanged;
                    this.currentPage.Disappearing -= this.OnPageDisappearing;
                }
                this.currentPage = value;

                if (this.currentPage != null)
                {
                    this.currentPage.Appearing += this.OnPageAppearing;
                    this.currentPage.SizeChanged += this.OnPageSizeChanged;
                    this.currentPage.Disappearing += this.OnPageDisappearing;
                }
            }
        }

        #region Events

        /// <summary>
        /// PageAppearing イベント
        /// </summary>
        public event PageStateChangedEventHandler PageAppearing;

        /// <summary>
        /// PageSizeChanged イベント
        /// </summary>
        public event PageStateChangedEventHandler PageSizeChanged;

        /// <summary>
        /// PageDisappearing イベント
        /// </summary>
        public event PageStateChangedEventHandler PageDisappearing;

        /// <summary>
        /// PageStateChanged イベントハンドラ delegate
        /// </summary>
        /// <param name="sender">イベント発行者</param>
        /// <param name="e">イベント引数</param>
        public delegate void PageStateChangedEventHandler(object sender, PageStateChangedEventArgs e);

        /// <summary>
        /// 画面の Appearing イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行者</param>
        /// <param name="e">イベント引数</param>
        private void OnPageAppearing(object sender, EventArgs e)
        {
            if (this.PageAppearing == null)
            {
                return;
            }
            this.PageAppearing(this, new PageStateChangedEventArgs(new Size(this.currentPage.Width, this.currentPage.Height)));
        }

        /// <summary>
        /// 画面の SizeChanged イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行者</param>
        /// <param name="e">イベント引数</param>
        private void OnPageSizeChanged(object sender, EventArgs e)
        {
            if (this.PageSizeChanged == null)
            {
                return;
            }
            this.PageSizeChanged(this, new PageStateChangedEventArgs(new Size(this.currentPage.Width, this.currentPage.Height)));
        }

        /// <summary>
        /// 画面の Disappearing イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行者</param>
        /// <param name="e">イベント引数</param>
        private void OnPageDisappearing(object sender, EventArgs e)
        {
            if (this.PageDisappearing == null)
            {
                return;
            }
            this.PageDisappearing(this, new PageStateChangedEventArgs(new Size(this.currentPage.Width, this.currentPage.Height)));
        }

        #endregion //Events
    }

    /// <summary>
    /// 画面状態変更イベント引数
    /// </summary>
    public class PageStateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 画面サイズ
        /// </summary>
        public Size PageSize { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="size">画面サイズ</param>
        public PageStateChangedEventArgs(Size size)
        {
            this.PageSize = size;
        }
    }
}
