using System;
using EvolveApp;
using Foundation;
using Styles.iOS.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(StyledButton), typeof(EvolveApp.iOS.StyledButtonRenderer))]
namespace EvolveApp.iOS
{
	public class StyledButtonRenderer : ButtonRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				var _styledElement = e.NewElement as StyledButton;

				TextStyle.Main.Style<UILabel>(Control.TitleLabel, _styledElement.CssStyle);
			}
		}
	}
}