using System.Windows.Input;

namespace Nwesp.Maui.Android.Samples;

public partial class TestPage : BaseDemoPage
{
	public TestPage()
	{

        ErrorIconClickedCommand = new Command(async () =>
        {
            await DisplayAlertAsync("Error", "Error Message", "Ok");
        });
        ButtonCommand = new Command(() => 
            IsLayoutVisible = !IsLayoutVisible
        );
        InitializeComponent();
        BindingContext = this;
	}

    private bool _isLayoutVisible;
    public bool IsLayoutVisible
    {
        get => _isLayoutVisible;
        set
        {
            _isLayoutVisible = value;
            OnPropertyChanged();
        }
    }
    public ICommand ButtonCommand { get; }
    public ICommand ErrorIconClickedCommand { get; }
}