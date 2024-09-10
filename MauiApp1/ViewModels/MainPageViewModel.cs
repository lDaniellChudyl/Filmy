using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Windows.Input;
using MauiApp1.Controllers;
using MauiApp1.Models;
using MauiApp1.Pages;

namespace MauiApp1.ViewModels;

public class MainPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private List<MovieModel> PopularMovies { get; set; }
    private List<MovieModel> TrendingMovies { get; set; }
    private List<MovieModel> NowPlayingMovies { get; set; }
    private List<GenreModel> Genres { get; set; }
    

    public List<MovieModel> ObPopularMovies
    {
        get
        {
            return PopularMovies;
        }
        set
        {
            PopularMovies = value;
            OnPropertyChanged();
        }
    }

    public List<MovieModel> ObTrendingMovies
    {
        get
        {
            return TrendingMovies;
        }
        set
        {
            TrendingMovies = value;
            OnPropertyChanged();
        }
    }

    public List<MovieModel> ObNowPlayingMovies
    {
        get
        {
            return NowPlayingMovies;
        }
        set
        {
            NowPlayingMovies = value;
            OnPropertyChanged();
        }
    }

    public ICommand SearchCommand { get; }
    public string SearchText { get; set; }

    public MainPageViewModel()
    {
        initMovies();
        SearchCommand = new Command<string>(OnSearch);
    }

    void OnSearch(string query) => AppShell.Current.Navigation.PushAsync(new SearchMovie(SearchText));


    protected async void initMovies()
    {
        TrendingMovies = new List<MovieModel>();
        GetTrendingMovies();
        PopularMovies = new List<MovieModel>();
        GetPopularMovies();
        NowPlayingMovies = new List<MovieModel>();
        GetNowPlayingMovies();

        Genres = new List<GenreModel>();
        await GetGenres();

    }

    private async Task GetGenres()
    {
        var result = await MoviesRestController.GetGenres();
        Genres.AddRange(Newtonsoft.Json.JsonConvert.DeserializeObject<GenreRestResponseModel>(result.Content).genres);

        foreach (var item in PopularMovies)
        {
            item.genres.AddRange(Genres.FindAll(x => item.genre_ids.Contains(x.id)));
        }

        foreach (var item in TrendingMovies)
        {
            item.genres.AddRange(Genres.FindAll(x => item.genre_ids.Contains(x.id)));
        }

        foreach (var item in NowPlayingMovies)
        {
            item.genres.AddRange(Genres.FindAll(x => item.genre_ids.Contains(x.id)));
        }
    }

    private async void GetPopularMovies()
    {
        var result = await MoviesRestController.GetPopular(1);
        PopularMovies.AddRange(Newtonsoft.Json.JsonConvert.DeserializeObject<MovieRestResponseModel>(result.Content).results);
    }

    private async void GetTrendingMovies()
    {
        var result = await MoviesRestController.GetTrending(1);
        TrendingMovies.AddRange(Newtonsoft.Json.JsonConvert.DeserializeObject<MovieRestResponseModel>(result.Content).results);
    }

    private async void GetNowPlayingMovies()
    {
        var result = await MoviesRestController.GetNowPlaying(1);
        NowPlayingMovies.AddRange(Newtonsoft.Json.JsonConvert.DeserializeObject<MovieRestResponseModel>(result.Content).results);
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}