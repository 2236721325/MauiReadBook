using CommunityToolkit.Mvvm.DependencyInjection;
using WM.EasyReading.ViewModels;

namespace WM.EasyReading.Views;

public partial class BookShelfPage : ContentPage
{
	private BookShelfViewModel _vm;
    public BookShelfPage(BookShelfViewModel vm)
    {

        InitializeComponent();
        BindingContext = vm;
        _vm = vm;



    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await _vm.InitBooks();
    }
    private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
		await	_vm.OnBookTaped(e.ItemIndex);
    }
}