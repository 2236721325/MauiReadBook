using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using WM.EasyReading.DB.Databases;
using WM.EasyReading.DB.Models;
using WM.EasyReading.Messages;
using WM.EasyReading.Service.Dtos;
using WM.EasyReading.Service.Services;
using WM.EasyReading.Views;

namespace WM.EasyReading.ViewModels
{
    public partial class BookDetailViewModel : ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        private BookDetailDto currentBook;

        private ISuperSearch _SuperSearch;
        private MyDatabase _db;

        public BookDetailViewModel(ISuperSearch superSearch, MyDatabase db)
        {
            _SuperSearch = superSearch;
            _db = db;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {

            var book = query["book"] as BookSimpleDto;

            CurrentBook = await _SuperSearch.SearchDetail(book);

            WeakReferenceMessenger.Default.Send(new GetStoreIndexMessage(CurrentBook));
        }

      
        [RelayCommand]
        private  async void AddBookShelf()
        {
            var author = currentBook.Simples.Author;
            var title = currentBook.Simples.Title;
            var book=await _db.Database.Table<StoreBook>()
                .Where(e => e.Author == author &&
                e.Title == title).FirstOrDefaultAsync();
            if (book != null)
            {
                await Shell.Current.CurrentPage.DisplayAlert("已加入书架", "不要重复加", "了解");
                return;
            }
            var bookStore = new StoreBook()
            {
                Author = author,
                Title = title,
                Url = currentBook.Simples.Url,
                LastReadChapter = currentBook.BookChapters[0].Name,
                LastReadChapterIndex = 0,
                LastReadChapterUrl = currentBook.BookChapters[0].Url,
            };
            await _db.Database.InsertAsync(bookStore);

            await Shell.Current.CurrentPage.DisplayAlert("加入书架成功", "哈哈哈哈哈", "了解");

        }



        public async Task GotoRead(int index)
        {
            var navigationParameter = new Dictionary<string, object>()
            {
                {"currentBook",currentBook},
                {"index",index }
            };
            await Shell.Current.GoToAsync(nameof(BookReadPage), navigationParameter);
        }
    }
}
