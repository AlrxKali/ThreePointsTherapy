using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ThreePoints.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThreePoints.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        HttpClient _client;
        public LoginPage()
        {
            _client= new HttpClient();

            InitializeComponent();
            SetHttpClientHeaders(_client);
        }

        public async void OnLoginClicked(object sender, EventArgs e)
        {
            string userName = GetUserName(e);
            string pass = GetPassword(e);

            if (userName.Equals("alrxkali") == true && pass.Equals("123456") == true)
            {
                Application.Current.MainPage = new NavigationPage(new WelcomePage());
            }
            else
            {
                ResetUI();
            }
            //var model = new Login() { UserName = userName, Password = pass};
            //var json = JsonConvert.SerializeObject(model);

            //HttpContent content = new StringContent(json);

            //try
            //{
            //    HttpResponseMessage response = await _client.PutAsync(@"https://172.19.112.1:7061/api/users/login/", content);

            //    if (response.IsSuccessStatusCode)
            //    {
            //        Application.Current.MainPage = new NavigationPage(new WelcomePage());
            //    }
            //    else
            //    {
            //        ResetUI();
            //    }
            //}
            //catch(Exception ex)
            //{
            //    throw ex;
            //}
        }
        private string GetUserName(EventArgs e)
        {
            string un = txtUserName.Text.Trim();
            return un;
        }

        private string GetPassword(EventArgs e)
        {
            string ps = txtPassword.Text.Trim();
            return ps;
        }

        private void ResetUI()
        {
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
        }

        private static void SetHttpClientHeaders(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}