namespace StaffContactDirectory.Database
{

    public class LocalDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public LocalDatabase(string databasePath)
        {
            _database = new SQLiteAsyncConnection(databasePath);
            _database.CreateTableAsync<PeopleModel>().Wait();
            _database.CreateTableAsync<DepartmentsModel>().Wait();            
        }

        public async Task<List<PeopleModel>> GetPeopleAsync()
        {
            var people = await _database.Table<PeopleModel>().ToListAsync();
            foreach (var person in people)
            {
                person.Departments = await _database.FindAsync<DepartmentsModel>(person.DepartmentId);
            }
            return people;
        }


        public async Task<int> SavePeopleAsync(List<PeopleModel> people)
        {
            await _database.DeleteAllAsync<PeopleModel>();
            return await _database.InsertAllAsync(people);
        }

        public async Task<List<DepartmentsModel>> GetDepartmentsAsync()
        {
            return await _database.Table<DepartmentsModel>().ToListAsync();
        }

        public async Task<int> SaveDepartmentsAsync(List<DepartmentsModel> departments)
        {
            await _database.DeleteAllAsync<DepartmentsModel>();
            return await _database.InsertAllAsync(departments);
        }
    }
}
