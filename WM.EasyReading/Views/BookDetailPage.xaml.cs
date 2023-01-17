using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using WM.EasyReading.DB.Databases;
using WM.EasyReading.DB.Models;
using WM.EasyReading.Messages;
using WM.EasyReading.Service.Dtos;
using WM.EasyReading.ViewModels;

namespace WM.EasyReading.Views;

public partial class BookDetailPage : ContentPage
{
    private readonly BookDetailViewModel _vm;
    private MyDatabase _db;
    public BookDetailPage(BookDetailViewModel vm, MyDatabase db)
    {
       

        InitializeComponent();
        BindingContext = vm;
        _vm = vm;





        // Register a message in some module
        WeakReferenceMessenger.Default.Register<GetStoreIndexMessage>(this, async (r, m) =>
        {
            await GetStoreMessage(m.Value);
        });
        _db = db;
    }

    public async Task GetStoreMessage(BookDetailDto dto)
    {
        var title = dto.Simples.Title;
        var author = dto.Simples.Author;
        var bookStore = await _db.Database.Table<StoreBook>()
         .Where(b => b.Title == title &&
         b.Author == author).FirstOrDefaultAsync();

        if (bookStore != null)
        {
            listView.SelectedItem = dto.BookChapters[bookStore.LastReadChapterIndex];
            listView.ScrollTo(dto.BookChapters[bookStore.LastReadChapterIndex], ScrollToPosition.Center, true);
        }
    }




    private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        await _vm.GotoRead(e.ItemIndex);
    }
}