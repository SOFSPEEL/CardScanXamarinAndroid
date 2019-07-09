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

namespace CardscanDroidBinding
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private TextView _txtCardNumber;
        private TextView _txtExpiryDate;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
         
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

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (ScanActivity.IsScanResult(requestCode))
            {
                if (resultCode == Result.Ok && data != null)
                {
                    var scanResult = ScanActivity.CreditCardFromResult(data);

                    var cardNumber = scanResult?.Number ?? "";
                    for (int i = 4; i < cardNumber.Length; i += 4)
                    {
                        cardNumber = cardNumber.Insert(i + i / 4 - 1, " ");
                    }

                    _txtCardNumber.Text = cardNumber;
                    _txtExpiryDate.Text = $"{scanResult?.ExpiryMonth}/{scanResult?.ExpiryYear}".Trim('/');
                }
                else
                {
                    Toast.MakeText(this, "Failed to capture card number", ToastLength.Short).Show();
                }
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
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

