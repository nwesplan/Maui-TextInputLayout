using Nwesp.Maui.Android.Platforms.iOS;
using Microsoft.Maui.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nwesp.Maui.Android
{
    public partial class TextInputLayoutHandler : ViewHandler<TextInputLayout, MauiTextInputLayout>
    {
       

        protected override MauiTextInputLayout CreatePlatformView()
        {
            throw new NotImplementedException();
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
    }
}
