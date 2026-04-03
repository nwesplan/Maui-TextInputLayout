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

        // Fix for Borders clipping when the control is rearranged on screen (toggling visibility. adding/removing views)
        //private void ViewTreeObserver_GlobalLayout(object? sender, EventArgs e)
        //{
        //    if (VirtualView is null || PlatformView is null)
        //    {
        //        return;
        //    }

        //    var x = PlatformView.GetX();
        //    var y = PlatformView.GetY();
        //    if (x != PlatformView.PreviousX || y != PlatformView.PreviousY)
        //    {
        //        PlatformView.PreviousX = x;
        //        PlatformView.PreviousY = y;

        //        if (PlatformView.IsAttachedToWindow)
        //        {
        //            PlatformView.InvalidateMeasure(VirtualView);
        //        }
        //    }
        //}

        protected override void ConnectHandler(MauiTextInputLayout platformView)
        {
            base.ConnectHandler(platformView);

            if (PlatformEntry is not null)
            {
                PlatformEntry.TextChanged += PlatformEntry_TextChanged;
                PlatformEntry.FocusChange += PlatformEntry_FocusChange;
            }

            
            PlatformView.SetErrorIconOnClickListener(new OnClickListener(() =>
            {
                VirtualView?.ErrorIconClicked();
            }));
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

        public static void MapBackgroundColor(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.UpdateBackgroundColor(entry);
        }

        public static void MapOutlineColor(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.UpdateOutlineColor(entry);
        }

        public static void MapFocusedOutlineColor(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.UpdateOutlineColor(entry);
        }

        public static void MapDisabledOutlineColor(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.UpdateOutlineColor(entry);
        }

        public static void MapDisabledOutlineOpacity(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.UpdateOutlineColor(entry);
        }

        public static void MapBoxStrokeCornerRadius(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.UpdateBoxCornerRadius(entry);
        }

        public static void MapBoxStrokeWidth(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.UpdateBoxStrokeWidth(entry);
        }

        public static void MapBoxStrokeFocusedWidth(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.UpdateBoxStrokeFocusedWidth(entry);
        }

        public static void MapHint(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.MapHint(entry);
        }

        public static void MapHintColor(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.UpdateHintColors(entry);
        }

        public static void MapHintOpacity(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.UpdateHintColors(entry);
        }

        public static void MapIsHintAlwaysExpanded(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler?.PlatformView?.MapIsHintAlwaysExpanded(entry);
        }

        public static void MapBoxBackgroundMode(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.UpdateBoxBackgroundMode(entry);
        }

        public static void MapEndIcon(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.MapCustomEndIcon(entry, handler.MauiContext);
        }

        public static void MapEndIconColor(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.UpdateEndIconColor(entry);
        }

        public static void MapEndIconVisibilityMode(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.UpdateEndIconVisibilityMode(entry);
        }

        public static void MapEndIconMode(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.UpdateEndIconMode(entry);
        }
        
        public static void MapStartIcon(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.MapCustomStartIcon(entry, handler.MauiContext);
        }

        public static void MapStartIconColor(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.UpdateStartIconColor(entry);
        }

        public static void MapPrefix(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.UpdatePrefixText(entry);
        }

        public static void MapPrefixTextColor(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.UpdatePrefixTextColor(entry);
        }

        public static void MapSuffix(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.UpdateSuffixText(entry);
        }

        public static void MapSuffixTextColor(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.UpdateSuffixTextColor(entry);
        }

        public static void MapSupportingText(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.UpdateSupportingText(entry);
        }

        public static void MapErrorText(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.UpdateErrorText(entry);
        }

        public static void MapCounterEnabled(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.UpdateCounterEnabled(entry);
        }

        public static void MapCounterMaxLength(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.UpdateCounterMaxLength(entry);
        }

        public static void MapPadding(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.UpdatePadding(entry);
        }

        public static void MapIsErrorEnabled(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.UpdateIsErrorEnabled(entry);
        }

        public static void MapCursorColor(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.UpdateCursorColor(entry);
        }

        public static void MapErrorCursorColor(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.UpdateErrorCursorColor(entry);
        }

        public static void MapErrorOutlineColor(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.UpdateErrorOutlineColor(entry);
        }

        public static void MapCounterTextColor(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.UpdateCounterTextColor(entry);
        }

        public static void MapCounterOverflowTextColor(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.UpdateCounterOverflowTextColor(entry);
        }

        public static void MapSupportingTextColor(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.UpdateSupportingTextColor(entry, handler.PlatformView.HasFocus);
        }

        public static void MapIsEnabled(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            ViewHandler.MapIsEnabled(handler, entry);
            handler.PlatformView?.UpdateSupportingTextColor(entry, handler.PlatformView.HasFocus);
        }

        public static void MapStartIconClickedCommand(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.UpdateStartIconClickedCommand(entry);
        }

        public static void MapEndIconClickedCommand(ITextInputLayoutHandler handler, ITextInputLayout entry)
        {
            handler.PlatformView?.UpdateEndIconClickedCommand(entry);
        }
    }

    public class MaterialPickerHandler : PickerHandler
    {
        private BoxBackgroundMode _boxBackgroundMode;
        protected override MauiPicker CreatePlatformView()
        {
            var picker = ContextThemeHelper.BuildContextThemeWrapper(Context, _boxBackgroundMode, (t) => new MauiPicker(t));
            // Don't allow free form text
            picker.Focusable = false;
            picker.FocusableInTouchMode = false;
            picker.InputType = InputTypes.Null;
            picker.ShowSoftInputOnFocus = false;
            return picker;
        }

        public override void SetVirtualView(IView view)
        {
            _boxBackgroundMode = OutlineManager.ParseBoxBackgroundMode(view);
            base.SetVirtualView(view);
        }
    }
}
