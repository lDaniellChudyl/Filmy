using System.Collections;
using MauiApp1.Models;
using MauiApp1.ViewModels;
using Microsoft.Maui.Controls;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        private int _currentIndex = 0;
        private readonly TimeSpan _autoSwipeInterval = TimeSpan.FromSeconds(5);

        public MainPage()
        {
            InitializeComponent();
        }

        private void InputView_OnTextChanged(object? sender, TextChangedEventArgs e)
        {
            var ViewModel = BindingContext as MainPageViewModel;
            SearchBar searchBar = (SearchBar)sender;
            if (ViewModel != null)
            {
                ViewModel.SearchText = searchBar.Text;
            }
        }
    }

}
