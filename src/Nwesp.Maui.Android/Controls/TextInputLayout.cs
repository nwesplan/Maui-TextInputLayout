using Nwesp.Maui.Android.Models.Enums;
using Nwesp.Maui.Android.Models.Events;
using Microsoft.Maui;
using Microsoft.Maui.Layouts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nwesp.Maui.Android.Utilities;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls.Shapes;
using System.Windows.Input;
using Nwesp.Maui.Android.Abstractions;
using Nwesp.Maui.Android.Models;
using Nwesp.Maui.Android.Platforms.Android.Listeners;
using Nwesp.Maui.Android.Platforms.Android;
using Nwesp.Maui.Android.Platforms.Android.Managers;
using Android.Widget;






#if ANDROID
using Microsoft.Maui.Platform;
#endif
namespace Nwesp.Maui.Android.Controls
{
    public class TextInputLayout : ContentView, ITextInputLayout
    {
        public IMaterialEntry? MaterialEntry => Content as IMaterialEntry;
        static TextInputLayout()
        {
            OutlineColorProperty = BindableProperty.Create(nameof(OutlineColor), typeof(Color), typeof(TextInputLayout), defaultValue: ThemeHelper.GetOutlineColor());
            FocusedOutlineColorProperty = BindableProperty.Create(nameof(FocusedOutlineColor), typeof(Color), typeof(TextInputLayout), defaultValue: ThemeHelper.GetFocusedOutlineColor());
            DisabledOutlineColorProperty = BindableProperty.Create(nameof(DisabledOutlineColor), typeof(Color), typeof(TextInputLayout), defaultValue: ThemeHelper.GetDisabledOutlineColor());
            DisabledOutlineOpacityProperty = BindableProperty.Create(nameof(DisabledOutlineOpacity), typeof(float), typeof(TextInputLayout), defaultValue: ThemeHelper.GetDisabledOutlineOpacity());

            HintProperty = BindableProperty.Create(nameof(Hint), typeof(string), typeof(TextInputLayout));
            DefaultHintColorProperty = BindableProperty.Create(nameof(DefaultHintColor), typeof(Color), typeof(TextInputLayout), defaultValue: ThemeHelper.GetLabelTextColor());
            FocusedHintColorProperty = BindableProperty.Create(nameof(FocusedHintColor), typeof(Color), typeof(TextInputLayout), defaultValue: ThemeHelper.GetFocusedLabelTextColor());
            DisabledHintColorProperty = BindableProperty.Create(nameof(DisabledHintColor), typeof(Color), typeof(TextInputLayout), defaultValue: ThemeHelper.GetDisabledLabelTextColor());
            DisabledHintOpacityProperty = BindableProperty.Create(nameof(DisabledHintOpacity), typeof(float), typeof(TextInputLayout), defaultValue: ThemeHelper.GetDisabledLabelTextOpacity());
            IsHintAlwaysExpandedProperty = BindableProperty.Create(nameof(IsHintAlwaysExpanded), typeof(bool), typeof(TextInputLayout));

            BoxBackgroundModeProperty = BindableProperty.Create(nameof(BoxBackgroundMode), typeof(BoxBackgroundMode), typeof(TextInputLayout), defaultBindingMode: BindingMode.OneTime, propertyChanged: BoxBackgroundModePropertyChanged);
            EndIconVisibilityModeProperty = BindableProperty.Create(nameof(EndIconVisibilityMode), typeof(IconVisibilityMode), typeof(TextInputLayout), defaultValue: IconVisibilityMode.HasTextWhileEditing);
            EndIconModeProperty = BindableProperty.Create(nameof(EndIconMode), typeof(EndIconMode), typeof(TextInputLayout), defaultValue: EndIconMode.ClearText);

            EndIconProperty = BindableProperty.Create(nameof(EndIcon), typeof(ImageSource), typeof(TextInputLayout), defaultValue: ImageSource.FromFile("material_clear.svg"));
            EndIconColorProperty = BindableProperty.Create(nameof(EndIconColor), typeof(Color), typeof(TextInputLayout), defaultValue: ThemeHelper.GetTrailingIconColor());
            DisabledEndIconColorProperty = BindableProperty.Create(nameof(DisabledEndIconColor), typeof(Color), typeof(TextInputLayout), defaultValue: ThemeHelper.GetDisabledTrailingIconColor());
            DisabledEndIconOpacityProperty = BindableProperty.Create(nameof(DisabledEndIconOpacity), typeof(float), typeof(TextInputLayout), defaultValue: ThemeHelper.GetDisabledTrailingIconOpacity());
            EndIconClickedCommandProperty = BindableProperty.Create(nameof(EndIconClickedCommand), typeof(ICommand), typeof(TextInputLayout));

            StartIconProperty = BindableProperty.Create(nameof(StartIcon), typeof(ImageSource), typeof(TextInputLayout));
            StartIconColorProperty = BindableProperty.Create(nameof(StartIconColor), typeof(Color), typeof(TextInputLayout), defaultValue: ThemeHelper.GetLeadingIconColor());
            DisabledStartIconColorProperty = BindableProperty.Create(nameof(DisabledStartIconColor), typeof(Color), typeof(TextInputLayout), defaultValue: ThemeHelper.GetDisabledLeadingIconColor());
            DisabledStartIconOpacityProperty = BindableProperty.Create(nameof(DisabledStartIconOpacity), typeof(float), typeof(TextInputLayout), defaultValue: ThemeHelper.GetDisabledLeadingIconOpacity());

            PrefixProperty = BindableProperty.Create(nameof(Prefix), typeof(string), typeof(TextInputLayout));
            PrefixTextColorProperty = BindableProperty.Create(nameof(PrefixTextColor), typeof(Color), typeof(TextInputLayout), defaultValue: ThemeHelper.GetInputTextPrefixColor());
            DisabledPrefixTextColorProperty = BindableProperty.Create(nameof(DisabledPrefixTextColor), typeof(Color), typeof(TextInputLayout), defaultValue: ThemeHelper.GetDisabledInputTextColor());
            DisabledPrefixTextColorOpacityProperty = BindableProperty.Create(nameof(DisabledPrefixTextColorOpacity), typeof(float), typeof(TextInputLayout), defaultValue: ThemeHelper.GetDisabledInputTextOpacity());

            SuffixProperty = BindableProperty.Create(nameof(Suffix), typeof(string), typeof(TextInputLayout));
            SuffixTextColorProperty = BindableProperty.Create(nameof(SuffixTextColor), typeof(Color), typeof(TextInputLayout), defaultValue: ThemeHelper.GetInputTextSuffixColor());
            DisabledSuffixTextColorProperty = BindableProperty.Create(nameof(DisabledSuffixTextColor), typeof(Color), typeof(TextInputLayout), defaultValue: ThemeHelper.GetDisabledInputTextColor());
            DisabledSuffixTextColorOpacityProperty = BindableProperty.Create(nameof(DisabledSuffixTextColorOpacity), typeof(float), typeof(TextInputLayout), defaultValue: ThemeHelper.GetDisabledInputTextOpacity());

            SupportingTextProperty = BindableProperty.Create(nameof(SupportingText), typeof(string), typeof(TextInputLayout));
            ErrorTextProperty = BindableProperty.Create(nameof(ErrorText), typeof(string), typeof(TextInputLayout));

            BoxStrokeCornerRadiusProperty = BindableProperty.Create(nameof(BoxStrokeCornerRadius), typeof(CornerRadius), typeof(TextInputLayout));
            BoxStrokeWidthProperty = BindableProperty.Create(nameof(BoxStrokeWidth), typeof(int), typeof(TextInputLayout), defaultValue: ThemeHelper.GetOutlineWidth());
            BoxStrokeFocusedWidthProperty = BindableProperty.Create(nameof(BoxStrokeFocusedWidth), typeof(int), typeof(TextInputLayout), defaultValue: ThemeHelper.GetFocusedOutlineWidth());

            CounterEnabledProperty = BindableProperty.Create(nameof(CounterEnabled), typeof(bool), typeof(TextInputLayout));
            CounterMaxLengthProperty = BindableProperty.Create(nameof(CounterMaxLength), typeof(int), typeof(TextInputLayout));

            DisabledBackgroundColorProperty = BindableProperty.Create(nameof(DisabledBackgroundColor), typeof(Color), typeof(TextInputLayout));
            DisabledBackgroundColorOpacityProperty = BindableProperty.Create(nameof(DisabledBackgroundColorOpacity), typeof(float), typeof(TextInputLayout));

            IsErrorEnabledProperty = BindableProperty.Create(nameof(IsErrorEnabled), typeof(bool), typeof(TextInputLayout), defaultValue: false); // Default true adds spacing where text would be

            CursorColorProperty = BindableProperty.Create(nameof(CursorColor), typeof(Color), typeof(TextInputLayout), defaultValue: ThemeHelper.GetCaretColor());
            ErrorCursorColorProperty = BindableProperty.Create(nameof(ErrorCursorColor), typeof(Color), typeof(TextInputLayout), defaultValue: ThemeHelper.GetErrorCaretColor());

            ErrorOutlineColorProperty = BindableProperty.Create(nameof(ErrorOutlineColor), typeof(Color), typeof(TextInputLayout), defaultValue: ThemeHelper.GetErrorFocusedActiveIndicatorColor());
            FocusedErrorOutlineColorProperty = BindableProperty.Create(nameof(FocusedErrorOutlineColor), typeof(Color), typeof(TextInputLayout), defaultValue: ThemeHelper.GetErrorFocusedActiveIndicatorColor());

            CounterTextColorProperty = BindableProperty.Create(nameof(CounterTextColor), typeof(Color), typeof(TextInputLayout), defaultValue: ThemeHelper.GetInputTextColor());
            CounterOverflowTextColorProperty = BindableProperty.Create(nameof(CounterOverflowTextColor), typeof(Color), typeof(TextInputLayout), defaultValue: ThemeHelper.GetErrorSupportingTextColor());
            FocusedCounterOverflowTextColorProperty = BindableProperty.Create(nameof(FocusedCounterOverflowTextColor), typeof(Color), typeof(TextInputLayout), defaultValue: ThemeHelper.GetErrorSupportingTextColor());

            SupportingTextColorProperty = BindableProperty.Create(nameof(SupportingTextColor), typeof(Color), typeof(TextInputLayout), defaultValue: ThemeHelper.GetSupportingTextColor());
            DisabledSupportingTextColorProperty = BindableProperty.Create(nameof(DisabledSupportingTextColor), typeof(Color), typeof(TextInputLayout), defaultValue: ThemeHelper.GetDisabledSupportingTextColor());
            FocusedSupportingTextColorProperty = BindableProperty.Create(nameof(FocusedSupportingTextColor), typeof(Color), typeof(TextInputLayout), defaultValue: ThemeHelper.GetFocusedSupportingTextColor());
            DisabledSupportingTextColorOpacityProperty = BindableProperty.Create(nameof(DisabledSupportingTextColorOpacity), typeof(float), typeof(TextInputLayout), defaultValue: ThemeHelper.GetDisabledSupportingTextOpacity());

            StartIconClickedCommandProperty = BindableProperty.Create(nameof(StartIconClickedCommand), typeof(ICommand), typeof(TextInputLayout));
            EndIconClickedCommandProperty = BindableProperty.Create(nameof(EndIconClickedCommand), typeof(ICommand), typeof(TextInputLayout), defaultValueCreator: EndIconClickedDefaultValueCreator);
        }

