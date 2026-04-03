using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.Widget;
using Google.Android.Material.Internal;
using Google.Android.Material.TextField;
using Java.Lang;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Nwesp.Maui.Android.Abstractions;
using Nwesp.Maui.Android.Controls;
using Nwesp.Maui.Android.Models.Enums;
using Nwesp.Maui.Android.Platforms.Android.Listeners;
using Nwesp.Maui.Android.Platforms.Android.Managers;
using Nwesp.Maui.Android.Utilities;
using AResource = Android.Resource.Attribute;
using ASound = Android.Views.SoundEffects;
using AView = Android.Views.View;
using LLayout = Android.Widget.LinearLayout;
namespace Nwesp.Maui.Android.Platforms.Android
{
    public class MauiTextInputLayout : Google.Android.Material.TextField.TextInputLayout
    {
        public EndIconMode CustomEndIconMode { get; set; }
        public bool IsPassword { get; set; }
        public ImageSource? ShowPasswordIcon { get; set; }
        public ImageSource? HidePasswordIcon { get; set; }
        public bool HasTextAndFocus(bool hasFocus)
        {
            return hasFocus && !string.IsNullOrWhiteSpace(EditText?.Text);
        }
        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            if(!changed)
            {
                return;
            }

            // Set the EditText's width when the layout changes.
            EditText?.SetMinimumWidth(this.Width);
        }
        public IMauiContext MauiContext { get; }
        public MauiTextInputLayout(IMauiContext? mauiContext) : base(mauiContext!.Context!)
        {
            MauiContext = mauiContext;
            var density = DisplayHelper.GetDensity(Context);
            
            // Hack. For some reason when the box background mode is set to filled, the hint is positioned too high when focused and/or has text
            BoxCollapsedPaddingTop = (int)(8 * density);

            var endIcon = this.FindViewById<AppCompatImageButton>(Resource.Id.text_input_end_icon);
            if (endIcon is null)
            {
                return;
            }

            //EndIconMode = EndIconCustom;
            //EndIconVisible = true;
            //return;

            // EndIconBug
            // Leaving the following code as a reference. It seems the only issue now is that the End icon appears then disappears immediatley upon load of the main page only.

            // Hack. The following are workarounds for various issues with the end icon.
            // 1. Icon initially visible before focus state is applied
            // 2. Border shifting when navigating forms if the icon starts hidden
            // 3. End icon occasionally receiving focus during keyboard navigation

            // Make icon invisible with alpha before we toggle visibility (1)
            endIcon.Alpha = 0f;

            // Use custom mode to prevent default end icon behavior (2)
            EndIconMode = EndIconCustom;

            // Toggle visibility to trigger layout changes (1)
            EndIconVisible = true;

            // Queue on UI thread (2)
            Post(() =>
            {
                // Prevent the end icon from receiving focus (3)
                endIcon.Focusable = false;
                endIcon.FocusableInTouchMode = false;

                // Hide the icon after the layout is fixed (2)
                EndIconVisible = false;

                // Make icon visible again with alpha (1)
                endIcon.Alpha = 1f;
            });
        }
    }
}
