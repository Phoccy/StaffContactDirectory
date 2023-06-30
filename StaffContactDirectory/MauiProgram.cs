using Microsoft.Extensions.Logging;
using StaffContactDirectory.ViewModel;
using StaffContactDirectory.View;

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

        builder.Services.AddSingleton<PeopleService>(); 
        builder.Services.AddSingleton<ContactListViewModel>();
        builder.Services.AddSingleton<ContactListPage>();

        return builder.Build();
	}
}
