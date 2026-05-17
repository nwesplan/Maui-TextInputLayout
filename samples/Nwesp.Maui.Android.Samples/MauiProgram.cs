using Microsoft.Extensions.Logging;
using Nwesp.Maui.Android.Hosting;
using Nwesp.Maui.Android.Samples.Services;

namespace Nwesp.Maui.Android.Samples
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .AddTextInputLayout();

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder
                .RegisterPages()
                .RegisterServices();
            return builder.Build();
        }

        private static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<BoxBackgroundService>();

            return builder;
        }

        private static MauiAppBuilder RegisterPages(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<DemoPage>();
            builder.Services.AddTransient<PasswordPage>();
            builder.Services.AddTransient<PrefixSuffixPage>();
            builder.Services.AddTransient<TestPage>();
            builder.Services.AddTransient<ErrorAndCounterPage>();
            builder.Services.AddTransient<EndIconClearTextPage>();
            builder.Services.AddTransient<StartIconPage>();
            builder.Services.AddTransient<ColorsPage>();
            builder.Services.AddTransient<StandardFormPage>();
            return builder;
        }
    }
}
