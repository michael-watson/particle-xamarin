using Styles.Droid.Text;
using Styles.XForms.Core;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(StyledLabel), typeof(Styles.XForms.Droid.StyledLabelRenderer))]
namespace Styles.XForms.Droid
{
	public class StyledLabelRenderer : LabelRenderer
	{
		StyledLabel _styledElement;

		protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged(e);

			_styledElement = _styledElement ?? (Element as StyledLabel);

			var cssStyle = _styledElement.CssStyle;

			var textStyle = (!string.IsNullOrEmpty(_styledElement.TextStyleInstance) && TextStyle.Instances.ContainsKey(_styledElement.TextStyleInstance))
					? TextStyle.Instances[_styledElement.TextStyleInstance] : TextStyle.Main;

			if (Control != null)
			{
				textStyle.Style(Control, cssStyle, null, _styledElement.CustomTags);
			}
		}
	}
}