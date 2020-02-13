using System;
using Android.App;
using Android.Content;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace App1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, Android.Hardware.ISensorEventListener
    {
        private SensorManager sensorService;
        float lightSensorValue;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            //fab.Click += FabOnClick;

            // Get a SensorManager
            sensorService = (SensorManager)GetSystemService(Context.SensorService);

            // Get a Light Sensor
            var lightSensor = sensorService.GetDefaultSensor(SensorType.Light);
            sensorService.RegisterListener(this, lightSensor, Android.Hardware.SensorDelay.Game);

            fab.Click += delegate {
                Toast.MakeText(this, lightSensor.Vendor, ToastLength.Long).Show();
            };
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
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
            // Get a SensorManager
            sensorService = (SensorManager)GetSystemService(Context.SensorService);

            // Get a Light Sensor
            var lightSensor = sensorService.GetDefaultSensor(SensorType.Light);

            // Register this class a listener for light sensor
            sensorService.RegisterListener(null, lightSensor, Android.Hardware.SensorDelay.Game);

            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void OnSensorChanged(SensorEvent s)
        {
            // Your processing here
            s.Sensor = sensorService.GetDefaultSensor(SensorType.Light);
            lightSensorValue = s.Values[0];
            Toast.MakeText(this, lightSensorValue.ToString("0.00"), ToastLength.Long).Show();
        }

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
            //throw new NotImplementedException();
        }
    }
}

