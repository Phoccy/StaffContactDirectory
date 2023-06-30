using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace StaffContactDirectory.Service;

public class PeopleService
{
    private readonly RemoteDatabase _remoteDatabase;

    public PeopleService()
    {
        _remoteDatabase = new RemoteDatabase();
    }

    public async Task<List<PeopleModel>> GetPeopleAsync()
    {
        return await _remoteDatabase.People.Include(p => p.Departments).ToListAsync();
    }

    public async Task<PeopleModel> GetPersonByIdAsync(int id)
    {
        return await _remoteDatabase.People.FindAsync(id);
    }

    public async Task AddPersonAsync(PeopleModel person)
    {
        _remoteDatabase.People.Add(person);
        await _remoteDatabase.SaveChangesAsync();
    }

    public async Task UpdatePersonAsync(PeopleModel person)
    {
        _remoteDatabase.People.Update(person);
        await _remoteDatabase.SaveChangesAsync();
    }

    public async Task DeletePersonAsync(PeopleModel person)
    {
        _remoteDatabase.People.Remove(person);
        await _remoteDatabase.SaveChangesAsync();
    }
}