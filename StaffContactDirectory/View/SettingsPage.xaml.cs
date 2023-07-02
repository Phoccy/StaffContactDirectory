namespace StaffContactDirectory.View
{

    public partial class SettingsPage : ContentPage
    {
        private SettingsViewModel viewModel;

        public SettingsPage(SettingsViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            BindingContext = this.viewModel;
        }

        //private void OnTextSizeSliderValueChanged(object sender, ValueChangedEventArgs e)
        //{
        //    viewModel.SetTextSize((int)e.NewValue);
        //}

        //private void OnBrightnessSliderValueChanged(object sender, ValueChangedEventArgs e)
        //{
        //    viewModel.SetBrightness((int)e.NewValue);
        //}

        //private void OnSoundEffectsSwitchToggled(object sender, ToggledEventArgs e)
        //{
        //    viewModel.EnableSoundEffects(e.Value);
        //}
    }
}