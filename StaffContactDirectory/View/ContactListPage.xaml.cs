namespace StaffContactDirectory.View;

public partial class ContactListPage : ContentPage
{
	public ContactListPage(ContactListViewModel contactListViewModel)
	{
		InitializeComponent();
		BindingContext = contactListViewModel;
		contactListViewModel.LoadPeopleCommand.Execute(this);
	}
}