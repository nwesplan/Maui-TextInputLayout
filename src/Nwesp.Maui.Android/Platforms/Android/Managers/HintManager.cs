using Android.Content.Res;
using Android.Views;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AResource = Android.Resource.Attribute;
using AColor = Android.Graphics.Color;
using Nwesp.Maui.Android.Abstractions;


namespace Nwesp.Maui.Android.Platforms.Android.Managers
{
    public static class HintManager
    {
        private static int[][] _states = 
        [
            [-AResource.StateEnabled],                        // Disabled
            [AResource.StateEnabled, AResource.StateFocused], // Enabled & Focused
            [AResource.StateEnabled],                         // Normal
        ];
        
        public static void MapHint(this MauiTextInputLayout platformView, ITextInputLayout virtualView)
        {
            platformView.Hint = virtualView.Hint;
        }

        public static void UpdateHintColors(this MauiTextInputLayout platformView, ITextInputLayout virtualView)
        {
            platformView.DefaultHintTextColor = new ColorStateList(
                _states,
                [
                    virtualView.DisabledHintColor.WithAlpha(virtualView.DisabledHintOpacity).ToPlatform(),
                    virtualView.FocusedHintColor.ToPlatform(),
                    virtualView.HintColor.ToPlatform()
                ]
            );
        }

        public static void MapIsHintAlwaysExpanded(this MauiTextInputLayout platformView, ITextInputLayout virtualView)
        {
            platformView.ExpandedHintEnabled = !virtualView.IsHintAlwaysExpanded;
            return;
            // No longer needed?
            // Hack. Fix for Filled mode. When IsHintAlwaysExpanded is set to true and the edit text does not have text, focusing the entry adds height to the layout which causes other elements in the layout to shift.
            platformView.Post(async () =>
            {
                platformView.ExpandedHintEnabled = false;
                await Task.Yield();
                


            });
        }
    }
}
