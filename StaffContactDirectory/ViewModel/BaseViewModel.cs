namespace StaffContactDirectory.ViewModel
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotEditing))]
        bool isEditing;

        [ObservableProperty]
        bool _sVisible;

        [ObservableProperty]
        string title;

        public bool IsNotBusy => !IsBusy;
        public bool IsNotEditing => !IsEditing;

        protected LocalDatabase _localDatabase;
        
        public BaseViewModel()
        {
            string databasePath = Path.Combine(FileSystem.AppDataDirectory, "StaffContactDirectoryNew.db");
            _localDatabase = new LocalDatabase(databasePath);
        }

        protected async Task<List<PeopleModel>> FetchPeopleFromLocalStorage()
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                    return null; // If there is internet activity, return null to indicate no data in local storage

                // Fetch people data from the SQLite database
                List<PeopleModel> people = await _localDatabase.GetPeopleAsync();
                return people;
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
                Debug.WriteLine($"Failed to fetch people data from local storage: {ex.Message}");
                return null;
            }
        }

        protected async Task<List<DepartmentsModel>> FetchDepartmentsFromLocalStorage()
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                    return null; // If there is internet activity, return null to indicate no data in local storage

                // Fetch department data from the SQLite database
                List<DepartmentsModel> departments = await _localDatabase.GetDepartmentsAsync();
                return departments;
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
                Debug.WriteLine($"Failed to fetch department data from local storage: {ex.Message}");
                return null;
            }
        }

    }
}
