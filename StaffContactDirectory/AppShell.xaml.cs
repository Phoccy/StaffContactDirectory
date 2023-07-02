namespace StaffContactDirectory;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(ContactDetailsPage), typeof(ContactDetailsPage));
        Routing.RegisterRoute(nameof(AddContactPage), typeof(AddContactPage));
    }
}
