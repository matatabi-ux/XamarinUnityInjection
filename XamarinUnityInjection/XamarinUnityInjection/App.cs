using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Unity;
using Prism.Mvvm;
using Xamarin.Forms;
using XamarinUnityInjection.Services;
using XamarinUnityInjection.Views;

namespace XamarinUnityInjection
{
    /// <summary>
    /// 共通 アプリケーションクラス
    /// </summary>
    public class App
    {
        /// <summary>
        /// アプリケーションクラス参照
        /// </summary>
        public static readonly App Current;

        /// <summary>
        /// アプリ内で管理するモジュールのコンテナ
        /// </summary>
        public static readonly UnityContainer Container = new UnityContainer();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        static App()
        {
            Current = new App();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private App()
            : base()
        {
            // Prism.Mvvm.Xamarin アセンブリを読み込み可能にするおまじない
            var autoWired = ViewModelLocator.AutoWireViewModelProperty.DefaultValue;
        }

        /// <summary>
        /// アプリケーション起動処理
        /// </summary>
        public void OnLaunchApplication()
        {
            // 画面状態監視サービス を DI コンテナに登録
            Container.RegisterType<IPageStateDetectService, PageStateDetectService>(new ContainerControlledLifetimeManager());

            // ViewModel をインスタンス化するデフォルトメソッドを指定します
            ViewModelLocationProvider.SetDefaultViewModelFactory((type) => Container.Resolve(type));
        }

        /// <summary>
        /// メイン画面を取得します
        /// </summary>
        /// <returns>メイン画面</returns>
        public static Page GetMainPage()
        {
            return new TopPage();
        }
    }
}
