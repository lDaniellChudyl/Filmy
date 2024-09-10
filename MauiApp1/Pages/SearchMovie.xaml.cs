using System.ComponentModel;
using MauiApp1.ViewModels;

namespace MauiApp1.Pages;

public partial class SearchMovie : ContentPage
{

	public SearchMovie(string query)
    {
        BindingContext = new SearchMovieViewModel(query);
		InitializeComponent();
	}
}