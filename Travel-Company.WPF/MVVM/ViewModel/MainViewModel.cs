﻿using System;
using System.Globalization;
using System.Linq;
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
using WPFLocalizeExtension.Engine;

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
    public RelayCommand NavigateToEmployeesCommand { get; set; } = null!;
    public RelayCommand NavigateToClientsCommand { get; set; } = null!;
    public RelayCommand NavigateToPenaltiesCommand { get; set; } = null!;
    public RelayCommand NavigateToTouristGroupsCommand { get; set; } = null!;
    public RelayCommand NavigateToRoutesCommand { get; set; } = null!;
    // Catalogs
    public RelayCommand NavigateToCountriesCommand { get; set; } = null!;
    public RelayCommand NavigateToStreetsCommand { get; set; } = null!;
    public RelayCommand NavigateToHotelsCommand { get; set; } = null!;
    public RelayCommand NavigateToPopulatedPlacesCommand { get; set; } = null!;
    // Localization
    public RelayCommand SwitchLocalizationCommand { get; set; } = null!;

    public MainViewModel(INavigationService service)
    {
        Navigation = service;
        //MainMenuVisibility = Visibility.Collapsed;
        InitializePagesCommands();
        InitializeCatalogsCommands();
        SwitchLocalizationCommand = new RelayCommand(
            execute: _ =>
            {
                LocalizeDictionary.Instance.SetCurrentThreadCulture = true;
                if (LocalizeDictionary.Instance.Culture.Name == "ru-RU")
                {
                    LocalizeDictionary.Instance.Culture = new CultureInfo("en-US");
                }
                else if (LocalizeDictionary.Instance.Culture.Name == "en-US")
                {
                    LocalizeDictionary.Instance.Culture = new CultureInfo("ru-RU");
                }
            },
            canExecute: _ => true);
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