using System.Windows;
using Travel_Company.WPF.Core;
using Travel_Company.WPF.Core.Enums;
using Travel_Company.WPF.Data.Dto;
using Travel_Company.WPF.MVVM.ViewModel.Catalogs;
using Travel_Company.WPF.MVVM.ViewModel.Clients;
using Travel_Company.WPF.MVVM.ViewModel.Employees;
using Travel_Company.WPF.MVVM.ViewModel.Groups;
using Travel_Company.WPF.MVVM.ViewModel.Penalties;
using Travel_Company.WPF.MVVM.ViewModel.Routes;
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

    // Pages
    public RelayCommand NavigateToEmployeesCommand { get; set; }
    public RelayCommand NavigateToClientsCommand { get; set; }
    public RelayCommand NavigateToPenaltiesCommand { get; set; }
    public RelayCommand NavigateToTouristGroupsCommand { get; set; }
    public RelayCommand NavigateToRoutesCommand { get; set; }

    // Catalogs
    public RelayCommand NavigateToCountriesCommand { get; set; }
    public RelayCommand NavigateToStreetsCommand { get; set; }
    public RelayCommand NavigateToHotelsCommand { get; set; }
    public RelayCommand NavigateToPopulatedPlacesCommand { get; set; }

    public MainViewModel(INavigationService service)
    {
        Navigation = service;
        //MainMenuVisibility = Visibility.Collapsed;

        InitializePagesCommands();
        InitializeCatalogsCommands();
    }

    private void InitializePagesCommands()
    {
        NavigateToEmployeesCommand = new RelayCommand(
            execute: _ => Navigation.NavigateTo<EmployeesViewModel>(),
            canExecute: _ => true);
        NavigateToClientsCommand = new RelayCommand(
            execute: _ => Navigation.NavigateTo<ClientsViewModel>(),
            canExecute: _ => true);
        NavigateToPenaltiesCommand = new RelayCommand(
            execute: _ => Navigation.NavigateTo<PenaltiesViewModel>(),
            canExecute: _ => true);
        NavigateToTouristGroupsCommand = new RelayCommand(
            execute: _ => Navigation.NavigateTo<GroupsViewModel>(),
            canExecute: _ => true);
        NavigateToRoutesCommand = new RelayCommand(
            execute: _ => Navigation.NavigateTo<RoutesViewModel>(),
            canExecute: _ => true);
    }

    private void InitializeCatalogsCommands()
    {
        NavigateToCountriesCommand = CreateNavigationCommand(CatalogType.Country);
        NavigateToStreetsCommand = CreateNavigationCommand(CatalogType.Street);
        NavigateToHotelsCommand = CreateNavigationCommand(CatalogType.Hotel);
        NavigateToPopulatedPlacesCommand = CreateNavigationCommand(CatalogType.Place);
    }

    private RelayCommand CreateNavigationCommand(CatalogType catalogType)
    {
        return new RelayCommand(
            execute: _ =>
            {
                var message = new CatalogTypeMessage { CatalogType = catalogType };
                App.EventAggregator.Publish(message);
                Navigation.NavigateTo<CatalogsViewModel>();
            },
            canExecute: _ => true);
    }
}