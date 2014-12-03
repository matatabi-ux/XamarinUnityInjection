#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2014.
// </copyright>
//-----------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Unity;
using Xamarin.Forms;
using XamarinUnityInjection.Services;

namespace XamarinUnityInjection.ViewModels
{
    /// <summary>
    /// 最初の画面の ViewModel
    /// </summary>
    public class TopPageViewModel : BindableBase
    {
        #region Privates

        /// <summary>
        /// 画面状態監視サービス
        /// </summary>
        private IPageStateDetectService pageStateDetectService;

        #endregion //Privates

        #region CenterX

        /// <summary>
        /// 中心 X 座標
        /// </summary>
        private double centerX = default(double);

        /// <summary>
        /// 中心 X 座標
        /// </summary>
        public double CenterX
        {
            get { return this.centerX; }
            set { this.SetProperty<double>(ref this.centerX, value); }
        }

        #endregion //CenterX

        #region CenterY

        /// <summary>
        /// 中心 Y 座標
        /// </summary>
        private double centerY = default(double);

        /// <summary>
        /// 中心 Y 座標
        /// </summary>
        public double CenterY
        {
            get { return this.centerY; }
            set { this.SetProperty<double>(ref this.centerY, value); }
        }

        #endregion //CenterY

        #region Width

        /// <summary>
        /// 横幅
        /// </summary>
        private double width = default(double);

        /// <summary>
        /// 横幅
        /// </summary>
        public double Width
        {
            get { return this.width; }
            set
            {
                this.SetProperty<double>(ref this.width, value);
                this.Draw();
            }
        }

        #endregion //Width

        #region Height

        /// <summary>
        /// 縦幅
        /// </summary>
        private double height = default(double);

        /// <summary>
        /// 縦幅
        /// </summary>
        public double Height
        {
            get { return this.height; }
            set
            {
                this.SetProperty<double>(ref this.height, value);
                this.Draw();
            }
        }

        #endregion //Height

        #region HourHand

        /// <summary>
        /// 時針
        /// </summary>
        private HandItemViewModel hourHand;

        /// <summary>
        /// 時針
        /// </summary>
        public HandItemViewModel HourHand
        {
            get { return this.hourHand; }
            set { this.SetProperty<HandItemViewModel>(ref this.hourHand, value); }
        }

        #endregion //HourHand

        #region MinuteHand

        /// <summary>
        /// 分針
        /// </summary>
        private HandItemViewModel minuteHand;

        /// <summary>
        /// 分針
        /// </summary>
        public HandItemViewModel MinuteHand
        {
            get { return this.minuteHand; }
            set { this.SetProperty<HandItemViewModel>(ref this.minuteHand, value); }
        }

        #endregion //MinuteHand

        #region SecondHand

        /// <summary>
        /// 秒針
        /// </summary>
        private HandItemViewModel secondHand;

        /// <summary>
        /// 秒針
        /// </summary>
        public HandItemViewModel SecondHand
        {
            get { return this.secondHand; }
            set { this.SetProperty<HandItemViewModel>(ref this.secondHand, value); }
        }

        #endregion //SecondHand

        #region TickItems プロパティ

        /// <summary>
        /// 目盛り
        /// </summary>
        private ObservableCollection<TickItemViewModel> tickItems = new ObservableCollection<TickItemViewModel>();

        /// <summary>
        /// 目盛り の取得と設定
        /// </summary>
        public ObservableCollection<TickItemViewModel> TickItems
        {
            get { return this.tickItems; }
            set { this.SetProperty<ObservableCollection<TickItemViewModel>>(ref this.tickItems, value); }
        }

        #endregion //TickItems プロパティ

        #region NumberItems プロパティ

        /// <summary>
        /// 数字
        /// </summary>
        private ObservableCollection<NumberItemViewModel> numberItems = new ObservableCollection<NumberItemViewModel>();

        /// <summary>
        /// 数字 の取得と設定
        /// </summary>
        public ObservableCollection<NumberItemViewModel> NumberItems
        {
            get { return this.numberItems; }
            set { this.SetProperty<ObservableCollection<NumberItemViewModel>>(ref this.numberItems, value); }
        }

        #endregion //NumberItems プロパティ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="navigationService">画面遷移サービス（DI コンテナにより自動注入される）</param>
        /// <param name="repository">リポジトリ（DI コンテナにより自動注入される）</param>
        public TopPageViewModel(IPageStateDetectService pageStateDetectService)
        {
            this.pageStateDetectService = pageStateDetectService;

            // Unity を利用して ViewModel のインスタンスを生成
            this.hourHand = App.Container.Resolve<HandItemViewModel>(
                new PropertyOverride("Color", new ResolvedParameter<string>("HourHandColor")));

            this.minuteHand = App.Container.Resolve<HandItemViewModel>(
                new PropertyOverride("Color", new ResolvedParameter<string>("MinuteHandColor")));

            this.secondHand = App.Container.Resolve<HandItemViewModel>(
                new PropertyOverride("Color", new ResolvedParameter<string>("SecondHandColor")));

            this.tickItems.Clear();
            for (var i = 0; i < 60; i++)
            {
                this.tickItems.Add(App.Container.Resolve<TickItemViewModel>());
            }

            this.numberItems.Clear();
            for (var i = 1; i < 13; i++)
            {
                this.numberItems.Add(App.Container.Resolve<NumberItemViewModel>(new PropertyOverride("Number", i)));
            }

            this.pageStateDetectService.PageAppearing += this.OnPageAppearing;
        }

