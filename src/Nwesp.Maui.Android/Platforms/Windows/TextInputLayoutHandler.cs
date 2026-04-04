using Nwesp.Maui.Android.Platforms.Windows;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nwesp.Maui.Android
{
    public partial class TextInputLayoutHandler : ViewHandler<ITextInputLayout, MauiTextInputLayout>
    {
        protected override MauiTextInputLayout CreatePlatformView()
        {
            throw new NotImplementedException();
        }
        public static void MapBackgroundColor(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            //handler.PlatformView?.();
        }

        public static void MapBorderColor(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {

        }
        public static void MapFocusedBorderColor(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {

        }
        public static void MapHint(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {

        }
        public static void MapHintColor(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {

        }
        public static void MapFocusedHintColor(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {

        }
        public static void MapIsHintAlwaysExpanded(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {

        }
        public static void MapEndIcon(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
        }
        public static void MapBoxBackgroundMode(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
        }
        public static void MapEndIconVisibilityMode(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
        }
        public static void MapEndIconColor(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
        }
    }
}
