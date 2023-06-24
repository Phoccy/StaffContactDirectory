using StaffContactDirectory.ViewModels;

namespace StaffContactDirectory.Views;

public partial class MainView : ContentPage
{	

	public MainView()
	{		
        InitializeComponent();
		BindingContext = new MainViewModel();
	}
}