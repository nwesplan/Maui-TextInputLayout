using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Text;
using Android.Text.Style;
using Android.Widget;
using AndroidX.AppCompat.Widget;
using AndroidX.Core.Graphics.Drawable;
using Google.Android.Material.TextField;
using Microsoft.Extensions.Logging;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Nwesp.Maui.Android.Abstractions;
using Nwesp.Maui.Android.Models.Enums;
using Nwesp.Maui.Android.Platforms.Android.Listeners;
using Nwesp.Maui.Android.Utilities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Google.Android.Material.TextField.TextInputLayout;
using static Nwesp.Maui.Android.Platforms.Android.MauiTextInputLayout;
using AResource = Android.Resource.Attribute;
using AView = Android.Views.View;
using MColor = Microsoft.Maui.Graphics.Color;

namespace Nwesp.Maui.Android.Platforms.Android.Managers
{
    public static class IconManager
    {
        private static ConcurrentDictionary<string, Drawable> DrawableDictionary = new ();

        public static async void UpdateEndIconMode(this MauiTextInputLayout platformView, ITextInputLayout virtualView)
        {
            platformView.CustomEndIconMode = virtualView.EndIconMode;

            if (platformView.CustomEndIconMode == EndIconMode.Password && virtualView.Content is IEntry entry)
            {
                if(entry.IsPassword)
                {
                    platformView.SetToggleOffPasswordIcon();
                }
                else
                {
                    platformView.SetToggleOnPasswordIcon();
                }
            }
            else if(platformView.CustomEndIconMode == EndIconMode.ClearText)
            {
                await platformView.UpdateEndIcon(virtualView);
            }
        }

        public static void UpdateEndIconVisibilityMode(this MauiTextInputLayout platformView, ITextInputLayout virtualView)
        {
            platformView.TextInputLayoutFocusChanged(virtualView, platformView.HasFocus);
            platformView.TextInputLayoutTextChanged(virtualView, platformView.EditText?.Text);

            if (virtualView.EndIconVisibilityMode == IconVisibilityMode.Always)
            {
                platformView.ShowEndIcon();
            }
            else if (virtualView.EndIconVisibilityMode == IconVisibilityMode.Never)
            {
                platformView.HideEndIcon();
            }
        }

        public static void UpdateStartIconClickedCommand(this MauiTextInputLayout platformView, ITextInputLayout virtualView)
        {
            if (virtualView.StartIconClickedCommand is not null)
            {
                platformView.SetStartIconOnClickListener(new OnClickListener(() =>
                {
                    virtualView.StartIconClickedCommand.Execute(null);
                }));
            }
            else
            {
                platformView.SetStartIconOnClickListener(null);
            }
        }

        public static void UpdateEndIconClickedCommand(this MauiTextInputLayout platformView, ITextInputLayout virtualView)
        {
            if (virtualView.EndIconClickedCommand is not null)
            {
                platformView.SetEndIconOnClickListener(new OnClickListener(() =>
                {
                    virtualView.EndIconClickedCommand.Execute(null);
                }));
            }
            else
            {
                platformView.SetEndIconOnClickListener(null);
            }
        }

        public static void UpdateErrorIconClickedCommand(this MauiTextInputLayout platformView, ITextInputLayout virtualView)
        {
            if (virtualView.ErrorIconClickedCommand is not null)
            {
                platformView.SetErrorIconOnClickListener(new OnClickListener(() =>
                {
                    virtualView.ErrorIconClickedCommand.Execute(null);
                }));
            }
            else
            {
                platformView.SetErrorIconOnClickListener(null);
            }
        }

        public static void DefaultEndIconClickedCommand(this MauiTextInputLayout platformView)
        {
            if (platformView.CustomEndIconMode == EndIconMode.Password)
            {
                var cursorPosition = platformView.EditText?.GetCursorPosition() ?? 0;
                if (platformView.IsPassword)
                {
                    platformView.SetToggleOnPasswordIcon();
                    platformView.EditText?.TogglePasswordOff();
                }
                else
                {
                    platformView.SetToggleOffPasswordIcon();
                    platformView.EditText?.TogglePasswordOn();
                }
                platformView.EditText?.SetSelection(cursorPosition, cursorPosition);
            }
            else if (platformView.CustomEndIconMode == EndIconMode.ClearText)
            {
                platformView.EditText?.Text = string.Empty;
            }
        }

        public static void UpdateShowPasswordIcon(this MauiTextInputLayout platformView, ITextInputLayout virtualView)
        {
            platformView.ShowPasswordIcon = virtualView.ShowPasswordIcon;
            platformView.UpdateEndIconMode(virtualView);
        }

        public static void UpdateHidePasswordIcon(this MauiTextInputLayout platformView, ITextInputLayout virtualView)
        {
            platformView.HidePasswordIcon = virtualView.HidePasswordIcon;
            platformView.UpdateEndIconMode(virtualView);
        }

