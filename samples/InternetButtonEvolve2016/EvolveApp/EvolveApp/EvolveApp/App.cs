using System;
using System.Reflection;
using System.IO;

using Xamarin.Forms;
using EvolveApp.Pages;
using Styles.Core.Text;

namespace EvolveApp
{
	public class App : Application
	{
		public static string Token = "7770f37e1e767f9eb4a72ca81a933b4026957e02";

		public App()
		{
			var navPage = new NavigationPage(new MyDevicesPage());
			var style = DependencyService.Get<ITextStyle>();

			var assembly = typeof(App).GetTypeInfo().Assembly;
			Stream stream = assembly.GetManifestResourceStream("EvolveApp.Style.css");
			string text = string.Empty;
			using (var reader = new StreamReader(stream))
			{
				text = reader.ReadToEnd();
				style.SetCSS(text);
			}

			MainPage = navPage;

			if (Device.OS == TargetPlatform.iOS)
			{
				navPage.BarBackgroundColor = AppColors.Blue;
				navPage.BarTextColor = Color.White;
			}
		}

		protected override void OnStart()
		{
			// Handle when your app starts
			//ParticleCloud.AccessToken = new ParticleAccessToken(Token, RefreshToken, Expiration);
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
			//ParticleCloud.AccessToken = new ParticleAccessToken(Token, RefreshToken, Expiration);
		}
	}
}
