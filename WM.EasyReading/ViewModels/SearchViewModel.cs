using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.EasyReading.Service.Dtos;
using WM.EasyReading.Service.Services;
using WM.EasyReading.Views;

namespace WM.EasyReading.ViewModels
{
    public partial class SearchViewModel : ObservableObject
    {
        private readonly ISuperSearch _SuperSearch;
        public SearchViewModel(ISuperSearch superSearch)
        {
            _searchResults = new ObservableCollection<BookSimpleDto>();
            _SuperSearch = superSearch;
        }

        [ObservableProperty]
        private string _searchValue="";

        [ObservableProperty]
        private ObservableCollection<BookSimpleDto> _searchResults;

        [RelayCommand]
        private async void Search()
        {
            SearchResults = new ObservableCollection<BookSimpleDto>(await _SuperSearch.Search(_searchValue));
        }

        public async Task OnBookTapped(int index)
        {
            var navigationParameter = new Dictionary<string, object>()
            {
                {"book",SearchResults[index]}
            };

            await Shell.Current.GoToAsync(nameof(BookDetailPage), navigationParameter);
        }

    }
}
