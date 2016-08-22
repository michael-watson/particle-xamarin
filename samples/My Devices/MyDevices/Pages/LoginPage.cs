using System;
using System.Reactive.Linq;

using Xamarin.Forms;
using System.Collections.Generic;
using Akavache;
using Particle;
using Particle.Models;
using MyDevices.ViewModels;

using MyLoginUI.Pages;

namespace MyDevices.Pages
{
	public class LoginPage : ReusableLoginPage
	{
		bool isInitialized = false;

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (!isInitialized)
			{
				Navigation.InsertPageBefore(new MyDevicesPage(), this);
				isInitialized = true;
			}
		}

		public override async void Login(string userName, string passWord, bool saveUserName)
		{
			base.Login(userName, passWord, saveUserName);

			await ParticleCloud.SharedInstance.CreateOAuthClientAsync(App.Token, "xamarin");
			var response = await ParticleCloud.SharedInstance.LoginWithUserAsync(userName, passWord);

			if (ParticleCloud.AccessToken != null && response)
			{
				await BlobCache.UserAccount.InsertObject("Access Token", ParticleCloud.AccessToken.Token);
				await BlobCache.UserAccount.InsertObject("Refresh Token", ParticleCloud.AccessToken.RefreshToken);
				await BlobCache.UserAccount.InsertObject("Expiration", ParticleCloud.AccessToken.Expiration);
				await BlobCache.UserAccount.InsertObject("Username", userName);
				App.HasValidToken = true;

				await Navigation.PopAsync();
			}
			else
				await DisplayAlert("Login Error", "Invalid Login", "Try Again");
		}

		public override async void RunAfterAnimation()
		{
			base.RunAfterAnimation();

			try
			{
				var username = await BlobCache.UserAccount.GetObject<string>("Username");
				SetUsernameEntry(username);

				var accessToken = await BlobCache.UserAccount.GetObject<string>("Access Token");
				var refreshToken = await BlobCache.UserAccount.GetObject<string>("Refresh Token");
				var expiration = await BlobCache.UserAccount.GetObject<DateTime>("Expiration");

				//if (expiration > DateTime.Now && accessToken != null)
				//{
				//	ParticleCloud.AccessToken = new ParticleAccessToken(accessToken, refreshToken, expiration);
				//	await Navigation.PopAsync(false);
				//}
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
		}
	}
}