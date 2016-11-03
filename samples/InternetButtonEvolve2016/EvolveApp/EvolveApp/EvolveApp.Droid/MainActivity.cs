using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using Plugin.Toasts;
using Xamarin.Forms.Platform.Android;
using Styles.Droid.Text;
using Styles.Core.Text;

namespace EvolveApp.Droid
{
	[Activity(Label = "EvolveApp",
			  Icon = "@drawable/icon",
			  ScreenOrientation = ScreenOrientation.Portrait,
			  ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
			  Theme = "@style/MyTheme",
			  MainLauncher = true
			 )]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);
			DependencyService.Register<ToastNotificatorImplementation>(); // Register your dependency
            DependencyService.Register<ITextStyle, TextStyle>();

            var style = (Styles.Droid.Text.TextStyle)Xamarin.Forms.DependencyService.Get<ITextStyle> ();
			style.AddFont ("SegoeUI-SemiBold", "SegoeUIBold.ttf");
            style.AddFont ("SegoeUI-Light", "SegoeUILight.ttf");
			style.AddFont ("SegoeUI", "SegoeUIRegular.ttf");

            ToastNotificatorImplementation.Init(this);

			LoadApplication(new App());

            //Akavache.BlobCache.ApplicationName = "EvolveApp";
        }
	}
}