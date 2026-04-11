using Microsoft.Maui.Graphics.Text;
using Nwesp.Maui.Android.Abstractions;
using Nwesp.Maui.Android.Models.Enums;
using Nwesp.Maui.Android.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nwesp.Maui.Android.Controls
{
    public class MaterialDatePicker : DatePicker, IMaterialEntry
    {
        public MaterialDatePicker() : base()
        {
            TextColor = ThemeHelper.GetInputTextColor();
        }
        public BoxBackgroundMode BoxBackgroundMode { get; set; }

        public static readonly BindableProperty DisabledTextColorProperty = BindableProperty.Create(nameof(DisabledTextColor), typeof(Color), typeof(MaterialDatePicker), defaultValue: ThemeHelper.GetDisabledInputTextColor());
        public Color DisabledTextColor
        {
            get => (Color)GetValue(DisabledTextColorProperty);
            set => SetValue(DisabledTextColorProperty, value);
        }
        public static readonly BindableProperty DisabledTextColorOpacityProperty = BindableProperty.Create(nameof(DisabledTextColorOpacity), typeof(float), typeof(MaterialDatePicker), defaultValue: ThemeHelper.GetDisabledInputTextOpacity());
        public float DisabledTextColorOpacity
        {
            get => (float)GetValue(DisabledTextColorOpacityProperty);
            set => SetValue(DisabledTextColorOpacityProperty, value);
        }
    }
}
