using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.CloudFirestore;
using Firebase;

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Posts : ContentPage
    {
        ObservableCollection<Announcement> announcements { get; set; } = new ObservableCollection<Announcement>();

        
        public Posts()
        {

            InitializeComponent();
            announcements = new ObservableCollection<Announcement>();
            GetPosts();
            AnnouncementView.ItemsSource = announcements;
        }

        private async void GetPosts() {
            var posts = await CrossCloudFirestore.Current.Instance.CollectionGroup("posts").GetAsync();
            var obj = posts.ToObjects<Announcement>();
            foreach (Announcement an in obj) {
                announcements.Add(an);
            }

        }
        async private void AnnouncementView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            var selectedData = (Announcement)e.SelectedItem;
            String thisTitle = selectedData.Title;
            String thisContent = selectedData.Content;
            await DisplayAlert(thisTitle, thisContent, "OK");
        }

    }

}