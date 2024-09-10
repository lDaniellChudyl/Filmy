using RestSharp;

namespace MauiApp1.Controllers;

public class MoviesRestController
{

    protected static async Task<RestResponse> Call(string request, string endpoint)
    {
        var client = new RestClient(PrepareRestOptions(endpoint));
        return await client.GetAsync(MakeRequest(""));
    }

    protected static RestRequest MakeRequest(string request)
    {
        AppConfig config = new AppConfig();
        RestRequest _request = new RestRequest(request);
        _request.AddHeader("accept", "application/json");
        _request.AddHeader("Authorization", $"Bearer {config.auth}");

        return _request;
    }
    //https://image.tmdb.org/t/p/original/p5kpFS0P3lIwzwzHBOULQovNWyj.jpg.jpg

    protected static RestClientOptions PrepareRestOptions(string endpoint) =>
        new RestClientOptions($"https://api.themoviedb.org/3/{endpoint}");

    public static async Task<RestResponse> GetPopular(int page = 1)
    {
        return await Call("", "movie/popular?language=pl-PL&page=1");
    }

    public static async Task<RestResponse> GetTrending(int page = 1)
    {
        return await Call("", "trending/movie/week?language=pl-PL");
    }

    public static async Task<RestResponse> GetGenres()
    {
        return await Call("", "genre/movie/list?language=pl");
    }

    public static async Task<RestResponse> GetNowPlaying(int page = 1)
    {
        return await Call("", "movie/now_playing?language=pl-PL&page=1");
    }

    public static async Task<RestResponse> GetSearchMovie(string query,int page = 1)
    {
        return await Call("", $"search/movie?query={query}&include_adult=true&language=pl-PL&page={page}");
    }

}