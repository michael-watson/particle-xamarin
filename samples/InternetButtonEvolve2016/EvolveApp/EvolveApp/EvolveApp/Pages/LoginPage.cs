using System;
using Xamarin.Forms;
using System.Collections.Generic;
using Particle;
using Particle.Models;
using EvolveApp.Pages;
using EvolveApp.ViewModels;
using Xamarin;
using Akavache;

namespace EvolveApp.Pages
{
	public class LoginPage : ReusableLoginPage
	{
		LoginViewModel ViewModel;

		public LoginPage()
		{
			ViewModel = new LoginViewModel();
			BindingContext = ViewModel;
			LogoFileImageSource = "particle.png";

			indicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");

			if (Device.OS == TargetPlatform.Windows)
				indicator.SetBinding(ActivityIndicator.IsVisibleProperty, "IsBusy");
		}

		public override async void Login(string userName, string passWord)
		{
			base.Login(userName, passWord);

			var response = await ViewModel.HandleLoginAsync(userName, passWord, App.Token);

			if (ParticleCloud.AccessToken != null && response)
			{
				BlobCache.UserAccount.InsertObject<string>("Access Token", ParticleCloud.AccessToken.Token);
				BlobCache.UserAccount.InsertObject<string>("Refresh Token", ParticleCloud.AccessToken.RefreshToken);
				BlobCache.UserAccount.InsertObject<DateTime>("Expiration", ParticleCloud.AccessToken.Expiration);

				await Navigation.PopModalAsync();
			}
			else
				await DisplayAlert("Login Error", "Invalid Login", "Try Again");
		}

		public override async void NewUserSignUp()
		{
			base.NewUserSignUp();

			await Navigation.PushAsync(new NewUserSignUpPage());
		}

		public override async void RunAfterAnimation()
		{
			base.RunAfterAnimation();

			if (ParticleCloud.AccessToken != null)
				await Navigation.PopModalAsync();
		}
#if __ANDROID__
		protected override bool OnBackButtonPressed()
		{
			return true;
		}
#endif
	}
}