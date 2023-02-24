using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ThreePoints.Views;
using Xamarin.Forms;

namespace ThreePoints
{
    public partial class MainPage : ContentPage
    {
        Image splashImage;
        HttpClient _client;
        public MainPage(HttpClient client)
        {
            this._client = client;

            InitializeComponent();
            SplashScreen();
        }
        private void SplashScreen()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            var sub = new AbsoluteLayout();

            splashImage = new Image
            {
                Source = "threePoints.png",
                WidthRequest = 250,
                HeightRequest = 250
            };
            AbsoluteLayout.SetLayoutFlags(splashImage,
                AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashImage,
                new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            sub.Children.Add(splashImage);

            this.BackgroundColor = Color.FromHex("#FFFFFF");
            Content = sub;

        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await splashImage.ScaleTo(1, 2000);
            await splashImage.ScaleTo(0.9, 1500, Easing.Linear);
            await splashImage.ScaleTo(150, 1200, Easing.Linear);

            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}
