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

namespace XamarinUnityInjection.ViewModels
{
    public class NumberItemViewModel : BindableBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NumberItemViewModel()
        {
        }

        #region Color

        /// <summary>
        /// 配色
        /// </summary>
        private string color = default(string);

        /// <summary>
        /// 配色
        /// </summary>
        public string Color
        {
            get { return this.color; }
            set { this.SetProperty<string>(ref this.color, value); }
        }

        #endregion //Color
        
        #region Number

        /// <summary>
        /// 数字
        /// </summary>
        private int number = default(int);

        /// <summary>
        /// 数字
        /// </summary>
        public int Number
        {
            get { return this.number; }
            set
            {
                this.SetProperty<int>(ref this.number, value);
                this.OnPropertyChanged("NumberString");
            }
        }

        public string NumberString
        {
            get
            {
                var digit = string.Format(" {0:D}", this.number);
                return digit.Substring(digit.Length - 2, 2);
            }
        }

        #endregion //Number

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

        #region Left

        /// <summary>
        /// 左余白
        /// </summary>
        private double left = default(double);

        /// <summary>
        /// 左座標
        /// </summary>
        public double Left
        {
            get { return this.left; }
            set
            {
                this.SetProperty<double>(ref this.left, value);
                this.OnPropertyChanged("LayoutBounds");
            }
        }

        #endregion //Left

        #region Top

        /// <summary>
        /// 上余白
        /// </summary>
        private double top = default(double);

        /// <summary>
        /// 上余白
        /// </summary>
        public double Top
        {
            get { return this.top; }
            set
            {
                this.SetProperty<double>(ref this.top, value);
                this.OnPropertyChanged("LayoutBounds");
            }
        }

        #endregion //Top

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
                this.OnPropertyChanged("LayoutBounds");
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
                this.OnPropertyChanged("LayoutBounds");
            }
        }

        #endregion //Height

        /// <summary>
        /// LayoutBounds
        /// </summary>
        public string LayoutBounds
        {
            get { return string.Format("{0}, {1}, {2}, {3}", this.left, this.top, this.width, this.height); }
        }

        #region Rotation

        /// <summary>
        /// 回転角度（ラジアン）
        /// </summary>
        private double rotation = default(double);

        /// <summary>
        /// rotation
        /// </summary>
        public double Rotation
        {
            get { return this.rotation; }
            set { this.SetProperty<double>(ref this.rotation, value); }
        }

        #endregion //Rotation
    }
}
