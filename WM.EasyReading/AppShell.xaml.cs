using WM.EasyReading.Views;

namespace WM.EasyReading;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(BookDetailPage), typeof(BookDetailPage));
        Routing.RegisterRoute(nameof(SearchPage), typeof(SearchPage));
        Routing.RegisterRoute(nameof(BookShelfPage), typeof(BookShelfPage));
        Routing.RegisterRoute(nameof(BookReadPage), typeof(BookReadPage));




    }
}
