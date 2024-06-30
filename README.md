Changes that need to be made in order for application to function properly: <br />
<br />
google-services.json => add firebase conncetion<br />
AndroidManifest.xml => set "package" so its connected to firebase<br />
Chats.xaml.cs; Donation.xaml.cs; => replace "FIREBASE_API" with Firebase Realtime-Database API key<br />
Donation.xaml.cs => replace "STRIPE_API" with stripe.net API key (test or live)<br />
Donation.xaml.cs => replace "RECEIVER_EMAIL" with stripe account email, which is dedicated to receive money<br />


Aby wpełni korzystać z funkcjonalności aplikacji należy dokonać zmian w plikach: <br />
<br />
google-services.json => umieścić dane do połączenia z bazą Firebase<br />
AndroidManifest.xml => ustawić "package" połączony z Firebase<br />
Chats.xaml.cs; Donation.xaml.cs; => "FIREBASE_API" zastąpić kluczem do bazy Firebase Realtime-Database<br />
Donation.xaml.cs => "STRIPE_API" zastąpić kluczem API do Stripe.net testowym lub rzeczywistym<br />
Donation.xaml.cs => "RECEIVER_EMAIL" zastąpić emailem konta Stripe, na które mają wpływać środki<br />


