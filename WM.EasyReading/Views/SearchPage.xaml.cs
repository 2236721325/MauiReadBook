using CommunityToolkit.Mvvm.DependencyInjection;
using WM.EasyReading.ViewModels;

namespace WM.EasyReading.Views;

public partial class SearchPage : ContentPage
{
	private SearchViewModel _vm;
    public SearchPage(SearchViewModel vm)
    {
       

        InitializeComponent();
        BindingContext = vm;
        _vm = vm;


    }



    private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        await _vm.OnBookTapped(e.ItemIndex);

    }
}