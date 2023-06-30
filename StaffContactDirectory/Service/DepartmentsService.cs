namespace StaffContactDirectory.Service;

public class DepartmentsService
{
    private readonly RemoteDatabase _remoteDatabase;

    public DepartmentsService()
    {
        _remoteDatabase = new RemoteDatabase();
    }

    public async Task<List<DepartmentsModel>> GetDepartmentsAsync()
    {
        return await _remoteDatabase.Departments.ToListAsync();
    }
}