using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using WM.EasyReading.DB.Databases;
using WM.EasyReading.DB.Models;
using WM.EasyReading.Messages;
using WM.EasyReading.Service.Dtos;
using WM.EasyReading.Service.Services;
namespace WM.EasyReading.ViewModels;

public partial class BookReadViewModel : ObservableObject,IQueryAttributable
{
    private ISuperSearch _SuperSearch;
    private MyDatabase _db;

    [ObservableProperty]
    private BookChapterDto _chapterDetail;

    [ObservableProperty]
    private bool _visible=false;

    [ObservableProperty]
    private BookDetailDto _book;

   
    private int _currentIndex;

    public BookReadViewModel(MyDatabase db, ISuperSearch superSearch)
    {
        _db = db;
        _SuperSearch = superSearch;
    }

    public async void  ApplyQueryAttributes(IDictionary<string, object> query)
    {

        _book = query["currentBook"] as BookDetailDto;
        _currentIndex =(int) query["index"];
        ChapterDetail=  await _SuperSearch.SearchChapterContent(_book.BookChapters[_currentIndex]);

        WeakReferenceMessenger.Default.Register<OnReadBookPageDisappearing>(this, async (r, m) =>
        {
            await StoreLastReadChapter();
        });
    }
  
    private async Task  StoreLastReadChapter()
    {
        var author = _book.Simples.Author;
        var title = _book.Simples.Title;
        var book = await _db.Database.Table<StoreBook>()
            .Where(e => e.Author == author &&
            e.Title == title).FirstOrDefaultAsync();
        if (book == null)
        {
            return;
        }
        book.LastReadChapterIndex = _currentIndex;
        book.LastReadChapter = _chapterDetail.Name;
        book.LastReadChapterUrl = _chapterDetail.Url;
      
        await _db.Database.UpdateAsync(book);
    }
    //[RelayCommand]
    //private async void Swiped(SwipeGestureRecognizer swipe)
    //{
    //    switch (swipe.Direction)
    //    {
    //        case SwipeDirection.Right:
    //            await NextChapter();
    //            return;
    //        case SwipeDirection.Left:
    //            await LastChapter();
    //            return;

    //        default:
    //            return ;
    //    }
    //}

    [RelayCommand]
    private  void Tap()
    {
        Visible = !Visible;
    }
    [RelayCommand]
    private async void NextChapter(ScrollView scroll)
    {
        if (_currentIndex == _book.BookChapters.Count - 1)
        {
            return;
        }
        ChapterDetail = await _SuperSearch.SearchChapterContent(_book.BookChapters[_currentIndex +1]);
        _currentIndex++;
        await scroll.ScrollToAsync(0, 0, true);
        return;
    }
    [RelayCommand]
    private async void LastChapter(ScrollView scroll)
    {
        if (_currentIndex ==0)
        {
            return;
        }
        ChapterDetail = await _SuperSearch.SearchChapterContent(_book.BookChapters[_currentIndex -1]);
        _currentIndex--;
        await scroll.ScrollToAsync(0, 0, true);

        return;
    }
}
