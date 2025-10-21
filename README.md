Changes that need to be made in order for application to function properly: <br />
<br />
google-services.json => add firebase conncetion<br />
AndroidManifest.xml => set "package" so its connected to firebase<br />
Chats.xaml.cs; Donation.xaml.cs; => replace "FIREBASE_API" with Firebase Realtime-Database API key<br />
Donation.xaml.cs => replace "STRIPE_API" with stripe.net API key (test or live)<br />
Donation.xaml.cs => replace "RECEIVER_EMAIL" with stripe account email, which is dedicated to receive money<br />

<br />
<br />
<br />



