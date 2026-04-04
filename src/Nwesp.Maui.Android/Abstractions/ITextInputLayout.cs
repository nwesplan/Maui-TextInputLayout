using Nwesp.Maui.Android.Models.Enums;
using Nwesp.Maui.Android.Models.Events;
using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nwesp.Maui.Android.Abstractions
{
    public interface ITextInputLayout : IView, IContentView
    {
        Color BackgroundColor { get; set; }
        Color DisabledBackgroundColor { get; set; }
        float DisabledBackgroundColorOpacity { get; set; }
        Color OutlineColor { get; set; }
        Color FocusedOutlineColor { get; set; }
        Color DisabledOutlineColor { get; set; }
        float DisabledOutlineOpacity { get; set; }
        string Hint { get; set; }
        Color HintColor { get; set; }
        Color FocusedHintColor { get; set; }
        Color DisabledHintColor { get; set; }
        float DisabledHintOpacity { get; set; }
        bool IsHintAlwaysExpanded { get; set; }

        BoxBackgroundMode BoxBackgroundMode { get; set; }
        IconVisibilityMode EndIconVisibilityMode { get; set; }
        EndIconMode EndIconMode { get; set; }
        ICommand EndIconClickedCommand { get; set; }
        ImageSource EndIcon { get; set; }
        Color EndIconColor { get; set; }
        Color DisabledEndIconColor { get; set; }
        float DisabledEndIconOpacity { get; set; }
        ICommand StartIconClickedCommand { get; set; }
        ImageSource StartIcon { get; set; }
        Color StartIconColor { get; set; }
        Color DisabledStartIconColor { get; set; }
        float DisabledStartIconOpacity { get; set; }
        string? Prefix { get; set; }
        string? Suffix { get; set; }
        string? SupportingText { get; set; }
        CornerRadius BoxStrokeCornerRadius { get; set; }
        int BoxStrokeWidth { get; set; }
        int BoxStrokeFocusedWidth { get; set; }
        bool CounterEnabled { get; set; }
        int CounterMaxLength { get; set; }
        Color SuffixTextColor { get; set; }
        Color DisabledSuffixTextColor { get; set; }
        float DisabledSuffixTextColorOpacity { get; set; }
        Color PrefixTextColor { get; set; }
        Color DisabledPrefixTextColor { get; set; }
        float DisabledPrefixTextColorOpacity { get; set; }
        IMaterialEntry? MaterialEntry { get; }
        Color CursorColor { get; set; }
        ImageSource ErrorIcon { get; set; }
        ICommand ErrorIconClickedCommand { get; set; }
        string? ErrorText { get; set; }
        bool IsErrorEnabled { get; set; }
        Color ErrorCursorColor { get; set; }
        Color ErrorOutlineColor { get; set; }
        Color FocusedErrorOutlineColor { get; set; }
        Color ErrorTextColor { get; set; }
        Color CounterTextColor { get; set; }
        Color CounterOverflowTextColor { get; set; }
        Color FocusedCounterOverflowTextColor { get; set; }
        IInteractiveColor CounterOverflowTextColors { get; }
        Color SupportingTextColor { get; set; }
        Color DisabledSupportingTextColor { get; set; }
        Color FocusedSupportingTextColor { get; set; }
        float DisabledSupportingTextColorOpacity { get; set; }
        IStatefulColor SupportingTextColors { get; }
        ImageSource ShowPasswordIcon { get; set; }
        ImageSource HidePasswordIcon { get; set; }
    }
}
