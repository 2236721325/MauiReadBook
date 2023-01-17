using WM.EasyReading.Services;
using WM.EasyReading.ViewModels;
using WM.EasyReading.DB.Databases;
using WM.EasyReading.Service.Services;
using WM.EasyReading.Views;

namespace WM.EasyReading;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

			builder.ConfigureServices();

		return builder.Build();
	}
	public static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
	{
		builder.Services.AddTransient<BookShelfViewModel>();
        builder.Services.AddTransient<SearchViewModel>();
        builder.Services.AddTransient<BookDetailViewModel>();
		builder.Services.AddTransient<BookReadViewModel>();

		builder.Services.AddSingleton<BookDetailPage>();
		builder.Services.AddTransient<SearchPage>();
		builder.Services.AddTransient<BookReadPage>();
		builder.Services.AddTransient<BookShelfPage>();




		builder.Services.AddSingleton<MyDatabase>();
		builder.Services.AddTransient<ISuperSearch, SuperSearch>();
        return builder;
	}
}
