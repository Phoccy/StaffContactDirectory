using StaffContactDirectory.Models;
using StaffContactDirectory.ViewModels;

namespace StaffContactDirectory.Views;

public partial class ContactListPage : ContentPage
{
	public ContactListPage()
	{
		InitializeComponent();
		BindingContext = new ContactListViewModel();
	}
    private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
	{

	}
    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is PeopleModel selectedPerson)
        {
            // Access the selected person object (selectedPerson) and perform any desired actions
            // For example, you can display a message with the selected person's details
           // DisplayAlert($"Name: {selectedPerson.Name}", $"Phone: {selectedPerson.Phone}", "OK");
            await Navigation.PushAsync(new ContactDetailsPage(selectedPerson));
        }

        collectionView.SelectedItem = null; // Deselect the item after handling the selection
    }
}