using StaffContactDirectory.Views;

namespace StaffContactDirectory;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
	}

	public void OnSettingsClicked(object sender, EventArgs e)
	{
        Application.Current.MainPage.Navigation.PushAsync(new SettingsPage());
    }
}
