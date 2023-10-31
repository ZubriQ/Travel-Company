﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using Travel_Company.WPF.Core;
using Travel_Company.WPF.Data.Base;
using Travel_Company.WPF.Data.Dto;
using Travel_Company.WPF.Models;
using Travel_Company.WPF.Services.Navigation;

namespace Travel_Company.WPF.MVVM.ViewModel.Clients;

public sealed class ClientsViewModel : Core.ViewModel
{
    private readonly IRepository<Client, int> _clientsRepository;

    private INavigationService _navigation;
    public INavigationService Navigation
    {
        get => _navigation;
        set
        {
            _navigation = value;
            OnPropertyChanged();
        }
    }

    private readonly List<Client> _fetchedClients;
    private List<Client> _clients = null!;
    public List<Client> Clients
    {
        get => _clients;
        set
        {
            _clients = value;
            OnPropertyChanged();
        }
    }

    private Client _selectedClient = null!;
    public Client SelectedClient
    {
        get => _selectedClient;
        set
        {
            _selectedClient = value;
            OnPropertyChanged();
        }
    }

    private string _searchText = string.Empty;
    public string SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            OnPropertyChanged();
            FilterItems();
        }
    }

    private Visibility _isPassportDataVisible = Visibility.Hidden;
    public Visibility IsPassportDataVisible
    {
        get => _isPassportDataVisible;
        set
        {
            _isPassportDataVisible = value;
            OnPropertyChanged();
        }
    }

    private string _toggleButtonContent = "Show Passport Data";
    public string ToggleButtonContent
    {
        get => _toggleButtonContent;
        set
        {
            _toggleButtonContent = value;
            OnPropertyChanged();
        }
    }

    private void FilterItems()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            Clients = _fetchedClients.ToList();
        }
        else
        {
            Clients = _fetchedClients
                .Where(c => c.FullName.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }

    public RelayCommand NavigateToUpdatingCommand { get; set; } = null!;
    public RelayCommand NavigateToInsertingCommand { get; set; } = null!;
    public RelayCommand DeleteSelectedItemCommand { get; set; } = null!;
    public RelayCommand ToggleColumnsCommand { get; set; } = null!;

    public ClientsViewModel(IRepository<Client, int> repository, INavigationService navigation)
    {
        _clientsRepository = repository;
        _navigation = navigation;

        _fetchedClients = FetchDataGridData();
        Clients = _fetchedClients;

        InitializeCommands();
    }

    private List<Client> FetchDataGridData() => _clientsRepository
        .GetQuaryable()
        .Include(c => c.Street)
        .Include(c => c.TouristGroup)
        .ToList();

    private void InitializeCommands()
    {
        NavigateToUpdatingCommand = new RelayCommand(
            execute: _ => HandleUpdating(),
            canExecute: _ => true);
        NavigateToInsertingCommand = new RelayCommand(
           execute: _ => Navigation.NavigateTo<ClientsCreateViewModel>(),
           canExecute: _ => true);
        DeleteSelectedItemCommand = new RelayCommand(
            execute: _ => HandleDeleting(),
            canExecute: _ => true);
        ToggleColumnsCommand = new RelayCommand(
            execute: _ => HandleColumnToggling(),
            canExecute: _ => true);
    }

    private void HandleColumnToggling()
    {
        IsPassportDataVisible = (IsPassportDataVisible is Visibility.Hidden)
            ? Visibility.Visible
            : Visibility.Hidden;
        ToggleButtonContent = IsPassportDataVisible is Visibility.Hidden
            ? "Show Passport Data" 
            : "Hide Passport Data";
    }

    private void HandleDeleting()
    {
        if (SelectedClient is not null)
        {
            _clientsRepository.Delete(SelectedClient);
            _clientsRepository.SaveChanges();
        }
    }

    private void HandleUpdating()
    {
        if (SelectedClient is not null)
        {
            var message = new ClientMessage { Client = SelectedClient };
            App.EventAggregator.Publish(message);
            Navigation.NavigateTo<ClientsUpdateViewModel>();
        }
    }
}