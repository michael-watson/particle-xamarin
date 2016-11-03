using System;
using Styles.XForms.Core;
using Xamarin.Forms;

namespace EvolveApp.Views
{
	public class DeviceCell : ViewCell
	{
		Label deviceName, lastHeard;

		public DeviceCell()
		{
			deviceName = new StyledLabel { CssStyle = "h3", VerticalOptions = LayoutOptions.Center };
			lastHeard = new StyledLabel { CssStyle = "body", VerticalOptions = LayoutOptions.Center };

			Grid layout = new Grid
			{
				ColumnDefinitions = new ColumnDefinitionCollection {
					new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) }
				},
				Padding = new Thickness(20, 0, 10, 0)
			};

			layout.Children.Add(deviceName, 0, 0);
			layout.Children.Add(lastHeard, 1, 0);

			deviceName.SetBinding(Label.TextProperty, "Name");
			lastHeard.SetBinding(Label.TextProperty, "LastHeard", BindingMode.Default, null, "{0:MM/dd H:mm}");

			View = layout;
		}
	}
}