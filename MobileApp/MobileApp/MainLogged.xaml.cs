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
    public partial class MainLogged : FlyoutPage
    {
        public static LocalData chatIdInfo = new LocalData { };
        public MainLogged()
        {
            
            InitializeComponent();
            flyout.flyoutlistview.ItemSelected += OnSelectedItem;
        }

        private async void OnSelectedItem(object sender, SelectedItemChangedEventArgs e)
        {
            var local = await App.LocalDatabase.GetLocalData();
            chatIdInfo.Login = local.Login;
            var item = e.SelectedItem as FlyoutPageItem;
            if (item != null) {
                if (!item.IsLogOut)
                {
                    Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetPage));
                    flyout.flyoutlistview.SelectedItem = null;
                    IsPresented = false;
                }
                else {
                    await App.LocalDatabase.logOut();
                    Application.Current.MainPage = new NavigationPage(new MainPage());

                }
            }
        }
    }
}