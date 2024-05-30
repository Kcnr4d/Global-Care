using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.CloudFirestore;
using Plugin.CloudFirestore.Attributes;
using Microsoft.Azure.Cosmos.Linq;

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectChat : ContentPage
    {
        public static ObservableCollection<ChatTargetInfo> Target { get; set; }
        public static int number = 0;
        public SelectChat()
        {
            InitializeComponent();
            GetFriends();
            listView.ItemsSource = Target;
        }

        public static async void GetFriends() {
            Target = new ObservableCollection<ChatTargetInfo> { };
            var login = await App.LocalDatabase.GetLocalData();
            var freindCount = await CrossCloudFirestore.Current.Instance.Collection("users").WhereEqualsTo("login", login.Login.ToString()).GetAsync();
            var user = freindCount.ToObjects<Users>().First();
            
            for (int i = 0; i < user.FriendsCount; i++)
            {
                var friend = await CrossCloudFirestore.Current.Instance.Collection(login.Login.ToString() + "*f").WhereEqualsTo("Id", i).GetAsync();
                ChatTargetInfo showFirend = friend.ToObjects<ChatTargetInfo>().First();
                Target.Add(showFirend);
            }
            number = user.FriendsCount;
            
        }
        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) {
                return;
            }
            var selectedData = (ChatTargetInfo)e.SelectedItem;
            Chats.targetChatId = selectedData.Name;
            await Navigation.PushModalAsync(new Chats());
            listView.SelectedItem = null;
        }

        public class ChatTargetInfo
        {
            [MapTo("Name")]
            public string Name { get; set; }
        }

        private async void Join(object sender, EventArgs e) {
            await Navigation.PushModalAsync(new JoinChat());
            
        }

    }
}