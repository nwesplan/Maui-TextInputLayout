using Android.Content;
using Android.Text;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Nwesp.Maui.Android.Models.Enums;
using Nwesp.Maui.Android.Platforms.Android.Managers;
using Nwesp.Maui.Android.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nwesp.Maui.Android
{
    public class MaterialDatePickerHandler : DatePickerHandler
    {
        private BoxBackgroundMode _boxBackgroundMode;
        protected override MauiDatePicker CreatePlatformView()
        {
            var picker = ContextThemeHelper.BuildContextThemeWrapper(Context, _boxBackgroundMode, (t) => new MauiDatePicker(t));
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
