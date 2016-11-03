using System;
using System.Collections.Generic;
using Styles.Core.Text;

namespace Styles.XForms.Core
{
	public interface IStyledView
	{
		string CssStyle { get; set; }
		string TextStyleInstance { get; set; }
		List<CssTagStyle> CustomTags { get; set; }
	}

	public interface IHtmlRenderer
	{
		bool EnableHtmlEditing { get; set; }
	}
}

