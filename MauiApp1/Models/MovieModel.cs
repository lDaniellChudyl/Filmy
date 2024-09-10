namespace MauiApp1.Models;

public class MovieModel
{
    public string id { get; set; }
    public string backdrop_path { get; set; }
    public string title { get; set; }
    public string overview { get; set; }
    public string media_type { get; set; }
    public string adult { get; set; }
    public List<int> genre_ids { get; set; }
    public List<GenreModel> genres { get; set; } = new List<GenreModel>();
    public double popularity { get; set; }
    public string release_date { get; set; }
    public double vote_average { get; set; }
    public int vote_count { get; set; }

}