using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.CloudFirestore;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        public Register()
        {
            InitializeComponent();
        }

        private void Login(object sender, EventArgs e) {
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }


        private async void RegisterAccount(object sender, EventArgs e) {
            bool allow = true;

            if (loginEntry.Text == null) {
                loginEntry.Placeholder = "Username can't be empty";
                loginEntry.PlaceholderColor = Color.FromRgba(255, 0, 0, 128);
                allow = false;
            }
            else if (loginEntry.Text.Length > 20)
            {
                loginEntry.Placeholder = "Username is too long";
                loginEntry.PlaceholderColor = Color.FromRgba(255, 0, 0, 128);
                allow = false;
            }
            if (emailEntry.Text == null)
            {
                emailEntry.Placeholder = "Email can't be empty";
                emailEntry.PlaceholderColor = Color.FromRgba(255, 0, 0, 128);
                allow = false;
            }
            else if (!emailEntry.Text.Contains('@') || !emailEntry.Text.Contains('.') || emailEntry.Text.Length > 40)
            {
                emailEntry.Placeholder = "Invalid email";
                emailEntry.PlaceholderColor = Color.FromRgba(255, 0, 0, 128);
                allow = false;
            }
            if (passwordEntry.Text == null)
            {
                passwordEntry.Placeholder = "Password can't be empty";
                passwordEntry.PlaceholderColor = Color.FromRgba(255, 0, 0, 128);
                allow = false;
            }
            else if (passwordEntry.Text.Length > 30)
            {
                passwordEntry.Placeholder = "Password is too long";
                passwordEntry.PlaceholderColor = Color.FromRgba(255, 0, 0, 128);
                allow = false;
            }
            if (repeatedPasswordEntry.Text != passwordEntry.Text || repeatedPasswordEntry.Text == null)
            {
                repeatedPasswordEntry.Text = "";
                repeatedPasswordEntry.Placeholder = "Passwords are not the same";
                repeatedPasswordEntry.PlaceholderColor = Color.FromRgba(255, 0, 0, 128);
                allow = false;
            }

            if (allow)
            {
                IQuerySnapshot checkLogin = await CrossCloudFirestore.Current.Instance.Collection("users").WhereEqualsTo("login", loginEntry.Text).GetAsync();
                IQuerySnapshot checkEmail = await CrossCloudFirestore.Current.Instance.Collection("users").WhereEqualsTo("email", emailEntry.Text).GetAsync();

                if (checkLogin.ToObjects<Users>().Count() != 0)
                {
                    loginEntry.Text = "";
                    loginEntry.Placeholder = "Username is already taken";
                    loginEntry.PlaceholderColor = Color.FromRgba(255, 0, 0, 128);
                    allow = false;
                }
                if (checkEmail.ToObjects<Users>().Count() != 0)
                {
                    emailEntry.Text = "";
                    emailEntry.Placeholder = "Email is already registered";
                    emailEntry.PlaceholderColor = Color.FromRgba(255, 0, 0, 128);
                    allow = false;
                }
                if (allow)
                {
                    Users user = new Users
                    {
                        Login = loginEntry.Text,
                        Email = emailEntry.Text,
                        Pass = passwordEntry.Text
                    };

                    await CrossCloudFirestore.Current.Instance.Collection("users").AddAsync(user);
                    reg.Text = "Succefully registered";
                    reg.IsEnabled = false;
                }
            }
        }
    }
}