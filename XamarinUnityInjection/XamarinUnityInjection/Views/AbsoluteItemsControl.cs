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
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinUnityInjection.Views
{
    /// <summary>
    /// ItemsControl 風 AbsoluteLayout
    /// </summary>
    public class AbsoluteItemsControl<T> : AbsoluteLayout
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AbsoluteItemsControl()
        {
        }

        #region ItemsSource

        /// <summary>
        /// ItemsSource BindableProperty
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create<AbsoluteItemsControl<T>, ObservableCollection<T>>(
            p => p.ItemsSource,
            new ObservableCollection<T>(),
            BindingMode.OneWay,
            null,
            OnItemsChanged);

        /// <summary>
        /// ItemsSource CLR プロパティ
        /// </summary>
        public ObservableCollection<T> ItemsSource
        {
            get { return (ObservableCollection<T>)this.GetValue(ItemsSourceProperty); }
            set { this.SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// ItemsSource 変更イベントハンドラ
        /// </summary>
        /// <param name="bindable">BindableObject</param>
        /// <param name="oldValue">古い値</param>
        /// <param name="newValue">新しい値</param>
        private static void OnItemsChanged(BindableObject bindable, ObservableCollection<T> oldValue, ObservableCollection<T> newValue)
        {
            var control = bindable as AbsoluteItemsControl<T>;
            if (control == null)
            {
                return;
            }
            control.ItemsSource.CollectionChanged += control.OnCollectionChanged;
            control.Children.Clear();

            foreach (var item in newValue)
            {
                var content = control.ItemTemplate.CreateContent();
                View view;
                var cell = content as ViewCell;
                if (cell != null)
                {
                    view = cell.View;
                }
                else
                {
                    view = (View)content;
                }

                view.BindingContext = item;
                control.Children.Add(view);
            }

            control.UpdateChildrenLayout();
            control.InvalidateLayout();
        }

        #endregion //ItemsSource

        #region ItemTemplate

        /// <summary>
        /// ItemTemplate BindableProperty
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create<AbsoluteItemsControl<T>, DataTemplate>(
            p => p.ItemTemplate,
            default(DataTemplate));

        /// <summary>
        /// ItemTemplate CLR プロパティ
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)this.GetValue(ItemTemplateProperty); }
            set { this.SetValue(ItemTemplateProperty, value); }
        }

        #endregion //ItemTemplate

        /// <summary>
        /// Items の変更イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                this.Children.RemoveAt(e.OldStartingIndex);
                this.UpdateChildrenLayout();
                this.InvalidateLayout();
            }

            if (e.NewItems == null)
            {
                return;
            }
            foreach (T item in e.NewItems)
            {
                var content = this.ItemTemplate.CreateContent();

                View view;
                var cell = content as ViewCell;
                if (cell != null)
                {
                    view = cell.View;
                }
                else
                {
                    view = (View)content;
                }

                view.BindingContext = item;
                this.Children.Insert(ItemsSource.IndexOf(item), view);
            }

            this.UpdateChildrenLayout();
            this.InvalidateLayout();
        }
    }
}