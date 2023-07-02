namespace StaffContactDirectory.ViewModel
{
    public partial class SettingsViewModel : BaseViewModel
    {
        private double _textSize;
        private double _brightness;
        private bool _enableSoundEffects;

        public double TextSize
        {
            get => _textSize;
            set
            {
                _textSize = value;
                OnPropertyChanged();

                // Apply the text size to the application or specific controls
                // For example, you can use Xamarin.Forms Application.Current.MainPage or modify specific controls
            }
        }

        public double Brightness
        {
            get => _brightness;
            set
            {
                _brightness = value;
                OnPropertyChanged();

                // Apply the brightness setting using Xamarin.Essentials or platform-specific APIs
                //Xamarin.Essentials.Brightness.SetBrightness(value);
            }
        }

        public bool EnableSoundEffects
        {
            get => _enableSoundEffects;
            set
            {
                _enableSoundEffects = value;
                OnPropertyChanged();

                // Apply the sound effects setting to your application logic
            }
        }

        public SettingsViewModel()
        {
            // Initialize default values for the settings
            TextSize = Preferences.Get(nameof(TextSize), 14);
            Brightness = Preferences.Get(nameof(Brightness), 1.0);
            EnableSoundEffects = Preferences.Get(nameof(EnableSoundEffects), true);
            SaveSettingsCommand = new Command(SaveSettings);
        }

        public ICommand SaveSettingsCommand { get; }

        private void SaveSettings()
        {
            Preferences.Set(nameof(TextSize), TextSize);
            Preferences.Set(nameof(Brightness), Brightness);
            Preferences.Set(nameof(EnableSoundEffects), EnableSoundEffects);
        }
    }
}