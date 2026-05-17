using Nwesp.Maui.Android.Hosting;

namespace Nwesp.Maui.Android.Samples
{
    public partial class App : Application
    {
        private readonly AppShell _shell;

        public App(AppShell shell)
        {
            InitializeComponent();

            _shell = shell;

            RegisterPages();
            this.ConfigureTextInputLayoutThemes();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(_shell);
        }

        private static void RegisterPages()
        {
            RegisterRoute<DemoPage>();
            RegisterRoute<PasswordPage>();
            RegisterRoute<PrefixSuffixPage>();
            RegisterRoute<TestPage>();
            RegisterRoute<ErrorAndCounterPage>();
            RegisterRoute<EndIconClearTextPage>();
            RegisterRoute<StartIconPage>();
            RegisterRoute<ColorsPage>();
            RegisterRoute<StandardFormPage>();
        }
        private static void RegisterRoute<T>()
        {
            var name = typeof(T).Name;
            Routing.RegisterRoute(name, typeof(T));
        }
    }
}
