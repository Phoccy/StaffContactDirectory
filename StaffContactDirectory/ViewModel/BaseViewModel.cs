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

    }

}
