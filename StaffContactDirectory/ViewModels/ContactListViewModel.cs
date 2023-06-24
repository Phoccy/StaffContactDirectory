using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaffContactDirectory.Views;
using StaffContactDirectory.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Collections.ObjectModel;

namespace StaffContactDirectory.ViewModels;

public class ContactListViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    private List<PeopleModel> _people;
    private List<DepartmentsModel> _departments;

    public List<PeopleModel> People
    {
        get { return _people; }
        set
        {
            _people = value;
            OnPropertyChanged(nameof(People));
        }
    }

    public List<DepartmentsModel> Departments
    {
        get { return _departments; }
        set
        {
            _departments = value;
            OnPropertyChanged(nameof(Departments));
        }
    }

    public IList<DepartmentsModel> DepartmentList
    {
        get { return _departments; }
        set
        {
            _departments = (List<DepartmentsModel>)value;
            OnPropertyChanged(nameof(Departments));
        }
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public ContactListViewModel()
    {
        ReadDepartmentsFile();
        ReadPeopleFile();
    }

    private async void ReadDepartmentsFile()
    {
        _departments = new List<DepartmentsModel>();
        var csvFile = "Departments.csv";
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
        };
        using Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync(csvFile);
        using StreamReader reader = new StreamReader(fileStream);
        using CsvReader csv = new CsvReader(reader, configuration);
        _departments = csv.GetRecords<DepartmentsModel>().ToList();
    }

    private async void ReadPeopleFile()
    {
        _people = new List<PeopleModel>();
        var csvFile = "People.csv";
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
        };
        using (Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync(csvFile))
        using (StreamReader reader = new StreamReader(fileStream))
        using (CsvReader csv = new CsvReader(reader, configuration))
        {
            _people = csv.GetRecords<PeopleModel>().ToList();
            // Map department names based on DepartmentId
            foreach (var person in _people)
            {
                var department = _departments.FirstOrDefault(d => d.Id == person.DepartmentId);
                if (department != null)
                {
                    person.Department = department;
                }
            }
        }
    }
}
