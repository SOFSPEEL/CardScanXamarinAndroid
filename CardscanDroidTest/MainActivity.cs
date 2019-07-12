using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Com.Getbouncer.Cardscan;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace CardscanDroidTest
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private TextView _txtCardNumber;
        private TextView _txtExpiryDate;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            AppCenter.Start("6711801e-a4a5-467a-932f-fb6375fc1870",
                   typeof(Analytics), typeof(Crashes));

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            _txtCardNumber = FindViewById<TextView>(Resource.Id.txtCardNumber);
            _txtExpiryDate = FindViewById<TextView>(Resource.Id.txtExpiryDate);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            ScanActivity.WarmUp(this);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (ScanActivity.IsScanResult(requestCode))
            {
                if (resultCode == Result.Ok && data != null)
                {
                    var scanResult = ScanActivity.CreditCardFromResult(data);
                    ProcessScanResult(scanResult);
                }
                else
                {
                    Toast.MakeText(this, "Failed to capture card number", ToastLength.Short).Show();
                }
            }
        }

        private void ProcessScanResult(CreditCard creditCard)
        {
            var cardNumber = creditCard?.Number ?? "";
            if (cardNumber.Length == 16)
            {
                cardNumber = cardNumber.Insert(12, "  ").Insert(8, "  ").Insert(4, "  ");
            }
            else if (cardNumber.Length == 15 ||
                     cardNumber.Length == 14)
            {
                cardNumber = cardNumber.Insert(10, "  ").Insert(4, "  ");
            }

            _txtCardNumber.Text = cardNumber;

            if (!string.IsNullOrEmpty(creditCard.ExpiryMonth) && !string.IsNullOrEmpty(creditCard.ExpiryYear))
            {
                _txtExpiryDate.Text = $"{creditCard?.ExpiryMonth}/{creditCard?.ExpiryYear}".Trim('/');
            }
            else
            {
                _txtExpiryDate.Text = "n/a";
            }
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            try
            {
                _txtCardNumber.Text = string.Empty;
                _txtExpiryDate.Text = string.Empty;

                ScanActivity.StartDebug(this);               
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "Failed to open Scan View: " + ex.Message, ToastLength.Short).Show();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

