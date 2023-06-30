
namespace StaffContactDirectory.ViewModel
{
    public partial class AboutViewModel : BaseViewModel
    {
        public string AboutInfo { get; } = "I'm just a simple staff contact directory";
        public string Copyright { get; } = "Copyright 2023 - Adam Masters";
        public string VersionString { get; } = AppInfo.VersionString;
        public Version Version { get; } = AppInfo.Version;

        //IConnectivity connectivity;

        //public AboutViewModel(IConnectivity connectivity)
        //{
        //    this.connectivity = connectivity;
        //    if (connectivity.NetworkAccess == NetworkAccess.Internet)
        //    {
        //        Shell.Current.DisplayAlert("Connected", "You got internet", "OK");
        //        return;
        //    }

        //}
        
    }
}
