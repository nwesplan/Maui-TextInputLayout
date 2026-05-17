using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Nwesp.Maui.Android.Utilities;

namespace Nwesp.Maui.Android.Samples;

public partial class StandardFormPage : BaseDemoPage
{
	public StandardFormPage()
	{
		InitializeComponent();
		BindingContext = this;
        ImageHandler.Mapper.AppendToMapping("Test", (handler, view) =>
        {
            handler.PlatformView.ImageTintList = ThemeHelper.GetLeadingIconColor().ToDefaultColorStateList();
        });
    }
}