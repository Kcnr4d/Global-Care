using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;
using Plugin.CloudFirestore;

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Chats : ContentPage
    {

        private string chatId;
        public static string targetChatId;
        public ObservableCollection<ChatRecord> ChatItems { get; set; } = new ObservableCollection<ChatRecord>();
        public string groupName;

        FirebaseClient firebaseclient = new FirebaseClient("FIREBASE_API");
        public Chats()
        {
            InitializeComponent();

            BindingContext = this;
            chatId = MainLogged.chatIdInfo.Login.ToString();
            var messages = firebaseclient.Child(targetChatId).AsObservable<ChatRecord>().Subscribe(
                (dbevent) => 
                    {
                        if (dbevent.Object != null) {
                            ChatItems.Add(dbevent.Object);
                        }
                    }
                );
        }

        private async void Send(object sender, EventArgs e) {

            await firebaseclient.Child(targetChatId).PostAsync(new ChatRecord
            {
                Message = MainLogged.chatIdInfo.Login + ": " + entrymsg.Text,
                Sender = TextAlignment.Start
            });
        }
    }
}