using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace MyDevices.Droid
{
	[Activity(Label = "MyDevices.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            var density = Resources.DisplayMetrics.Density;

            App.ScreenWidth = (double)(Resources.DisplayMetrics.WidthPixels - 0.5f) / density;
            App.ScreenHeight = (double)(Resources.DisplayMetrics.HeightPixels - 0.5f) / density;

            Console.WriteLine($"Density: {density}");

            LoadApplication(new App());
		}
	}
}