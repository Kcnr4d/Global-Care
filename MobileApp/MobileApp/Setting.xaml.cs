using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Setting : ContentPage
    {
        private bool done = false;
        public Setting()
        {
            InitializeComponent();
            FirstTime();
            
        }

        private async void FirstTime()
        {
            var local = await App.LocalDatabase.GetLocalData();
            

            if (local.IsRemembered)
            {
                remember.IsToggled = true;
                finger.IsEnabled = true;
            }
            else
            {
                remember.IsToggled = false;
                finger.IsEnabled = false;
                finger.IsToggled = false;
                await App.LocalDatabase.saveFinger(false);
            }

            if (local.FingerPrint)
            {
                finger.IsToggled = true;
            }
            else finger.IsToggled = false;

            done = true;
        }

        private async void finger_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (done)
            {
                if (finger.IsToggled)
                {
                    await App.LocalDatabase.saveFinger(true);
                }
                else await App.LocalDatabase.saveFinger(false);
            }
        }

        private async void remember_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (done)
            {
                if (remember.IsToggled)
                {
                    await App.LocalDatabase.saveRemember(true);
                    finger.IsEnabled = true;
                }
                else
                {
                    await App.LocalDatabase.saveRemember(false);
                    finger.IsEnabled = false;
                    finger.IsToggled = false;
                    await App.LocalDatabase.saveFinger(false);
                }
            }
        }

        private async void LogOut(object sender, EventArgs e)
        {
            done = false;
            await App.LocalDatabase.logOut();
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
}