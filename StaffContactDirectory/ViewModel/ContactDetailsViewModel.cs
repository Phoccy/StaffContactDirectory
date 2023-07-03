namespace StaffContactDirectory.ViewModel
{
    [QueryProperty(nameof(Person), "Person")]
    public partial class ContactDetailsViewModel : BaseViewModel
    {
        public ObservableCollection<DepartmentsModel> Departments { get; } = new ObservableCollection<DepartmentsModel>();

        DepartmentsService departmentsService;
        PeopleService peopleService;
        IConnectivity connectivity;

        [ObservableProperty]
        PeopleModel person;
        [ObservableProperty]
        UpdatedPersonModel updatedPerson;
        [ObservableProperty]
        bool isRefreshing;
        [ObservableProperty]
        int selectedIndex;
        [ObservableProperty]
        object selectedItem;

        public ContactDetailsViewModel(DepartmentsService departmentsService, PeopleService peopleService, IConnectivity connectivity)
        {
            Title = "Contact Details";
            Person = new PeopleModel();
            UpdatedPerson = new UpdatedPersonModel();
            this.departmentsService = departmentsService;
            this.peopleService = peopleService;
            this.connectivity = connectivity;
        }

        [RelayCommand]
        public async Task GetDepartments()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                // Fetch data from local storage
                List<DepartmentsModel> localDepartments = await FetchDepartmentsFromLocalStorage();

                if (localDepartments != null)
                {
                    // Data available in local storage, update the UI
                    Departments.Clear();
                    foreach (var department in localDepartments)
                        Departments.Add(department);
                }

                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    // Fetch data from the server
                    var departments = await departmentsService.GetDepartmentsAsync();

                    // Update the UI
                    Departments.Clear();
                    foreach (var department in departments)
                        Departments.Add(department);

                    // Update local storage with the latest data
                    await _localDatabase.SaveDepartmentsAsync(departments);
                }

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
        private void EnableUpdate()
        {
            IsEditing = true;
        }

        [RelayCommand]
        private void CancelUpdate()
        {
            IsEditing = false;
            UpdatedPerson = null;
            UpdatedPerson = new UpdatedPersonModel();
        }

        [RelayCommand]
        private async Task UpdatePerson(PeopleModel person)
        {
            // TODO : Better sanitise and implement person update
            if (IsBusy)
                return;

            try
            {
                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No Connectivity", "You cannot update contacts at the moment.", "OK");
                    return;
                }

                IsBusy = true;

                Type updatedPersonType = UpdatedPerson.GetType();
                Type personType = person.GetType();

                var areEqual = personType.Equals(updatedPersonType);
                if (areEqual)
                {
                    UpdatedPerson.Id = Person.Id;
                    UpdatedPerson.DepartmentId = (SelectedIndex > 0) ? SelectedIndex : Person.DepartmentId;
                    MergeObjects(UpdatedPerson, Person);
                    // REMOVE: This
                    await Shell.Current.DisplayAlert("Same", "Same", "OK");
                }

                await peopleService.UpdatePersonAsync(Person);

                // REMOVE: This
                await Shell.Current.DisplayAlert("Updated",
                    $"Id: {Person.Id} - {UpdatedPerson.Id}\r\n" +
                    $"Name: {Person.Name} - {UpdatedPerson.Name}\r\n" +
                    $"Phone: {Person.Phone} - {UpdatedPerson.Phone}\r\n" +
                    $"DepartmentId: {Person.DepartmentId} - {UpdatedPerson.DepartmentId} \r\n" +
                    $"Street: {Person.Street} - {UpdatedPerson.Street}\r\n" +
                    $"Suburb: {Person.Suburb} - {UpdatedPerson.Suburb}\r\n" +
                    $"State: {Person.State} - {UpdatedPerson.State}\r\n" +
                    $"Postcode: {Person.Postcode} - {UpdatedPerson.Postcode}\r\n" +
                    $"Country: {Person.Country} - {UpdatedPerson.Country}\r\n" +
                    $"SelectedIndex: {SelectedIndex} - SelectedItem: {SelectedItem}", "OK");

                IsEditing = false;
                UpdatedPerson = null;
                UpdatedPerson = new UpdatedPersonModel();

                await Shell.Current.DisplayAlert("Updated", $"{Person.Name} updated successfully.", "OK");

                var page = Shell.Current.Navigation.NavigationStack.FirstOrDefault(p => p is ContactDetailsPage);
                if (page != null)
                    Shell.Current.Navigation.RemovePage(page);

                await Shell.Current.GoToAsync(nameof(ContactDetailsPage), true, new Dictionary<string, object>
                {
                    {"Person", Person}
                });

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

        private static void MergeObjects(object sourceObject, object destinationObject)
        {
            var sourceProperties = sourceObject.GetType().GetProperties();
            var destinationProperties = destinationObject.GetType().GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                var destinationProperty = destinationProperties.FirstOrDefault(p => p.Name == sourceProperty.Name);

                if (destinationProperty != null)
                {
                    var sourceValue = sourceProperty.GetValue(sourceObject);
                    var destinationValue = destinationProperty.GetValue(destinationObject);

                    // Ignore null or empty source values and values that match destination
                    if (sourceValue != null && !string.IsNullOrEmpty(sourceValue.ToString()) && !sourceValue.Equals(destinationValue))
                    {
                        destinationProperty.SetValue(destinationObject, sourceValue);
                    }
                }
            }
        }
    }
}