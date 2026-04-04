using Android.Content.PM;
using Android.Content.Res;
using Android.Views;
using AndroidX.AppCompat.View;
using Nwesp.Maui.Android.Platforms.Android;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Icu.Text.CaseMap;
using static Android.Provider.MediaStore;
using AColor = Android.Graphics.Color;
using ContextThemeWrapper = AndroidX.AppCompat.View.ContextThemeWrapper;
using AResource = Android.Resource.Attribute;
using AView = Android.Views.View;
using Nwesp.Maui.Android.Platforms.Android.Managers;
using Javax.Crypto;
using Android.Graphics.Drawables;
using Android.Graphics;
using Nwesp.Maui.Android.Models.Enums;
using Android.Content;
using Android.Widget;
using Google.Android.Material.TextField;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using AndroidX.ConstraintLayout.Core.Widgets;
using Android.Util;
using AndroidX.AppCompat.Widget;
using static Google.Android.Material.TextField.TextInputLayout;
using Color = Microsoft.Maui.Graphics.Color;
using Nwesp.Maui.Android.Models.Exceptions;
using Java.Lang;
using Microsoft.Maui.Controls.Platform;
using JMode = Android.Text.JustificationMode;
using Android.Text;
using ATextAlignment = Android.Views.TextAlignment;
using System.Resources;
using ATheme = Android.Content.Res;
using ARect = Android.Graphics.Rect;
using AndroidX.Core.Widget;
using AndroidX.Core.Graphics.Drawable;
using Nwesp.Maui.Android.Utilities;
using static Android.Views.View;
using static Android.Views.ViewTreeObserver;
using Layout = Microsoft.Maui.Controls.Layout;
using Android.Views.InputMethods;
using AViewGroup = Android.Views.ViewGroup;
using static Nwesp.Maui.Android.Platforms.Android.MauiTextInputLayout;
using AViewStates = Android.Views.ViewStates;
using Microsoft.Maui.Graphics;
using Google.Android.Material.Internal;
using Android.Graphics.Drawables.Shapes;
using AShapeDrawable = Android.Graphics.Drawables.ShapeDrawable;
using Android.Animation;
using ABlendMode = Android.Graphics.BlendMode;
using AProgressBar = Android.Widget.ProgressBar;
using Nwesp.Maui.Android.Abstractions;
using Nwesp.Maui.Android.Controls;
using Nwesp.Maui.Android.Platforms.Android.Listeners;
using ATextChangedEventArgs = Android.Text.TextChangedEventArgs;

namespace Nwesp.Maui.Android
{

    public partial class TextInputLayoutHandler : ViewHandler<ITextInputLayout, MauiTextInputLayout>
    {
        protected override MauiTextInputLayout CreatePlatformView()
        {
            var textInputLayout =  new MauiTextInputLayout(MauiContext);
            return textInputLayout;
        }

        public override void SetVirtualView(IView view)
        {
            if(view is not ContentView contentView)
            {
                return;
            }

            if(view is ITextInputLayout layout)
            {
                // Setting some default values in the Maui component is not working properly.
                layout.DisabledHintOpacity = ThemeHelper.GetDisabledLabelTextOpacity();
                if (layout.Content is IMaterialEntry materialEntry)
                {
                    materialEntry.BoxBackgroundMode = layout.BoxBackgroundMode;
                    VirtualEntry = materialEntry;
                }
            }
            
            PlatformEntry = contentView.Content.ToPlatform(MauiContext!) as EditText ?? throw IllegalContentException.ThrowTextInputLayoutIllegalContent();
            base.SetVirtualView(view);
            PlatformView.AddView(PlatformEntry);
            PlatformView.TextInputLayoutTextChanged(VirtualView, PlatformEntry?.Text);
        }

        protected override void ConnectHandler(MauiTextInputLayout platformView)
        {
            base.ConnectHandler(platformView);

            if (PlatformEntry is not null)
            {
                PlatformEntry.TextChanged += PlatformEntry_TextChanged;
                PlatformEntry.FocusChange += PlatformEntry_FocusChange;
            }
        }
        
        protected override void DisconnectHandler(MauiTextInputLayout platformView)
        {
            base.DisconnectHandler(platformView);
            if (PlatformEntry is not null)
            {
                PlatformEntry.TextChanged -= PlatformEntry_TextChanged;
                PlatformEntry.FocusChange -= PlatformEntry_FocusChange;
            }
            platformView.SetEndIconOnClickListener(null);
            platformView.SetStartIconOnClickListener(null);
            platformView.SetErrorIconOnClickListener(null);
        }

