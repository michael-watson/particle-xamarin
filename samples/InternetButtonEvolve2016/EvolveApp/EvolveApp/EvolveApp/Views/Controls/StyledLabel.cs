using System.Collections.Generic;
using Xamarin.Forms;
using Styles.Core.Text;

namespace Styles.XForms.Core
{
	public class StyledLabel : Label, IStyledView
	{
		public string CssStyle { get; set; }
		public string TextStyleInstance { get; set; }
		public List<CssTagStyle> CustomTags { get; set; }
	}
}