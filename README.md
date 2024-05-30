Aby wpełni korzystać z funkcjonalności aplikacji należy dokonać zmian w plikach: <br />
<br />
google-services.json => umieścić dane do połączenia z bazą Firebase<br />
AndroidManifest.xml => ustawić "package" połączony z Firebase<br />
Chats.xaml.cs; Donation.xaml.cs; => "FIREBASE_API" zastąpić kluczem do bazy Firebase Realtime-Database<br />
Donation.xaml.cs => "STRIPE_API" zastąpić kluczem API do Stripe.net testowym (playground) lub rzeczywistym (real money)<br />
Donation.xaml.cs => "RECEIVER_EMAIL" zastąpić emailem konta Stripe, na które mają wpływać środki<br />
