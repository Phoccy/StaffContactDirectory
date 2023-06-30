using StaffContactDirectory.ViewModel;

namespace StaffContactDirectory.View;

public partial class AboutPage : ContentPage
{
	public AboutPage(AboutViewModel aboutViewModel)
	{
		InitializeComponent();
		BindingContext = aboutViewModel;
	}
}