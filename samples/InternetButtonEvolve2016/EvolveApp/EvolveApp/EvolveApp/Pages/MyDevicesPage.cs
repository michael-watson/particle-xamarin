using System;
using System.Reactive.Linq;
using System.Collections.Generic;

using EvolveApp.ViewModels;

using Akavache;
using Xamarin.Forms;

using Particle;
using EvolveApp.Views;
using Styles.XForms.Core;

namespace EvolveApp.Pages
{
	public class MyDevicesPage : ContentPage
	{
		ListView deviceListView;
		MyDevicesViewModel ViewModel;
		ActivityIndicator indicator;
		Label updatedTimeLabel;

		public bool RequestRefresh;
		public bool IsInitialized = false;

		public MyDevicesPage(List<ParticleDevice> devices = null)
		{
			ToolbarItem addDevice = new ToolbarItem { Icon = (FileImageSource)FileImageSource.FromFile("ic_add_black_24dp.png") };
			ToolbarItems.Add(addDevice);

			Title = "My Devices";

			deviceListView = new ListView()
			{
				ItemTemplate = new DataTemplate(typeof(DeviceCell)),
				IsPullToRefreshEnabled = true,
				Header = new DeviceListViewHeader(),
				BackgroundColor = Color.Transparent
			};

			updatedTimeLabel = new StyledLabel
			{
				CssStyle = "body",
				HorizontalOptions = LayoutOptions.End,
				Text = DateTime.Now.ToString()
			};

			Content = new StackLayout
			{
				Children = {
					//indicator,
					deviceListView,
					updatedTimeLabel
				},
				Padding = new Thickness(0, 10, 0, 0)
			};

			ViewModel = new MyDevicesViewModel();
			RequestRefresh = false;
			BindingContext = ViewModel;
			BackgroundColor = AppColors.BackgroundColor;

			deviceListView.SetBinding(ListView.ItemsSourceProperty, "Devices");
			deviceListView.SetBinding(ListView.RefreshCommandProperty, "RefreshCommand");
			deviceListView.SetBinding(ListView.IsRefreshingProperty, "Refreshing", BindingMode.TwoWay);
			updatedTimeLabel.SetBinding(Label.TextProperty, "LastRefresh");
			deviceListView.ItemSelected += ViewDeviceDetails;

		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			try
			{
				var accessToken = await BlobCache.UserAccount.GetObject<string>("Access Token");
				var refreshToken = await BlobCache.UserAccount.GetObject<string>("Refresh Token");
				var expiration = await BlobCache.UserAccount.GetObject<DateTime>("Expiration");
				ParticleCloud.AccessToken = new ParticleAccessToken(accessToken, refreshToken, expiration);

				await ViewModel.GetDevicesAsync();
			}
			catch (KeyNotFoundException e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);

				var page = new LoginPage();
				NavigationPage.SetHasNavigationBar(page, false);
				await Navigation.PushModalAsync(page);
			}
		}

		public async void ViewDeviceDetails(object sender, SelectedItemChangedEventArgs e)
		{
			ParticleDevice device = e.SelectedItem as ParticleDevice;
			if (device == null)
				return;

			await Navigation.PushAsync(new DeviceLandingPage((ParticleDevice)e.SelectedItem));
			deviceListView.SelectedItem = null;
		}
	}
}