using StaffContactDirectory.Models;
using StaffContactDirectory.ViewModels;
namespace StaffContactDirectory.Views;

public partial class ContactDetailsPage : ContentPage
{
	public ContactDetailsPage(PeopleModel selectedPerson)
	{
		InitializeComponent();
		BindingContext = new ContactDetailsViewModel();

		var viewModel = (ContactDetailsViewModel)BindingContext;
		viewModel.SelectedPerson = selectedPerson;

	}
}