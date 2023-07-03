using System.Reflection;

namespace StaffContactDirectory.ViewModel
{
    public partial class AddContactViewModel : BaseViewModel
    {
        public ObservableCollection<DepartmentsModel> Departments { get; } = new ObservableCollection<DepartmentsModel>();

        DepartmentsService departmentsService;
        PeopleService peopleService;
        IConnectivity connectivity;

        [ObservableProperty]
        PeopleModel person;
        [ObservableProperty]
        bool isRefreshing;
        [ObservableProperty]
        int selectedIndex;
        [ObservableProperty]
        object selectedItem;

        public AddContactViewModel(DepartmentsService departmentsService, PeopleService peopleService, IConnectivity connectivity)
        {
            Person = new PeopleModel();
            this.departmentsService = departmentsService;
            this.peopleService = peopleService;
            this.connectivity = connectivity;
        }

        [RelayCommand]
        async Task AddPerson(PeopleModel person)
        {            
            if (person == null)
                return;

            if (await SanitisePerson(person))
            {

                if (IsBusy)
                    return;

                try
                {
                    if (connectivity.NetworkAccess != NetworkAccess.Internet)
                    {
                        await Shell.Current.DisplayAlert("No Connectivity", "You cannot add any new contacts at the moment.", "OK");
                        return;
                    }

                    IsBusy = true;

                    await peopleService.AddPersonAsync(person);

                    await Shell.Current.DisplayAlert("Success", $"{person.Name} has been added to your contact list.", "OK");

                    //Person = null;
                    //Person = new PeopleModel();
                    await Shell.Current.Navigation.PopAsync();

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
        }

        async Task<bool> SanitisePerson(PeopleModel person)
        {
            bool result = true;

            if (person.GetType().GetProperty("ImageSrc").CanWrite)
                person.GetType().GetProperty("ImageSrc").SetValue(person, "placeholder_male.png");

            PropertyInfo[] properties = typeof(PeopleModel).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType == typeof(string))
                {
                    string value = (string)property.GetValue(person);

                    string trimmedValue = value?.Trim();
                    property.SetValue(person, trimmedValue);

                    if (string.IsNullOrEmpty(trimmedValue))
                    {
                        await Shell.Current.DisplayAlert("Whoops!", $"You must provide a value for {property.Name}", "OK");
                        result = false;
                    }
                }
            }

            if (person.GetType().GetProperty("Phone") != null)
            {
                string phone = person.GetType().GetProperty("Phone").GetValue(person)?.ToString();

                if (!string.IsNullOrEmpty(phone) && phone.Length < 10)
                {
                    await Shell.Current.DisplayAlert("Whoops!", "Phone must be 10 digits long", "OK");
                    result = false;
                }
            }

            if (person.GetType().GetProperty("DepartmentId") != null)
            {
                object departmentIdObj = person.GetType().GetProperty("DepartmentId").GetValue(person);

                if (departmentIdObj == null || !int.TryParse(departmentIdObj.ToString(), out int departmentId) || departmentId == 0)
                {
                    await Shell.Current.DisplayAlert("Whoops!", "Please select a Department", "OK");
                    result = false;
                }
            }

            return result;
        }


        [RelayCommand]
        private async Task CancelAdd()
        {
            //Person = null;
            //Person = new PeopleModel();
            await Shell.Current.Navigation.PopAsync();
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
    }
}