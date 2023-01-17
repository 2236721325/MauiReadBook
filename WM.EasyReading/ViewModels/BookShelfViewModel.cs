using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.EasyReading.DB.Databases;
using WM.EasyReading.DB.Models;
using WM.EasyReading.Service.Dtos;
using WM.EasyReading.Views;

namespace WM.EasyReading.ViewModels
{
    public partial class BookShelfViewModel:ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<StoreBook> _storeBooks;

        private readonly MyDatabase _db;

        public BookShelfViewModel(MyDatabase db)
        {
            _db = db;
        }
        public async Task InitBooks()
        {
            StoreBooks = new ObservableCollection<StoreBook>(
               await _db.Database.Table<StoreBook>().ToListAsync()
              );
        }

        public async Task OnBookTaped(int index)
        {
            var navigationParameter = new Dictionary<string, object>()
            {
                {"book",new BookSimpleDto(_storeBooks[index])}
            };

            await Shell.Current.GoToAsync(nameof(BookDetailPage), navigationParameter);
        }
    }

}