        private void PlatformEntry_FocusChange(object? sender, AView.FocusChangeEventArgs e)
        {
            PlatformView?.TextInputLayoutFocusChanged(VirtualView, e.HasFocus);
        }
        
        private void PlatformEntry_TextChanged(object? sender, ATextChangedEventArgs e)
        {
            PlatformView?.TextInputLayoutTextChanged(VirtualView, e.Text?.ToString());
        }

        public static void MapBackgroundColor(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateBackgroundColor(view);
        }

        public static void MapOutlineColor(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateOutlineColor(view);
        }

        public static void MapFocusedOutlineColor(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateOutlineColor(view);
        }

        public static void MapDisabledOutlineColor(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateOutlineColor(view);
        }

        public static void MapDisabledOutlineOpacity(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateOutlineColor(view);
        }

        public static void MapBoxStrokeCornerRadius(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateBoxCornerRadius(view);
        }

        public static void MapBoxStrokeWidth(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateBoxStrokeWidth(view);
        }

        public static void MapBoxStrokeFocusedWidth(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateBoxStrokeFocusedWidth(view);
        }

        public static void MapHint(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.MapHint(view);
        }

        public static void MapHintColor(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateHintColors(view);
        }

        public static void MapHintOpacity(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateHintColors(view);
        }

        public static void MapIsHintAlwaysExpanded(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler?.PlatformView?.MapIsHintAlwaysExpanded(view);
        }

        public static void MapBoxBackgroundMode(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateBoxBackgroundMode(view);
        }

        public static void MapEndIcon(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateEndIcon(view);
        }

        public static void MapErrorIcon(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateErrorIcon(view);
        }

        public static void MapErrorTextColor(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateErrorTextColor(view);
        }

        public static void MapEndIconColor(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateEndIconColor(view);
        }

        public static void MapEndIconVisibilityMode(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateEndIconVisibilityMode(view);
        }

        public static void MapEndIconMode(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateEndIconMode(view);
        }
        
        public static void MapStartIcon(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateStartIcon(view);
        }

        public static void MapStartIconColor(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateStartIconColor(view);
        }

        public static void MapPrefix(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdatePrefixText(view);
        }

        public static void MapPrefixTextColor(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdatePrefixTextColor(view);
        }

        public static void MapSuffix(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateSuffixText(view);
        }

        public static void MapSuffixTextColor(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateSuffixTextColor(view);
        }

        public static void MapSupportingText(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateSupportingText(view);
        }

        public static void MapErrorText(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateErrorText(view);
        }

        public static void MapCounterEnabled(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateCounterEnabled(view);
        }

        public static void MapCounterMaxLength(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateCounterMaxLength(view);
        }

        public static void MapPadding(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdatePadding(view);
        }

        public static void MapIsErrorEnabled(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateIsErrorEnabled(view);
        }

        public static void MapCursorColor(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateCursorColor(view);
        }

        public static void MapErrorCursorColor(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateErrorCursorColor(view);
        }

        public static void MapErrorOutlineColor(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateErrorOutlineColor(view);
        }

        public static void MapCounterTextColor(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateCounterTextColor(view);
        }

        public static void MapCounterOverflowTextColor(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateCounterOverflowTextColor(view);
        }

        public static void MapSupportingTextColor(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateSupportingTextColor(view);
        }

        public static void MapIsEnabled(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            ViewHandler.MapIsEnabled(handler, view);
            handler.PlatformView?.UpdateStatefulColors(view, handler.PlatformView.HasFocus);
        }

        public static void MapStartIconClickedCommand(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateStartIconClickedCommand(view);
        }

        public static void MapEndIconClickedCommand(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateEndIconClickedCommand(view);
        }
        public static void MapErrorIconClickedCommand(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateErrorIconClickedCommand(view);
        }
        
        public static void MapShowPasswordIcon(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateShowPasswordIcon(view);
        }

        public static void MapHidePasswordIcon(ITextInputLayoutHandler handler, ITextInputLayout view)
        {
            handler.PlatformView?.UpdateHidePasswordIcon(view);
        }
    }
}
