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
            TextSize = Preferences.Get(nameof(TextSize), 14);
            Brightness = Preferences.Get(nameof(Brightness), 1.0);
            EnableSoundEffects = Preferences.Get(nameof(EnableSoundEffects), true);
        }

        protected override void OnStart()
        {
            InitializeSettings();
        }
    }
}