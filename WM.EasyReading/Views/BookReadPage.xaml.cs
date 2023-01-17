using CommunityToolkit.Mvvm.Messaging;
using WM.EasyReading.Messages;
using WM.EasyReading.ViewModels;

namespace WM.EasyReading.Views;

public partial class BookReadPage : ContentPage
{
	private BookReadViewModel _vm;
    public BookReadPage(BookReadViewModel vm)
    {
      
        InitializeComponent();
        BindingContext = vm;
        _vm = vm;


    }
    protected override  void OnDisappearing()
    {
        base.OnDisappearing();
		WeakReferenceMessenger.Default.Send<OnReadBookPageDisappearing>(new OnReadBookPageDisappearing(true));

    }

}