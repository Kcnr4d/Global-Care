using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.CloudFirestore;

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Report : ContentPage
    {
        public Report()
        {
            InitializeComponent();
        }
        async private void addAnnouncement_Clicked(object sender, EventArgs e)
        {
            if (groupName.Text != null && location.Text != null && description.Text != null)
            {
                string newTitle = groupName.Text + ", " + location.Text;
                string newContent = description.Text;
                Announcement post = new Announcement { Title = newTitle, Content = newContent };
                await CrossCloudFirestore.Current.Instance.Collection("posts").AddAsync(post);

                await DisplayAlert("Zgłoszenie", "Zgłoszenie zostało wysłane", "OK");

                groupName.Text = string.Empty;
                description.Text = string.Empty;
                location.Text = string.Empty;
            }
            else
            {
                await DisplayAlert("Dane", "Proszę uzupełnić wszystkie dane", "OK");
            }

        }

    }
}