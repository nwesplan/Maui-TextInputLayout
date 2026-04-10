using Android.Graphics;
using Android.Text;
using Android.Text.Method;
using Android.Text.Style;
using Android.Widget;
using AndroidX.AppCompat.View;
using AndroidX.AppCompat.Widget;
using Java.Lang;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Nwesp.Maui.Android.Abstractions;
using Nwesp.Maui.Android.Controls;
using Nwesp.Maui.Android.Models.Enums;
using Nwesp.Maui.Android.Platforms.Android;
using Nwesp.Maui.Android.Platforms.Android.Managers;
using Nwesp.Maui.Android.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nwesp.Maui.Android
{
    public partial class MaterialEntryHandler : EntryHandler
    {
        public static new IPropertyMapper<MaterialEntry, MaterialEntryHandler> Mapper = new PropertyMapper<MaterialEntry, MaterialEntryHandler>(EntryHandler.Mapper)
        {
            [nameof(IMaterialEntry.TextColor)] = MapTextColor,
            [nameof(IMaterialEntry.DisabledTextColor)] = MapTextColor,
            [nameof(IMaterialEntry.DisabledTextColorOpacity)] = MapTextColor,
        };

        public static new CommandMapper<MaterialEntry, MaterialEntryHandler> CommandMapper = new(EntryHandler.CommandMapper)
        {

        };

        public MaterialEntryHandler() : base(Mapper, CommandMapper)
        {
        }

        protected override MauiAppCompatEditText CreatePlatformView()
        {
            var editText = ContextThemeHelper.BuildContextThemeWrapper(Context, _boxBackgroundMode, (t) => new MauiAppCompatEditText(t));
            
            return editText;
        }

        private BoxBackgroundMode _boxBackgroundMode;
        public override void SetVirtualView(IView view)
        {
            _boxBackgroundMode = OutlineManager.ParseBoxBackgroundMode(view);

            base.SetVirtualView(view);
            if(view is IEntry entry && entry.IsPassword)
            {
                // Hack: Fix for password mask spacing upon initial load.
                PlatformView.Post(() =>
                {
                    var logger = MauiContext?.Services.GetRequiredService<ILogger<TextInputLayoutHandler>>();
                    logger?.LogWarning("Toggling Password init");
                    PlatformView?.TogglePasswordOn();
                });
            } 
        }

        protected override void ConnectHandler(MauiAppCompatEditText platformView)
        {
            base.ConnectHandler(platformView);
            //platformView.AddTextChangedListener(new TextWatcher());
        }

        protected override void DisconnectHandler(MauiAppCompatEditText platformView)
        {
            base.DisconnectHandler(platformView);
        }

        public static void MapTextColor(MaterialEntryHandler handler, MaterialEntry view)
        {
            handler.PlatformView?.UpdateTextColor(view);
        }

        private class TextWatcher : Java.Lang.Object, ITextWatcher
        {
            // Removes underline from current word while typing
            public void AfterTextChanged(IEditable? s)
            {
                var spans = s?.GetSpans(0, s.Length(), Java.Lang.Class.FromType(typeof(UnderlineSpan)))?.Cast<UnderlineSpan>();

                if(spans is null)
                {
                    return;
                }

                foreach (UnderlineSpan span in spans)
                {
                    s?.RemoveSpan(span);
                }
            }

            public void BeforeTextChanged(ICharSequence? s, int start, int count, int after)
            {
            }

            public void OnTextChanged(ICharSequence? s, int start, int before, int count)
            {
            }
        }
    }
}
