using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Net;
using Android.Text;
using Android.Text.Style;
using Android.Util;
using Android.Widget;
using Microsoft.Maui;
using Microsoft.Maui.Platform;
using Nwesp.Maui.Android.Abstractions;
using Nwesp.Maui.Android.Models.Enums;
using Nwesp.Maui.Android.Platforms.Android.Managers;
using Nwesp.Maui.Android.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Views.View;
using static System.Net.Mime.MediaTypeNames;
using AColor = Android.Graphics.Color;
using AComplexUnitType = Android.Util.ComplexUnitType;
using AResource = Android.Resource.Attribute;
using ATextChangedEventArgs = Android.Text.TextChangedEventArgs;
using ATypeFaceStyle = Android.Graphics.TypefaceStyle;
using MColor = Microsoft.Maui.Graphics.Color;

namespace Nwesp.Maui.Android.Platforms.Android
{
    public static class MauiTextInputLayoutExtensions
    {
        public static void UpdatePrefixText(this MauiTextInputLayout platformView, ITextInputLayout virtualView)
        {
            if (string.Equals(platformView.PrefixText, virtualView.Prefix, StringComparison.CurrentCulture))
            {
                return;
            }
            //var spannable = new SpannableString(virtualView.Prefix);
            //spannable.SetSpan(
            //    new StyleSpan(ATypeFaceStyle.Bold),
            //    0,
            //    virtualView.Prefix.Length,
            //    SpanTypes.ExclusiveExclusive);
            //platformView.PrefixTextFormatted = spannable;
            
            //float textSize = (float)(virtualView.MaterialEntry?.FontSize ?? 14f);
            //platformView.PrefixTextView.SetTextSize(AComplexUnitType.Dip, 16f);

            // Default prefix/suffix text size is 16. This returns 44. (16 * 2.75(density))
            //var textSize1 = platformView.PrefixTextView.TextSize;

            // No longer needed?
            // Prefix gets misaligned from input text.
            // platformView.Post(() =>
            // {
                platformView.PrefixText = virtualView.Prefix;
                platformView.InvalidateMeasure(virtualView);
           // });
            
        }

        public static void UpdatePrefixTextColor(this MauiTextInputLayout platformView, ITextInputLayout virtualView)
        {
            int[][] states =
            [
                [AResource.StateEnabled],
                [-AResource.StateEnabled]
            ];

            int[] colors =
            [
                virtualView.PrefixTextColor.ToPlatform(),
                virtualView.DisabledPrefixTextColor.WithAlpha(virtualView.DisabledPrefixTextColorOpacity).ToPlatform()
            ];
            platformView.PrefixTextColor = new ColorStateList(states, colors);
        }

        public static void UpdateSuffixText(this MauiTextInputLayout platformView, ITextInputLayout virtualView)
        {
            if (string.Equals(platformView.SuffixText, virtualView.Suffix, StringComparison.CurrentCulture))
            {
                return;
            }
            //float textSize = (float)(virtualView.MaterialEntry?.FontSize ?? 14f);
            //platformView.SuffixTextView.SetTextSize(AComplexUnitType.Dip, textSize);
            //platformView.Post(() =>
            //{
                platformView.SuffixText = virtualView.Suffix;
                platformView.InvalidateMeasure(virtualView);
            //});
        }

        public static void UpdateSuffixTextColor(this MauiTextInputLayout platformView, ITextInputLayout virtualView)
        {
            int[][] states =
            [
                [AResource.StateEnabled],
                [-AResource.StateEnabled]
            ];

            int[] colors =
            [
                virtualView.SuffixTextColor.ToPlatform(),
                virtualView.DisabledSuffixTextColor.WithAlpha(virtualView.DisabledSuffixTextColorOpacity).ToPlatform()
            ];
            platformView.SuffixTextColor = new ColorStateList(states, colors);
        }


        public static void UpdateSupportingText(this MauiTextInputLayout platformView, ITextInputLayout virtualView)
        {
            platformView.HelperText = virtualView.SupportingText;
        }
        public static void UpdateErrorText(this MauiTextInputLayout platformView, ITextInputLayout virtualView)
        {
            platformView.Error = virtualView.ErrorText;
        }

