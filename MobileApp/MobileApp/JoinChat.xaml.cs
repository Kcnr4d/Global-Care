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


namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JoinChat : ContentPage
    {
        public ObservableCollection<GroupNames> Target { get; set; }
        
        public JoinChat()
        {
            InitializeComponent();
            BindingContext = this;
            Target = new ObservableCollection<GroupNames> { };
            showGroups();
            listView.ItemsSource = Target;

        }

        private async void showGroups() {
            bool a = true;
            var group = await CrossCloudFirestore.Current.Instance.CollectionGroup("groupChats").GetAsync();
            var usergruops = await CrossCloudFirestore.Current.Instance.CollectionGroup(MainLogged.chatIdInfo.Login.ToString() + "*f").GetAsync();
            var list = group.ToObjects<GroupNames>();
            var list1 = usergruops.ToObjects<GroupNames>();
            foreach (GroupNames gr in list) {
                a = true;
                foreach (GroupNames gr1 in list1) 
                {
                    if (gr1.Name == gr.Name) {
                        a = false;
                    }
                }
                if (a)
                {
                    Target.Add(gr);
                }
            }
        }

        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            
            var selectedData = (GroupNames)e.SelectedItem;
            selectedData.Id = SelectChat.number;
            await CrossCloudFirestore.Current.Instance.Collection(MainLogged.chatIdInfo.Login.ToString() + "*f").AddAsync(selectedData);
            var fr = await CrossCloudFirestore.Current.Instance.Collection("users").WhereEqualsTo("login", MainLogged.chatIdInfo.Login.ToString()).GetAsync();
            var doc = fr.Documents.First();
            await CrossCloudFirestore.Current.Instance.Collection("users").Document(doc.Id).UpdateAsync("friends", SelectChat.number + 1);
            SelectChat.GetFriends();
            await Navigation.PopModalAsync();
            

        }

        public class GroupNames
        { 
            [MapTo("Name")]
            public string Name { get; set; }
            [MapTo("Id")]
            public int Id { get; set; }

        }

        private async void Create(object sender, EventArgs e)
        {

            if (grName != null) {
                GroupNameCreate groupName = new GroupNameCreate { Name = grName.Text };
                await CrossCloudFirestore.Current.Instance.Collection(MainLogged.chatIdInfo.Login.ToString() + "*f").AddAsync(new GroupNames { 
                    Name = grName.Text,
                    Id = SelectChat.number
                });
                var fr = await CrossCloudFirestore.Current.Instance.Collection("users").WhereEqualsTo("login", MainLogged.chatIdInfo.Login.ToString()).GetAsync();
                var doc = fr.Documents.First();
                await CrossCloudFirestore.Current.Instance.Collection("users").Document(doc.Id).UpdateAsync("friends", SelectChat.number + 1);
                await CrossCloudFirestore.Current.Instance.Collection("groupChats").AddAsync(groupName);
                grName.Text = "";
                await DisplayAlert("Created group", groupName.Name.ToString(), "OK");
                
                await Navigation.PopModalAsync();
                SelectChat.GetFriends();
            }
        }

        public class GroupNameCreate
        {
            [MapTo("Name")]
            public string Name { get; set; }

        }
    }
}