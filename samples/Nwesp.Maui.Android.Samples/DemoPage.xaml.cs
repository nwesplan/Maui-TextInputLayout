
using Nwesp.Maui.Android.Models.Enums;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Nwesp.Maui.Android.Samples;

public partial class DemoPage : ContentPage, IQueryAttributable
{
    public DemoPage()
	{
        
        InitializeComponent();
        
        Data = new(PageRoute.GetPages());
        PageCommand = new Command<object>(GoToPage);
        BindingContext = this;
    }

    private ObservableCollection<PageRoute> _data = [];
    public ObservableCollection<PageRoute> Data
    {
        get => _data;
        set
        {
            _data = value;
            OnPropertyChanged();
        }
    }

    public ICommand PageCommand { get; }


    private async void GoToPage(object obj)
    {
        if (obj is not PageRoute page)
        {
            return;
        }

        await Shell.Current.GoToAsync(page.Route, new Dictionary<string, object>()
        { 
            { nameof(PageRoute), page },
            {nameof(BoxBackgroundMode), _boxBackgroundMode }
        });
    }

    public class PageRoute
    {
        public string Route { get; set; }
        public string Description { get; set; }

        public PageRoute(string route, string description)
        {
            Route = route;
            Description = description;
        }

        public static IEnumerable<PageRoute> GetPages()
        {
            return
            [
                new PageRoute(nameof(PasswordPage), "Password Demo"),
                new PageRoute(nameof(PrefixSuffixPage), "Prefix / Suffix Demo"),
                new PageRoute(nameof(ErrorAndCounterPage), "Error / Counter Demo"),
                new PageRoute(nameof(EndIconClearTextPage), "End Icon / Clear Text Demo"),
                new PageRoute(nameof(StartIconPage), "Start Icon Demo"),
                new PageRoute(nameof(ColorsPage), "Colors"),
                new PageRoute(nameof(StandardFormPage), "Standard Form"),
                new PageRoute(nameof(TestPage), "Test")
            ];
        }
    }

    private BoxBackgroundMode _boxBackgroundMode;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if(query.TryGetValue(nameof(BoxBackgroundMode), out var obj) && obj is BoxBackgroundMode boxBackgroundMode)
        {
            _boxBackgroundMode = boxBackgroundMode;
        }
    }
}