        public static void UpdateCounterEnabled(this MauiTextInputLayout platformView, ITextInputLayout virtualView)
        {
            platformView.CounterEnabled = virtualView.CounterEnabled;
        }
        public static void UpdateCounterMaxLength(this MauiTextInputLayout platformView, ITextInputLayout virtualView)
        {
            platformView.CounterMaxLength = virtualView.CounterMaxLength;
        }
        public static void UpdatePadding(this MauiTextInputLayout platformView, ITextInputLayout virtualView)
        {
            var padding = virtualView.Padding;
            platformView.SetPadding((int)padding.Left, (int)padding.Top, (int)padding.Right, (int)padding.Bottom);
        }

        public static void UpdateIsErrorEnabled(this MauiTextInputLayout platformView, ITextInputLayout virtualView)
        {
            platformView.ErrorEnabled = virtualView.IsErrorEnabled;
        }

        public static void TextInputLayoutFocusChanged(this MauiTextInputLayout platformView, ITextInputLayout virtualView, bool hasFocus)
        {
            if (virtualView is null)
            {
                return;
            }

            platformView.UpdateSupportingTextColor(virtualView, hasFocus);

            if (virtualView.EndIconVisibilityMode != IconVisibilityMode.WhileEditing &&
                virtualView.EndIconVisibilityMode != IconVisibilityMode.HasTextWhileEditing)
            {
                return;
            }

            if (virtualView.EndIconVisibilityMode == IconVisibilityMode.HasTextWhileEditing)
            {
                if (platformView.HasTextAndFocus(hasFocus))
                {
                    platformView.ShowEndIcon();
                }
                else
                {
                    platformView.HideEndIcon();
                }
            }
            else
            {
                if (hasFocus)
                {
                    platformView.ShowEndIcon();
                }
                else
                {
                    platformView.HideEndIcon();
                }
            }
        }

        public static void TextInputLayoutTextChanged(this MauiTextInputLayout platformView, ITextInputLayout virtualView, string? text)
        {
            if (virtualView is null)
            {
                return;
            }

            if (virtualView.EndIconVisibilityMode != IconVisibilityMode.HasText &&
                virtualView.EndIconVisibilityMode != IconVisibilityMode.HasTextWhileEditing)
            {
                return;
            }

            if (virtualView.EndIconVisibilityMode == IconVisibilityMode.HasTextWhileEditing)
            {
                if (platformView.HasTextAndFocus(platformView.HasFocus))
                {
                    platformView.ShowEndIcon();
                }
                else
                {
                    platformView.HideEndIcon();
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(text))
                {
                    platformView.ShowEndIcon();
                }
                else
                {
                    platformView.HideEndIcon();
                }
            }
        }

        public static void UpdateCursorColor(this MauiTextInputLayout platformView, ITextInputLayout virtualView)
        {
            platformView.CursorColor = virtualView.CursorColor.ToDefaultColorStateList();
        }
        public static void UpdateErrorCursorColor(this MauiTextInputLayout platformView, ITextInputLayout virtualView)
        {
            platformView.CursorErrorColor = virtualView.ErrorCursorColor.ToDefaultColorStateList();
        }
        public static void UpdateSupportingTextColor(this MauiTextInputLayout platformView, ITextInputLayout virtualView, bool hasFocus)
        {
            platformView.SetHelperTextColor(ColorStateHelper.GetSupportingTextColorStateList(virtualView, hasFocus));
        }

        public static void UpdateCounterTextColor(this MauiTextInputLayout platformView, ITextInputLayout virtualView)
        {
            platformView.CounterTextColor = virtualView.CounterTextColor.ToDefaultColorStateList();
        }

        public static void UpdateCounterOverflowTextColor(this MauiTextInputLayout platformView, ITextInputLayout virtualView)
        {
            platformView.CounterOverflowTextColor = virtualView.CounterOverflowTextColor.ToDefaultColorStateList();
        }
    }
}
