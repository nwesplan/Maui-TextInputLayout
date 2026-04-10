using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#if ANDROID
using PlatformView = Nwesp.Maui.Android.Platforms.Android.MauiTextInputLayout;
using PlatformEntry = Android.Widget.EditText;
#elif WINDOWS
using PlatformView = Nwesp.Maui.Android.Platforms.Windows.MauiTextInputLayout;
using PlatformEntry = Nwesp.Maui.Android.Platforms.Windows.MauiTextInputEditText;
#elif IOS || MACCATALYST
using PlatformView = Nwesp.Maui.Android.Platforms.iOS.MauiTextInputLayout;
using PlatformEntry = Nwesp.Maui.Android.Platforms.iOS.MauiTextInputLayout;
#endif
namespace Nwesp.Maui.Android.Abstractions
{
    public interface ITextInputLayoutHandler : IViewHandler
    {
        new ITextInputLayout VirtualView { get; }
        new PlatformView PlatformView { get; }
    }
}
