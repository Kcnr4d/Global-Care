using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.CloudFirestore;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Remember : ContentPage
    {
        
        public Remember()
        {
            InitializeComponent();
            loginEntry.Text = App.LocalDatabase.GetLocalData().Result.Login.ToString();
            PopFingerprint();
        }

        private async void PopFingerprint() {
            var local = await App.LocalDatabase.GetLocalData();
            if (local.FingerPrint)
            {
                var avaible = await CrossFingerprint.Current.IsAvailableAsync();
                if (!avaible)
                {
                    await DisplayAlert("Error", "No Biometrics available", "OK");
                    return;
                }
                var auth = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration("Log in", "Log into your account using biometrics"));

                if (auth.Authenticated) {
                    Application.Current.MainPage = new MainLogged();
                }
            }
        
        }

        private async void Login(object sender, EventArgs e)
        {
            if (passwordEntry.Text == null)
            {
                passwordEntry.Placeholder = "Password can't be empty";
                passwordEntry.PlaceholderColor = Color.FromRgba(255, 0, 0, 128);
            }


            if (passwordEntry.Text != null)
            {
                var local = await App.LocalDatabase.GetLocalData();
                var findUser = await CrossCloudFirestore.Current.Instance.Collection("users").WhereEqualsTo("login", local.Login).GetAsync();


                var user = findUser.ToObjects<Users>().First();

                if (user.Pass == passwordEntry.Text)
                {
                    Application.Current.MainPage = new MainLogged();
                }

            }
        }


        private async void Forget(object sender, EventArgs e) {
            await App.LocalDatabase.logOut();
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }

    }
}