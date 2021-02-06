using TweetMemories.Services;
using TweetMemories.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TweetMemories
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<MyUserDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
