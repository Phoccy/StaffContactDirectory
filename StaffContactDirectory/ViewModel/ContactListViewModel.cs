namespace StaffContactDirectory.ViewModel
{
    public partial class ContactListViewModel : BaseViewModel
    {
        public ObservableCollection<PeopleModel> People { get; } = new();
        PeopleService peopleService;
        IConnectivity connectivity;

        public ContactListViewModel(PeopleService peopleService, IConnectivity connectivity)
        {
            Title = "Contact List";
            this.peopleService = peopleService;
            this.connectivity = connectivity;
        }

               [RelayCommand]
        async Task GoToContactDetails(PeopleModel person)
        {
            if (person == null) return;

            await Shell.Current.GoToAsync(nameof(ContactDetailsPage), true, new Dictionary<string, object>
            {
                {"Person", person}
            });
        }

        [ObservableProperty]
        bool isRefreshing;

        [RelayCommand]
        async Task LoadPeople()
        {
            if (IsBusy) return;

            try
            {
                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No Connectivity", "I don't know what to do!", "OK");
                    return;
                }

                IsBusy = true;
                var people = await peopleService.GetPeopleAsync();
                if (People.Count != 0) People.Clear();
                foreach (var person in people)
                    People.Add(person);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"The people are out of reach: {ex.Message}");
                await Shell.Current.DisplayAlert("Whoops!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }
        [RelayCommand]
        private async void Delete(PeopleModel person)
        {
            // Fix this up
            await peopleService.DeletePersonAsync(person);
        }
    }
}
