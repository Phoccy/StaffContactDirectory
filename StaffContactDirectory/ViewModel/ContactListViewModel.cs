namespace StaffContactDirectory.ViewModel
{
    public partial class ContactListViewModel : BaseViewModel
    {
        public ObservableCollection<PeopleModel> People { get; } = new ObservableCollection<PeopleModel>();

        PeopleService peopleService;
        IConnectivity connectivity;

        [ObservableProperty]
        bool isRefreshing;

        public ContactListViewModel(PeopleService peopleService, IConnectivity connectivity)
        {
            Title = "Contact List";
            this.peopleService = peopleService;
            this.connectivity = connectivity;
        }

        [RelayCommand]
        async Task AddContact()
        {
            await Shell.Current.GoToAsync(nameof(AddContactPage), true);
        }

        [RelayCommand]
        async Task GoToContactDetails(PeopleModel person)
        {
            if (person == null)
                return;

            await Shell.Current.GoToAsync(nameof(ContactDetailsPage), true, new Dictionary<string, object>
            {
                {"Person", person}
            });
        }

        [RelayCommand]
        public async Task GetPeople()
        {
            if (IsBusy)
                return;

            try
            {
                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No Connectivity", "I don't know what to do!", "OK");
                    return;
                }

                IsBusy = true;
                var people = await peopleService.GetPeopleAsync();

                if (People.Count != 0)
                    People.Clear();

                foreach (var person in people)
                    People.Add(person);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"The peopleList are out of reach: {ex.Message}");
                await Shell.Current.DisplayAlert("Whoops!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }
        // TODO: Add this somewhere
        [RelayCommand]
        async Task DeletePerson(PeopleModel person)
        {
            if (person == null) return;
            bool result = await Shell.Current.DisplayAlert("Confirmation", $"Are you sure you want to delete {person.Name}?", "Yes", "No");
            if (result)
            {
                await peopleService.DeletePersonAsync(person);
                await GetPeople();
            }

        }
    }
}
