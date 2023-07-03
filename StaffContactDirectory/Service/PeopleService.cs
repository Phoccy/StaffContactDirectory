UpdatePersonAsync(PeopleModel person)
    {
        _remoteDatabase.People.Update(person);
        await _remoteDatabase.SaveChangesAsync();
    }

    public async Task DeletePersonAsync(PeopleModel person)
    {
        _remoteDatabase.People.Remove(person);
        await _remoteDatabase.SaveChangesAsync();
    }

    public void Dispose()
    {
        _remoteDatabase.Dispose();
    }
}
