using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Firebase.Database;
using Plugin.CloudFirestore.Attributes;
using Firebase.Database.Query;
using Plugin.CloudFirestore;
using System.Collections.ObjectModel;
using Stripe;
using System.Threading;


namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Donation : ContentPage
    {
        
        public ObservableCollection<Donated> Don { get; set; } = new ObservableCollection<Donated>();
        public double alreadyDonated = 0;
        public double alreadyDonated1 = 0;
        FirebaseClient firebaseclient = new FirebaseClient("FIREBASE_API");

        public Token stripeToken;
        public TokenService TokenService;
        public string TestApiKey = "STRIPE_API";

        public Donation()
        {
           
            InitializeComponent();
            BindingContext = this;
            var money = firebaseclient.Child("d1").AsObservable<Donated>().Subscribe(
                (dbevent) =>
                {
                    if (dbevent.Object != null)
                    {
                        Donated add = new Donated { 
                            amm = dbevent.Object.amm / 1000,
                            dot = "dot.jpd"
                        };
                        alreadyDonated = dbevent.Object.amm;
                        Don.Clear();
                        Don.Add(add);
                    }
                }
                );
        }

        public async Task PaymentAsync() {
            bool isTransectionSuccess = false;
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;
            try
            {
                await Task.Run(async () =>
                {
                    var Token = CreateToken();
                    if (Token != null)
                    {
                        isTransectionSuccess = MakePayment();
                    }
                });
            }
            catch (Exception ex) {
                throw ex;
            }
        }


        public bool MakePayment()
        {
            try
            {
                StripeConfiguration.SetApiKey("STRIPE_API");
                var options = new ChargeCreateOptions
                {
                    Amount = (long)float.Parse(donationEntry.Text),
                    Currency = "PLN",
                    Description = "Donation",
                    StatementDescriptor = "Custom",
                    Capture = true,
                    ReceiptEmail = "RECEIVER_EMAIL",
                    Source = stripeToken.Id
                };
                var service = new ChargeService();
                Charge charge = service.Create(options);
                return true;
            }
            catch (Exception ex) {
                throw ex;
            
            }
        }

        public string CreateToken()
        {
            try
            {
                StripeConfiguration.SetApiKey(TestApiKey);
                var service = new ChargeService();
                var tokenoptions = new TokenCreateOptions
                {
                    Card = new CreditCardOptions
                    {
                        Number = cardNumber.Text,
                        ExpYear = Int32.Parse(expirationYear.Text),
                        ExpMonth = Int32.Parse(expirationMonth.Text),
                        Cvc = cvc.Text,
                    }

                };
                TokenService = new TokenService();
                stripeToken = TokenService.Create(tokenoptions);
                return stripeToken.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private async void Donate(object sender, EventArgs e)
        {
            await PaymentAsync();
            await firebaseclient.Child("d1").DeleteAsync();
            await firebaseclient.Child("d1").PostAsync(new Donated
            {

                amm = alreadyDonated + Int32.Parse(donationEntry.Text)
            });

        }

        public class Donated {
            [MapTo("amm")]
            public double amm { get; set; }
            public string dot { get; set; }

        }
    }
}