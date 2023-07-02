namespace StaffContactDirectory;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("Trebuchet-MS.ttf", "TrebuchetRegular");
                fonts.AddFont("Trebuchet-MS-Bold.ttf", "TrebuchetBold");
                fonts.AddFont("Trebuchet-MS-Italiac.ttf", "TrebuchetItaliac");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);

        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<MainPage>();

        builder.Services.AddSingleton<AboutViewModel>();
        builder.Services.AddSingleton<AboutPage>();

        builder.Services.AddTransient<RemoteDatabase>();
        builder.Services.AddSingleton<PeopleService>();
        builder.Services.AddSingleton<ContactListViewModel>();
        builder.Services.AddTransient<ContactListPage>();

        builder.Services.AddSingleton<DepartmentsService>();
        builder.Services.AddSingleton<ContactDetailsViewModel>();
        builder.Services.AddTransient<ContactDetailsPage>();

        builder.Services.AddSingleton<AddContactViewModel>();
        builder.Services.AddSingleton<AddContactPage>();

        builder.Services.AddSingleton<SettingsViewModel>();
        builder.Services.AddSingleton<SettingsPage>();

        return builder.Build();
    }
}
