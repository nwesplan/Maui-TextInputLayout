using Android.Content.Res;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Platform;
using Nwesp.Maui.Android.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Nwesp.Maui.Android.Hosting
{
    public static class MauiAppBuilderExtensions
    {
        public static MauiAppBuilder AddTextInputLayout(this MauiAppBuilder builder)
        {
            builder.ConfigureMauiHandlers(config =>
            {
                config.AddHandler<TextInputLayout, TextInputLayoutHandler>();
                config.AddHandler<MaterialEntry, MaterialEntryHandler>();
                config.AddHandler<MaterialPicker, MaterialPickerHandler>();
                config.AddHandler<MaterialDatePicker, MaterialDatePickerHandler>();
            });
         

            #if DEBUG
                builder.Logging.SetMinimumLevel(LogLevel.Debug);
            #else
                builder.Logging.SetMinimumLevel(LogLevel.Warning);
            #endif


            return builder;
        }
    }
}
