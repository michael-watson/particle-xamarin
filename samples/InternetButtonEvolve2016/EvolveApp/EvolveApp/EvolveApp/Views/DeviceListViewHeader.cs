using System;

using Xamarin.Forms;
using Styles.XForms.Core;

namespace EvolveApp.Views
{
	public class DeviceListViewHeader : Grid
	{
		public DeviceListViewHeader()
		{
			var deviceName = new StyledLabel
			{
				CssStyle = "h2left",
				Text = "Device Name"
			};
			var lastHeard = new StyledLabel
			{
				CssStyle = "h2",
				Text = "Last Heard",
				HorizontalOptions = LayoutOptions.End,
			};

			ColumnDefinitions = new ColumnDefinitionCollection {
				new ColumnDefinition { Width = GridLength.Star },
				new ColumnDefinition { Width = GridLength.Auto }
			};

			Children.Add(deviceName, 0, 0);
			Children.Add(lastHeard, 1, 0);
			Padding = new Thickness(10, 0, 10, 0);
		}
	}
}