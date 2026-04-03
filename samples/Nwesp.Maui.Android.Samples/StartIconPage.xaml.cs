using System.Windows.Input;

namespace Nwesp.Maui.Android.Samples;

public partial class StartIconPage : BaseDemoPage
{
	public StartIconPage()
	{
		InitializeComponent();
		StartIconClickedCommand = new Command(async () =>
		{
            await DisplayAlertAsync("Command", "Custom Click Command", "Ok");
        });
		BindingContext = this;
	}

	public ICommand StartIconClickedCommand { get; }
}