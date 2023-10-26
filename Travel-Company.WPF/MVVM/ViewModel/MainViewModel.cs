using System.Windows;
using Travel_Company.WPF.Core;
using Travel_Company.WPF.MVVM.ViewModel.Clients;
using Travel_Company.WPF.MVVM.ViewModel.Employees;
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

    public RelayCommand NavigateToEmployeesCommand { get; set; }
    public RelayCommand NavigateToClientsCommand { get; set; }

    public MainViewModel(INavigationService service)
    {
        Navigation = service;
        //MainMenuVisibility = Visibility.Collapsed;

        NavigateToEmployeesCommand = new RelayCommand(
            execute: _ => Navigation.NavigateTo<EmployeesViewModel>(),
            canExecute: _ => true);
        NavigateToClientsCommand = new RelayCommand(
            execute: _ => Navigation.NavigateTo<ClientsViewModel>(),
            canExecute: _ => true);
    }
}