using StaffContactDirectory.ViewModels;

namespace StaffContactDirectory.Views;

public partial class AboutPage : ContentPage
{
	public AboutPage()
	{
		InitializeComponent();
		BindingContext = new AboutViewModel();
	}
}