using Nwesp.Maui.Android.Controls;
using Nwesp.Maui.Android.Models.Enums;
using Nwesp.Maui.Android.Samples.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using static AndroidX.ConstraintLayout.Core.Motion.Utils.HyperSpline;

namespace Nwesp.Maui.Android.Samples
{
    public partial class MainPage : ContentPage
    {
     
        private readonly BoxBackgroundService _boxBackgroundModeService;

        public MainPage()
        {
            _boxBackgroundModeService = Application.Current?.Handler?.MauiContext?.Services.GetService<BoxBackgroundService>() ?? new();
            InitializeComponent();
           
            
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                var stack = (e.ExceptionObject as Exception)?.ToString();

            };
            AppDomain.CurrentDomain.FirstChanceException += (s, e) =>
            {

            };

            FilledCommand = new Command(async () =>
            {
                _boxBackgroundModeService.SetBackground(BoxBackgroundMode.Filled);
                await Shell.Current.GoToAsync(nameof(DemoPage), new Dictionary<string, object>()
                {
                    { nameof(BoxBackgroundMode), BoxBackgroundMode.Filled }
                });
            });

            OutlineCommand = new Command(async () =>
            {
                _boxBackgroundModeService.SetBackground(BoxBackgroundMode.Outline);
                await Shell.Current.GoToAsync(nameof(DemoPage), new Dictionary<string, object>()
                {
                    { nameof(BoxBackgroundMode), BoxBackgroundMode.Outline }
                });
            });

            BindingContext = this;
            
            vsl.Add(new TextInputLayout()
            {
                BoxBackgroundMode = BoxBackgroundMode.Outline,
                Hint = "Label text",
                Content = new MaterialEntry()
            });
        }

        public ICommand FilledCommand { get; }
        public ICommand OutlineCommand { get; }

    }
}
