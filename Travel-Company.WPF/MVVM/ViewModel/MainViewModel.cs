using System.Windows;
using Travel_Company.WPF.Core;
using Travel_Company.WPF.Services.Navigation;

namespace Travel_Company.WPF.MVVM.ViewModel;

public sealed class MainViewModel : Core.ViewModel
{
    private INavigationService _navigation = null!;
    public INavigationService Navigation
    {
        get => _navigation;
        set
        {
            _navigation = value;
            OnPropertyChanged();
        }
    }

    private Visibility _mainMenuVisibility;
    public Visibility MainMenuVisibility
    {
        get { return _mainMenuVisibility; }
        set
        {
            if (value != _mainMenuVisibility)
            {
                _mainMenuVisibility = value;
                OnPropertyChanged(nameof(MainMenuVisibility));
            }
        }
    }

    // TODO: Navigate commands
    public RelayCommand NavigateToLoginCommand { get; set; }
    // etc. etc.

    public MainViewModel(INavigationService service)
    {
        Navigation = service;
        //MainMenuVisibility = Visibility.Collapsed;

        // TODO: Add Commands for navigation
        //NavigateToHomeCommand = new RelayCommand(
        //    execute: _ => Navigation.NavigateTo<LoginViewModel>(),
        //    canExecute: _ => true);
        // etc. etc.
    }
}
