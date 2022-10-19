using TraductionMots.ViewModels;

namespace TraductionMots;

public partial class MainPage : ContentPage
{
	
	public MainPage()
	{
		InitializeComponent();
        BindingContext = new TraductionViewModel();
    }

	
}

