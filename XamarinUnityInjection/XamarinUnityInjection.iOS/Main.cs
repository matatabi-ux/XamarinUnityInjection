using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using XamarinUnityInjection.ViewModels;

namespace XamarinUnityInjection.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // プラットフォーム依存のプロパティを注入
            App.Container
                .RegisterInstance<string>("HourHandColor", "#FF8469")    // 時針の色 
                .RegisterInstance<string>("MinuteHandColor", "#FF461C")  // 分針の色
                .RegisterInstance<string>("SecondHandColor", "#CC3816")  // 秒針の色
                .RegisterType<HandItemViewModel>(new InjectionProperty("Color", "#7F4234"))
                .RegisterType<TickItemViewModel>(new InjectionProperty("Color", "#7F4234")) // 目盛りの色
                .RegisterType<NumberItemViewModel>(new InjectionProperty("Color", "#7F230E"), new InjectionProperty("Number", 0)); // 数字の色

            // アプリケーション起動処理
            App.Current.OnLaunchApplication();

            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
