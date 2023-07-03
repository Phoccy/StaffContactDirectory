namespace StaffContactDirectory
{
    public partial class App : Application
    {
        public double TextSize { get; set; }
        public double Brightness { get; set; }
        public bool EnableSoundEffects { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        private void InitializeSettings()
        {
            TextSize = Microsoft.Maui.Storage.Preferences.Get(nameof(TextSize), 14);
            Brightness = Microsoft.Maui.Storage.Preferences.Get(nameof(Brightness), 1.0);
            EnableSoundEffects = Microsoft.Maui.Storage.Preferences.Get(nameof(EnableSoundEffects), true);
        }

        protected override void OnStart()
        {
            InitializeSettings();
        }
    }
}