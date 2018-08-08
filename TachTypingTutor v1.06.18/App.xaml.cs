using CoreLib;
using System;
using System.Windows;
using TawmFramework;
using VMLib;

namespace TachTypingTutor_v1._06._18
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App app;
        public App()
        {
            app = this;
            Resources.MergedDictionaries.Clear();

            Resources.MergedDictionaries.Clear();
            ResourceDictionary rd = Application.LoadComponent(new Uri(string.Format("themes/{0}.xaml", "light"), UriKind.Relative)) as ResourceDictionary;
            Resources.MergedDictionaries.Add(rd);

            Bootstrapper.Load(typeof(VMBase));
            Bootstrapper.Start(typeof(SignInWindow));
            Current.Shutdown();
        }

        internal static void ChangeTheme()
        {
            string theme = Settings.GetSettings().Theme.Name;
            app.Resources.MergedDictionaries.Clear();
            app.Resources.MergedDictionaries.Clear();
            ResourceDictionary rd = Application.LoadComponent(new Uri(string.Format("themes/{0}.xaml", theme), UriKind.Relative)) as ResourceDictionary;
            app.Resources.MergedDictionaries.Add(rd);
        }
    }
}
