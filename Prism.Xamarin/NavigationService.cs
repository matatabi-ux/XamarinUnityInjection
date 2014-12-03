#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2014.
// </copyright>
//-----------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.PubSubEvents;
using Prism.Mvvm.Interfaces;
using Prism.Xamarin;
using Prism.Xamarin.Events;
using Prism.Xamaron.Interfaces;
using Prism.Xamarin.ViewModels;
using Xamarin.Forms;

namespace Prism.Mvvm
{
    /// <summary>
    /// 画面遷移サービス
    /// </summary>
    public class NavigationService : INavigationService
    {
        #region Privates

        /// <summary>
        /// 画面遷移履歴情報
        /// </summary>
        private readonly static Stack<NavigatitonHistory> NavigationStack = new Stack<NavigatitonHistory>();

        /// <summary>
        /// デフォルトの Page 生成メソッド
        /// </summary>
        private static Func<Type, object> defaultPageFactory = type => Activator.CreateInstance(type);

        /// <summary>
        /// EventAggregator
        /// </summary>
        private static IEventAggregator eventAggregator;

        #endregion //Privates

        /// <summary>
        ///  画面遷移用ルート画面
        /// </summary>
        public static NavigationPage RootPage { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NavigationService(IEventAggregator eventAggregator)
        {
            RootPage = new NavigationPage();
            RootPage.Popped += OnPoped;

            NavigationService.eventAggregator = eventAggregator;
            NavigationService.eventAggregator.GetEvent<AppStateChangedEvent>().Subscribe(this.OnAppStateChanged);
        }

        /// <summary>
        /// デフォルトの Page 生成メソッドを設定します
        /// </summary>
        /// <param name="factory">デフォルトの Page 生成メソッド</param>
        public static void SetPageFactiory(Func<Type, object> factory)
        {
            NavigationService.defaultPageFactory = factory;
        }

        /// <summary>
        /// 画面遷移用ルート画面を設定します
        /// </summary>
        /// <param name="rootPage">画面遷移用ルート画面</param>
        public static void SetRootPage(NavigationPage rootPage)
        {
            RootPage.Popped -= OnPoped;
            RootPage = rootPage;
            RootPage.Popped += OnPoped;

            if (rootPage.CurrentPage == null)
            {
                return;
            }

            PrepareNavigation(rootPage.CurrentPage, null, NavigationMode.Pushed);
        }

        /// <summary>
        /// 画面遷移の前処理をします
        /// </summary>
        /// <param name="page">遷移先画面</param>
        /// <param name="parameter">遷移パラメータ</param>
        /// <param name="mode">遷移種別</param>
        private static void PrepareNavigation(Page page, object parameter, NavigationMode mode)
        {
            switch (mode)
            {
                case NavigationMode.Pushed:
                    NavigationStack.Push(new NavigatitonHistory(page.GetType(), parameter, mode));
                    break;

                case NavigationMode.Poped:
                    NavigationStack.Pop();
                    break;

                case NavigationMode.PopedToRoot:
                    for (var i = 0; i < NavigationStack.Count - 1; i++)
                    {
                        NavigationStack.Pop();
                    }
                    break;
            }

            var vm = page.BindingContext as INavigationAware;
            if (vm != null)
            {
                // 遷移時の処理
                vm.OnNavigatedTo(parameter, mode);
            }

            EventHandler onDisappearing = null;

            onDisappearing = new EventHandler(
                (sender, e) =>
                {
                    // 画面を離れる際の処理
                    page.Disappearing -= onDisappearing;

                    if (vm == null)
                    {
                        return;
                    }

                    vm.OnNavigatedFrom(false);
                });

            page.Disappearing += onDisappearing;
        }

        /// <summary>
        /// 画面遷移します
        /// </summary>
        /// <param name="pageType">遷移先クラスの型</param>
        /// <param name="parameter">遷移パラメータ</param>
        /// <returns>遷移に成功した場合 <code>true</code>、それ以外は<code>false</code></returns>
        public async Task<bool> NavigateAsync(Type pageType, object parameter = null)
        {
            var page = NavigationService.defaultPageFactory(pageType) as Page;

            if (page == null)
            {
                return false;
            }

            PrepareNavigation(page, parameter, NavigationMode.Pushed);
            await RootPage.PushAsync(page);

            return true;
        }

        /// <summary>
        /// 戻る遷移
        /// </summary>
        public async Task GoBack()
        {
            await RootPage.PopAsync();
        }

        /// <summary>
        /// 戻る遷移時の処理
        /// </summary>
        /// <param name="sender">イベント発行者</param>
        /// <param name="e">イベント引数</param>
        private static void OnPoped(object sender, NavigationEventArgs e)
        {
            var history = NavigationStack.Skip(1).FirstOrDefault();
            if (history != null)
            {
                PrepareNavigation(RootPage.CurrentPage, history.Parameter, NavigationMode.Poped);
            }
        }

        /// <summary>
        /// ホームへ戻る遷移
        /// </summary>
        public async Task GoHome()
        {
            await RootPage.PopToRootAsync();

            var history = NavigationStack.LastOrDefault();
            if (history != null)
            {
                PrepareNavigation(RootPage.CurrentPage, history.Parameter, NavigationMode.PopedToRoot);
            }
        }

        /// <summary>
        /// 戻る遷移可能フラグ
        /// </summary>
        /// <returns>戻り遷移可能な場合 <code>true</code>、それ以外は<code>false</code></returns>
        public bool CanGoBack()
        {
            return NavigationStack.Count() > 1;
        }

        /// <summary>
        /// アプリケーション実行状態変更イベントハンドラ
        /// </summary>
        /// <param name="state"></param>
        private void OnAppStateChanged(ChangedAppState state)
        {
            var vm = RootPage.CurrentPage.BindingContext as INavigationAware;
            if (vm == null)
            {
                return;
            }
            var history = NavigationStack.FirstOrDefault();
            if (history == null)
            {
                return;
            }

            switch (state.AppState)
            {
                case AppState.Running:
                    vm.OnNavigatedTo(history.Parameter, history.NavigationMode);
                    break;

                case AppState.Suspended:
                    vm.OnNavigatedFrom(true);
                    break;

                case AppState.NotRunning:
                case AppState.Terminated:
                    break;
            }
        }
    }
}