        private static object EndIconClickedDefaultValueCreator(BindableObject bindable)
        {
            if (bindable is TextInputLayout control && control.Handler is TextInputLayoutHandler handler)
            {
                return new Command(() =>
                {
                    handler?.PlatformView?.DefaultEndIconClickedCommand();
                });
            }
            return null!;
        }

        private static void BoxBackgroundModePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is not TextInputLayout control || newValue is not BoxBackgroundMode mode)
            {
                return;
            }

            // Background Color / Container Color
            control.TrySetDefaultProperty(BackgroundColorProperty, ThemeHelper.GetContainerColor(mode)); 
            control.TrySetDefaultProperty(DisabledBackgroundColorProperty, ThemeHelper.GetDisabledContainerColor(mode));
            control.TrySetDefaultProperty(DisabledBackgroundColorOpacityProperty, ThemeHelper.GetDisabledContainerOpacity(mode));

            control.TrySetDefaultProperty(BoxStrokeCornerRadiusProperty, ThemeHelper.GetContainerShape(mode).CornerRadius);
        }

        private void TrySetDefaultProperty<T>(BindableProperty property, T value)
        {
            if (!IsSet(property))
            {
                SetValue(property, value);
            }
        }


        public static readonly BindableProperty OutlineColorProperty;
        public static readonly BindableProperty FocusedOutlineColorProperty;
        public static readonly BindableProperty DisabledOutlineColorProperty;
        public static readonly BindableProperty DisabledOutlineOpacityProperty;

        public static readonly BindableProperty DisabledBackgroundColorProperty;
        public static readonly BindableProperty DisabledBackgroundColorOpacityProperty;

        public static readonly BindableProperty HintProperty;
        public static readonly BindableProperty DefaultHintColorProperty;
        public static readonly BindableProperty FocusedHintColorProperty;
        public static readonly BindableProperty DisabledHintColorProperty;
        public static readonly BindableProperty DisabledHintOpacityProperty;
        public static readonly BindableProperty IsHintAlwaysExpandedProperty;

        public static readonly BindableProperty BoxBackgroundModeProperty;
        public static readonly BindableProperty EndIconVisibilityModeProperty;
        public static readonly BindableProperty EndIconModeProperty;

        public static readonly BindableProperty EndIconProperty;
        public static readonly BindableProperty EndIconColorProperty;
        public static readonly BindableProperty DisabledEndIconColorProperty;
        public static readonly BindableProperty DisabledEndIconOpacityProperty;

        public static readonly BindableProperty StartIconProperty;
        public static readonly BindableProperty StartIconColorProperty;
        public static readonly BindableProperty DisabledStartIconColorProperty;
        public static readonly BindableProperty DisabledStartIconOpacityProperty;

        public static readonly BindableProperty PrefixProperty;
        public static readonly BindableProperty PrefixTextColorProperty;
        public static readonly BindableProperty DisabledPrefixTextColorProperty;
        public static readonly BindableProperty DisabledPrefixTextColorOpacityProperty;

        public static readonly BindableProperty SuffixProperty;
        public static readonly BindableProperty SuffixTextColorProperty;
        public static readonly BindableProperty DisabledSuffixTextColorProperty;
        public static readonly BindableProperty DisabledSuffixTextColorOpacityProperty;

        public static readonly BindableProperty BoxStrokeCornerRadiusProperty;
        public static readonly BindableProperty BoxStrokeWidthProperty;
        public static readonly BindableProperty BoxStrokeFocusedWidthProperty;

        public static readonly BindableProperty SupportingTextProperty;
        public static readonly BindableProperty ErrorTextProperty;
        
        public static readonly BindableProperty CounterEnabledProperty;
        public static readonly BindableProperty CounterMaxLengthProperty;

        public static readonly BindableProperty IsErrorEnabledProperty;

        public static readonly BindableProperty EndIconClickedCommandProperty;

        public static readonly BindableProperty CursorColorProperty;
        public static readonly BindableProperty ErrorCursorColorProperty;

        public static readonly BindableProperty ErrorOutlineColorProperty;
        public static readonly BindableProperty FocusedErrorOutlineColorProperty;

        public static readonly BindableProperty CounterTextColorProperty;
        public static readonly BindableProperty CounterOverflowTextColorProperty;
        public static readonly BindableProperty FocusedCounterOverflowTextColorProperty;

        public static readonly BindableProperty SupportingTextColorProperty;
        public static readonly BindableProperty DisabledSupportingTextColorProperty;
        public static readonly BindableProperty FocusedSupportingTextColorProperty;
        public static readonly BindableProperty DisabledSupportingTextColorOpacityProperty;

        public static readonly BindableProperty StartIconClickedCommandProperty;
        
        public void EndIconClicked()
        {
            if (EndIconClickedCommand is not null)
            {
                EndIconClickedCommand.Execute(null);
                return;
            }

            if(EndIconMode == EndIconMode.ClearText)
            {
                if (Content is InputView inputView)
                {
                    inputView.Text = string.Empty;
                }
                else if (Content is Picker picker)
                {
                    picker.SelectedItem = null;
                }
            }
        }

        public void StartIconClicked()
        {
  
        }

        public void ErrorIconClicked()
        {
            
        }

        public Color OutlineColor
        {
            get => (Color)GetValue(OutlineColorProperty);
            set => SetValue(OutlineColorProperty, value);
        }
        
        public Color FocusedOutlineColor
        {
            get => (Color)GetValue(FocusedOutlineColorProperty);
            set => SetValue(FocusedOutlineColorProperty, value);
        }
        public Color DisabledOutlineColor
        {
            get => (Color)GetValue(DisabledOutlineColorProperty);
            set => SetValue(DisabledOutlineColorProperty, value);
        }
        public float DisabledOutlineOpacity
        {
            get => (float)GetValue(DisabledOutlineOpacityProperty);
            set => SetValue(DisabledOutlineOpacityProperty, value);
        }
        public Color DisabledBackgroundColor
        {
            get => (Color)GetValue(DisabledBackgroundColorProperty);
            set => SetValue(DisabledBackgroundColorProperty, value);
        }

        public float DisabledBackgroundColorOpacity
        {
            get => (float)GetValue(DisabledBackgroundColorOpacityProperty);
            set => SetValue(DisabledBackgroundColorOpacityProperty, value);
        }

        public string Hint
        {
            get => GetValue(HintProperty)?.ToString() ?? string.Empty;
            set => SetValue(HintProperty, value);
        }

        public Color DefaultHintColor
        {
            get => (Color)GetValue(DefaultHintColorProperty);
            set => SetValue(DefaultHintColorProperty, value);
        }

        public Color FocusedHintColor
        {
            get => (Color)GetValue(FocusedHintColorProperty);
            set => SetValue(FocusedHintColorProperty, value);
        }
        public Color DisabledHintColor
        {
            get => (Color)GetValue(DisabledHintColorProperty);
            set => SetValue(DisabledHintColorProperty, value);
        }
        public float DisabledHintOpacity
        {
            get => (float)GetValue(DisabledOutlineOpacityProperty);
            set => SetValue(DisabledOutlineOpacityProperty, value);
        }

        public bool IsHintAlwaysExpanded
        {
            get => (bool)GetValue(IsHintAlwaysExpandedProperty);
            set => SetValue(IsHintAlwaysExpandedProperty, value);
        }

        /// <summary>
        /// BoxBackground mode needs to be set upon initialization of the component. This cannot be changed once set.
        /// </summary>
        public BoxBackgroundMode BoxBackgroundMode
        {
            get => (BoxBackgroundMode)GetValue(BoxBackgroundModeProperty);
            set => SetValue(BoxBackgroundModeProperty, value);
        }
        public IconVisibilityMode EndIconVisibilityMode
        {
            get => (IconVisibilityMode)GetValue(EndIconVisibilityModeProperty);
            set => SetValue(EndIconVisibilityModeProperty, value);
        }
        public EndIconMode EndIconMode
        {
            get => (EndIconMode)GetValue(EndIconModeProperty);
            set => SetValue(EndIconModeProperty, value);
        }

        [TypeConverter(typeof(ImageSourceConverter))]
        public ImageSource EndIcon
        {
            get => (ImageSource)GetValue(EndIconProperty);
            set => SetValue(EndIconProperty, value);
        }
        public Color EndIconColor
        {
            get => (Color)GetValue(EndIconColorProperty);
            set => SetValue(EndIconColorProperty, value);
        }

        public Color DisabledEndIconColor
        {
            get => (Color)GetValue(DisabledEndIconColorProperty);
            set => SetValue(DisabledEndIconColorProperty, value);
        }
        public float DisabledEndIconOpacity
        {
            get => (float)GetValue(DisabledEndIconOpacityProperty);
            set => SetValue(DisabledEndIconOpacityProperty, value);
        }

        [TypeConverter(typeof(ImageSourceConverter))]
        public ImageSource StartIcon
        {
            get => (ImageSource)GetValue(StartIconProperty);
            set => SetValue(StartIconProperty, value);
        }
        public Color StartIconColor
        {
            get => (Color)GetValue(StartIconColorProperty);
            set => SetValue(StartIconColorProperty, value);
        }

        public Color DisabledStartIconColor
        {
            get => (Color)GetValue(DisabledStartIconColorProperty);
            set => SetValue(DisabledStartIconColorProperty, value);
        }
        public float DisabledStartIconOpacity
        {
            get => (float)GetValue(DisabledStartIconOpacityProperty);
            set => SetValue(DisabledStartIconOpacityProperty, value);
        }

        public string? Prefix
        {
            get => GetValue(PrefixProperty)?.ToString();
            set => SetValue(PrefixProperty, value);
        }
        public Color PrefixTextColor
        {
            get => (Color)GetValue(PrefixTextColorProperty);
            set => SetValue(PrefixTextColorProperty, value);
        }
        public Color DisabledPrefixTextColor
        {
            get => (Color)GetValue(DisabledPrefixTextColorProperty);
            set => SetValue(DisabledPrefixTextColorProperty, value);
        }
        public float DisabledPrefixTextColorOpacity
        {
            get => (float)GetValue(DisabledPrefixTextColorOpacityProperty);
            set => SetValue(DisabledPrefixTextColorOpacityProperty, value);
        }
        public string? Suffix
        {
            get => GetValue(SuffixProperty)?.ToString();
            set => SetValue(SuffixProperty, value);
        }
        public Color SuffixTextColor
        {
            get => (Color)GetValue(SuffixTextColorProperty);
            set => SetValue(SuffixTextColorProperty, value);
        }
        public Color DisabledSuffixTextColor
        {
            get => (Color)GetValue(DisabledSuffixTextColorProperty);
            set => SetValue(DisabledSuffixTextColorProperty, value);
        }
        public float DisabledSuffixTextColorOpacity
        {
            get => (float)GetValue(DisabledSuffixTextColorOpacityProperty);
            set => SetValue(DisabledSuffixTextColorOpacityProperty, value);
        }
        public string? SupportingText
        {
            get => GetValue(SupportingTextProperty)?.ToString();
            set => SetValue(SupportingTextProperty, value);
        }

        public CornerRadius BoxStrokeCornerRadius
        {
            get => (CornerRadius)GetValue(BoxStrokeCornerRadiusProperty);
            set => SetValue(BoxStrokeCornerRadiusProperty, value);
        }
        public int BoxStrokeWidth
        {
            get => (int)GetValue(BoxStrokeWidthProperty);
            set => SetValue(BoxStrokeWidthProperty, value);
        }
        public int BoxStrokeFocusedWidth
        {
            get => (int)GetValue(BoxStrokeFocusedWidthProperty);
            set => SetValue(BoxStrokeFocusedWidthProperty, value);
        }

        public bool CounterEnabled
        {
            get => (bool)GetValue(CounterEnabledProperty);
            set => SetValue(CounterEnabledProperty, value);
        }
        public int CounterMaxLength
        {
            get => (int)GetValue(CounterMaxLengthProperty);
            set => SetValue(CounterMaxLengthProperty, value);
        }
        public string? ErrorText
        {
            get => GetValue(ErrorTextProperty)?.ToString();
            set => SetValue(ErrorTextProperty, value);
        }

        public bool IsErrorEnabled
        {
            get => (bool)GetValue(IsErrorEnabledProperty);
            set => SetValue(IsErrorEnabledProperty, value);
        }

        public ICommand EndIconClickedCommand
        {
            get => (ICommand)GetValue(EndIconClickedCommandProperty);
            set => SetValue(EndIconClickedCommandProperty, value);
        }
        
        public Color CursorColor
        {
            get => (Color)GetValue(CursorColorProperty);
            set => SetValue(CursorColorProperty, value);
        }
        public Color ErrorCursorColor
        {
            get => (Color)GetValue(ErrorCursorColorProperty);
            set => SetValue(ErrorCursorColorProperty, value);
        }
        public Color ErrorOutlineColor
        {
            get => (Color)GetValue(ErrorOutlineColorProperty);
            set => SetValue(ErrorOutlineColorProperty, value);
        }
        
        public Color FocusedErrorOutlineColor
        {
            get => (Color)GetValue(FocusedErrorOutlineColorProperty);
            set => SetValue(FocusedErrorOutlineColorProperty, value);
        }

        public Color CounterTextColor
        {
            get => (Color)GetValue(CounterTextColorProperty);
            set => SetValue(CounterTextColorProperty, value);
        }

        public Color CounterOverflowTextColor
        {
            get => (Color)GetValue(CounterOverflowTextColorProperty);
            set => SetValue(CounterOverflowTextColorProperty, value);
        }

        public Color FocusedCounterOverflowTextColor
        {
            get => (Color)GetValue(FocusedCounterOverflowTextColorProperty);
            set => SetValue(FocusedCounterOverflowTextColorProperty, value);
        }
        public Color SupportingTextColor
        {
            get => (Color)GetValue(SupportingTextColorProperty);
            set => SetValue(SupportingTextColorProperty, value);
        }
        public Color DisabledSupportingTextColor
        {
            get => (Color)GetValue(DisabledSupportingTextColorProperty);
            set => SetValue(DisabledSupportingTextColorProperty, value);
        }
        public Color FocusedSupportingTextColor
        {
            get => (Color)GetValue(FocusedSupportingTextColorProperty);
            set => SetValue(FocusedSupportingTextColorProperty, value);
        }
        public float DisabledSupportingTextColorOpacity
        {
            get => (float)GetValue(DisabledSupportingTextColorOpacityProperty);
            set => SetValue(DisabledSupportingTextColorOpacityProperty, value);
        }
        
        public ICommand StartIconClickedCommand
        {
            get => (ICommand)GetValue(StartIconClickedCommandProperty);
            set => SetValue(StartIconClickedCommandProperty, value);
        }
        public IStatefulColor SupportingTextColors => new StatefulColor
        (
            SupportingTextColor,
            FocusedSupportingTextColor,
            DisabledSupportingTextColor,
            DisabledSupportingTextColorOpacity
        );
    }
}