        /// <summary>
        /// 画面表示イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行者</param>
        /// <param name="e">イベント引数</param>
        private void OnPageAppearing(object sender, PageStateChangedEventArgs e)
        {
            this.pageStateDetectService.PageAppearing -= this.OnPageAppearing;

            this.Width = e.PageSize.Width;
            this.Height = e.PageSize.Height;

            this.Draw();
            this.TimerStart();

            this.pageStateDetectService.PageSizeChanged += this.OnPageSizeChanged;
        }

        /// <summary>
        /// 画面サイズ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行者</param>
        /// <param name="e">イベント引数</param>
        private void OnPageSizeChanged(object sender, PageStateChangedEventArgs e)
        {
            this.Width = e.PageSize.Width;
            this.Height = e.PageSize.Height;

            this.Draw();
        }

        /// <summary>
        /// 時計盤を描画します
        /// </summary>
        public void Draw()
        {
            if (this.width < 1 || this.height < 1)
            {
                return;
            }

            this.CenterX = this.width  / 2;
            this.CenterY = this.height / 2;

            var radius = Math.Min(this.width, this.height);

            var index = 0;
            foreach (var tickItem in this.tickItems)
            {
                double size = 0.45 * radius / (index % 5 == 0 ? 15 : 30);
                double radians = index * 2 * Math.PI / this.tickItems.Count;

                tickItem.Left = this.centerX + 0.45 * radius * Math.Sin(radians) - size / 2;
                tickItem.Top = this.centerY - 0.45 * radius * Math.Cos(radians) - size / 2;
                tickItem.Width = size;
                tickItem.Height = size;
                tickItem.Rotation = 180 * radians / Math.PI;

                index++;
            }

            foreach (var numberItem in this.numberItems)
            {
                var radians = numberItem.Number * 2 * Math.PI / this.numberItems.Count;

                numberItem.Width = 40;
                numberItem.Height = 25;
                numberItem.Left = this.centerX + 0.39 * radius * Math.Sin(radians) - numberItem.Width / 4;
                numberItem.Top = this.centerY - 0.39 * radius * Math.Cos(radians) - numberItem.Height / 2;
                numberItem.Rotation = 0;
            }
        }

        /// <summary>
        /// タイマー処理
        /// </summary>
        private void TimerStart()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    DateTime dateTime = DateTime.Now;

                    var radius = Math.Min(this.width, this.height);

                    double radians = ((dateTime.Hour % 12) * 60 + dateTime.Minute + 360) * 2 * Math.PI / 720;
                    this.hourHand.Width = 0.125 * 0.45 * radius;
                    this.hourHand.Height = 0.65 * 0.45 * radius;
                    this.hourHand.Left = this.centerX - 0.5 * this.hourHand.Width - 0.4 * this.hourHand.Height * Math.Sin(radians);
                    this.hourHand.Top = this.centerY - 0.5 * this.hourHand.Height + 0.4 * this.hourHand.Height * Math.Cos(radians);
                    this.hourHand.Rotation = 30 * (dateTime.Hour % 12) + 0.5 * dateTime.Minute;

                    radians = (dateTime.Minute * 60 + dateTime.Second + 1800) * 2 * Math.PI / 3600;
                    this.minuteHand.Width = 0.05 * 0.45 * radius;
                    this.minuteHand.Height = 0.8 * 0.45 * radius;
                    this.minuteHand.Left = this.centerX - 0.5 * this.minuteHand.Width - 0.4 * this.minuteHand.Height * Math.Sin(radians);
                    this.minuteHand.Top = this.centerY - 0.5 * this.minuteHand.Height + 0.4 * this.minuteHand.Height * Math.Cos(radians);
                    this.minuteHand.Rotation = 6 * dateTime.Minute + 0.1 * dateTime.Second;

                    radians = (dateTime.Second + 30) * 2 * Math.PI / 60;
                    this.secondHand.Width = 0.02 * 0.45 * radius;
                    this.secondHand.Height = 1.1 * 0.45 * radius;
                    this.secondHand.Left = this.centerX - 0.5 * this.secondHand.Width - 0.35 * this.secondHand.Height * Math.Sin(radians);
                    this.secondHand.Top = this.centerY - 0.5 * this.secondHand.Height + 0.35 * this.secondHand.Height * Math.Cos(radians);
                    this.secondHand.Rotation = 6 * dateTime.Second;

                    await Task.Delay(500);
                }
            });
        }
    }
}