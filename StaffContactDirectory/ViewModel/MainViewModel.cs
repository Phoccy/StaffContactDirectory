namespace StaffContactDirectory.ViewModel
{
    public partial class MainViewModel : BaseViewModel
    {
        public string CompanyLogo { get; } = "roi_logo.png";
        public string CompanyName { get; } = "Red Opal Innovations";
        public string AppTitle { get; } = AppInfo.Name;

        public MainViewModel()
        {

        }
    }
}
