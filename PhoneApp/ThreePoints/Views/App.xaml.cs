using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThreePoints
{
    public partial class App : Application
    {
        HttpClient _client;
        public App()
        {
            InitializeComponent();

            _client = new HttpClient();
            MainPage = new MainPage(_client);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            if (_client is null)
            {
                _client = new HttpClient();
            }
        }
    }
}
