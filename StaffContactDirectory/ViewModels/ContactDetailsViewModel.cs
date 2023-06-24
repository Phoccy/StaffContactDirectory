using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using StaffContactDirectory.Models;


namespace StaffContactDirectory.ViewModels;

public class ContactDetailsViewModel : BindableObject
{
    private PeopleModel _selectedPerson;
    private bool _isEditing;

    public ContactDetailsViewModel()
    {
        // Initialise the selected person
        _selectedPerson = new PeopleModel();
        _isEditing = false; // Set initial edit mode state

        EditCommand = new Command(ExecuteEditCommand);
        UpdateCommand = new Command(ExecuteUpdateCommand, CanExecuteUpdateCommand);
    }
    public PeopleModel SelectedPerson
    {
        get { return _selectedPerson; }
        set {
            _selectedPerson = value;
            OnPropertyChanged();
        }

    }

    public bool IsEditing
    {
        get { return _isEditing; }
        set
        {
            if (_isEditing != value)
            {
                _isEditing = value;
                OnPropertyChanged(nameof(IsEditing));
                OnPropertyChanged(nameof(IsNotEditing));
            }
        }
    }

    public bool IsNotEditing => !_isEditing;

    public ICommand EditCommand { get; }

    private void ExecuteEditCommand()
    {
        IsEditing = true;
    }

    public ICommand UpdateCommand { get; }

    private bool CanExecuteUpdateCommand()
    {
        return true;
    }

    private void ExecuteUpdateCommand()
    {
        IsEditing = false;
    }
}
