using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MauiApp1.Controllers;
using MauiApp1.Models;

namespace MauiApp1.ViewModels;

public class SearchMovieViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public string SearchQuery { get; private set; }
    protected int page { get; private set; } = 1;
    protected int maxPage { get; private set; }
    private List<GenreModel> Genres { get; set; }
    public ObservableCollection<MovieModel> Movies { get; set; }
    private bool LoadMoreVisible { get; set; }

    public bool ObLoadMoreVisible {
        get
        {
            return LoadMoreVisible;
        }
        set
        {
            if (maxPage >= page)
            {
                LoadMoreVisible = false;
            }
            else
            {
                LoadMoreVisible = true;
            }

            OnPropertyChanged();
        }
    }

    public ICommand ShowMoreCommand { get; private set; }

    public SearchMovieViewModel(string searchQuery)
    {
        SearchQuery = searchQuery;
        LoadMoreVisible = true;
        ShowMoreCommand = new Command(OnShowMore);
        initMovies();
    }

    void OnShowMore() => GetMovies(SearchQuery);

    protected async void initMovies()
    {

        Movies = new ObservableCollection<MovieModel>();
        GetMovies(SearchQuery);

        Genres = new List<GenreModel>();
        await GetGenres();
    }

    private async Task GetGenres()
    {
        var result = await MoviesRestController.GetGenres();
        Genres.AddRange(Newtonsoft.Json.JsonConvert.DeserializeObject<GenreRestResponseModel>(result.Content).genres);

        foreach (var item in Movies)
        {
            item.genres.AddRange(Genres.FindAll(x => item.genre_ids.Contains(x.id)));
        }
    }

    private async void GetMovies(string query)
    {
        var result = await MoviesRestController.GetSearchMovie(SearchQuery,page);
        MovieRestResponseModel MovieResponse =
            Newtonsoft.Json.JsonConvert.DeserializeObject<MovieRestResponseModel>(result.Content);
        maxPage = MovieResponse.total_pages;
        foreach (var item in MovieResponse.results)
        {
            Movies.Add(item);
        }
        page++;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}