        public static async void SetToggleOffPasswordIcon(this MauiTextInputLayout platformView)
        {
            platformView.IsPassword = true;
            platformView.EndIconDrawable = await MapCustomIcon(platformView.ShowPasswordIcon, platformView.MauiContext);
        }

        public static async void SetToggleOnPasswordIcon(this MauiTextInputLayout platformView)
        {
            platformView.IsPassword = false;
            platformView.EndIconDrawable = await MapCustomIcon(platformView.HidePasswordIcon, platformView.MauiContext);
        }

        public static async void ShowEndIcon(this MauiTextInputLayout platformView)
        {
            await platformView.EndIconInitializedCompletionSource.Task;
            // Removing the Post causes the layout to increase in height, shifting elements on the screen when the icon becomes visible (i.e. When focused)
 
            platformView.EndIconVisible = true;
        }

        public static async void HideEndIcon(this MauiTextInputLayout platformView)
        {
            await platformView.EndIconInitializedCompletionSource.Task;
            platformView.EndIconVisible = false;
        }

        public static void UpdateEndIconColor(this MauiTextInputLayout platformView, ITextInputLayout virtualView)
        {
            if (virtualView.EndIconColor is null)
            {
                platformView.SetEndIconTintList(null);
                return;
            }

            platformView.SetEndIconTintList(GetIconColorStateList(virtualView.EndIconColor, virtualView.DisabledEndIconColor, virtualView.DisabledEndIconOpacity));
        }

        private static ColorStateList GetIconColorStateList(MColor iconColor, MColor disabledColor, float disabledColorOpacity)
        {
            int[][] states =
            [
                [-AResource.StateEnabled],
                [AResource.StateEnabled],
            ];
            int[] colors =
            [
                disabledColor.WithAlpha(disabledColorOpacity).ToAndroid(),
                iconColor.ToAndroid()
            ];
            return new ColorStateList(states, colors);
        }

        public static void UpdateStartIconColor(this MauiTextInputLayout platformView, ITextInputLayout virtualView)
        {
            if (virtualView.StartIconColor is null)
            {
                platformView.SetStartIconTintList(null);
                return;
            }

            platformView.SetStartIconTintList(GetIconColorStateList(virtualView.StartIconColor, virtualView.DisabledStartIconColor, virtualView.DisabledStartIconOpacity));
        }

        public static async Task UpdateEndIcon(this MauiTextInputLayout platformView, ITextInputLayout virtualView)
        {
            platformView.EndIconDrawable = await MapCustomIcon(virtualView.EndIcon, platformView.MauiContext);
        }

        public static async Task UpdateStartIcon(this MauiTextInputLayout platformView, ITextInputLayout virtualView)
        {
            platformView.StartIconDrawable = await MapCustomIcon(virtualView.StartIcon, platformView.MauiContext);

            var startIcon = platformView.FindViewById<AppCompatImageButton>(Resource.Id.text_input_start_icon);
            if(startIcon is not null)
            {
                startIcon.Focusable = false;
                startIcon.FocusableInTouchMode = false;
            }
        }

        public static async Task UpdateErrorIcon(this MauiTextInputLayout platformView, ITextInputLayout virtualView)
        {
            platformView.ErrorIconDrawable = await MapCustomIcon(virtualView.ErrorIcon, platformView.MauiContext);
        }

        private static async Task<Drawable?> MapCustomIcon(ImageSource? icon, IMauiContext? mauiContext)
        {
            if (icon is null || mauiContext is null)
            {
                return null;
            }
            
            Drawable? drawable = null;
            try
            {
                if (icon is FileImageSource fileImageSource)
                {
                    if (DrawableDictionary.TryGetValue(fileImageSource.File, out Drawable? image))
                    {
                        drawable = image;
                    }
                    else
                    {
                        drawable = await CreateScaledDrawable(icon, mauiContext);
                        if (drawable is not null)
                        {
                            DrawableDictionary.TryAdd(fileImageSource.File, drawable);
                        }
                    }
                }
                else
                {
                    drawable = await CreateScaledDrawable(icon, mauiContext);
                }
            }
            catch(Exception ex) 
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return drawable;
        }

        private static async Task<Drawable?> CreateScaledDrawable(ImageSource icon, IMauiContext mauiContext)
        {
            var drawable = (await icon.GetPlatformImageAsync(mauiContext))?.Value;
            if (drawable is BitmapDrawable bitmapDrawable && bitmapDrawable.Bitmap is not null)
            {
                var density = DisplayHelper.GetDensity(mauiContext?.Context);
                int sizePx = (int)(DisplayHelper.IconSize * density);

                var scaled = Bitmap.CreateScaledBitmap(
                    bitmapDrawable.Bitmap,
                    sizePx,
                    sizePx,
                    true);

                drawable = new BitmapDrawable(mauiContext?.Context?.Resources, scaled);
            }

            return drawable;
        }
    }
}
