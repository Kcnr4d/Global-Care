using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using Plugin.CloudFirestore;
using SQLite;

namespace MobileApp
{
    public partial class MainPage : ContentPage
    {
        private Users user = new Users { };
        public MainPage()
        {
            InitializeComponent();
            CheckIfRemember();
        }



        private async void CheckIfRemember() {
            LocalData user = await App.LocalDatabase.GetLocalData();
            if (user.IsRemembered && user.Login != null) {
                Application.Current.MainPage = new NavigationPage(new Remember());
            }
        }

        private async void Login(object sender, EventArgs e)
        {
            if (loginEntry.Text == null)
            {
                loginEntry.Placeholder = "Username can't be empty";
                loginEntry.PlaceholderColor = Color.FromRgba(255, 0, 0, 128);
            }
            if (passwordEntry.Text == null)
            {
                passwordEntry.Placeholder = "Password can't be empty";
                passwordEntry.PlaceholderColor = Color.FromRgba(255, 0, 0, 128);
            }


            if (loginEntry.Text != null && passwordEntry.Text != null)
            {
                IQuerySnapshot findUser = await CrossCloudFirestore.Current.Instance.Collection("users").WhereEqualsTo("login", loginEntry.Text).GetAsync();


                user = findUser.ToObjects<Users>().First();
                if (user != null)
                {
                    if (user.Pass == passwordEntry.Text)
                    {
                        await App.LocalDatabase.saveLogin(user.Login);
                        Application.Current.MainPage = new MainLogged();
                    }
                }
            }
        }

        private async void Register(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Register());
        }
    }
}
