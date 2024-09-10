namespace MauiApp1.Models;

public class MovieRestResponseModel
{
    public int page { get; set; }
    public List<MovieModel> results { get; set; }
    public int total_pages { get; set; }
    public int total_results { get; set; }
}

