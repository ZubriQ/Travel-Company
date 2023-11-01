using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Travel_Company.WPF.Core;
using Travel_Company.WPF.Data.Base;
using Travel_Company.WPF.Data.Dto;
using Travel_Company.WPF.Models;
using Travel_Company.WPF.MVVM.ViewModel.Clients;
using Travel_Company.WPF.Services.Navigation;

namespace Travel_Company.WPF.MVVM.ViewModel.Penalties;

public class PenaltiesViewModel : Core.ViewModel
{
    private readonly IRepository<Penalty, int> _penaltiesRepository;

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

    private Client? _clientToFilterBy;

    private List<Penalty> _fetchedPenalties;
    private List<Penalty> _penalties = null!;
    public List<Penalty> Penalties
    {
        get => _penalties;
        set
        {
            _penalties = value;
            OnPropertyChanged();
        }
    }

    private Penalty _selectedItem = null!;
    public Penalty SelectedItem
    {
        get => _selectedItem;
        set
        {
            _selectedItem = value;
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

    private void FilterItems()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            Penalties = _fetchedPenalties.ToList();
        }
        else
        {
            Penalties = _fetchedPenalties
                .Where(c => c.Client.FullName.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }

    public RelayCommand NavigateToUpdatingCommand { get; set; } = null!;
    public RelayCommand NavigateToInsertingCommand { get; set; } = null!;
    public RelayCommand DeleteSelectedItemCommand { get; set; } = null!;

    public PenaltiesViewModel(IRepository<Penalty, int> repository, INavigationService navigation)
    {
        _penaltiesRepository = repository;
        _navigation = navigation;

        _fetchedPenalties = FetchDataGridData();
        Penalties = _fetchedPenalties;

        // TODO: Subscribe and get the client, then filter by it.

        InitializeCommands();
    }

    private List<Penalty> FetchDataGridData() => _penaltiesRepository
        .GetQuaryable()
        .Include(p => p.Client)
        .Include(c => c.TourGuide)
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
    }
    private void HandleUpdating()
    {
        if (SelectedItem is not null)
        {
            var message = new PenaltyMessage { Penalty = SelectedItem };
            App.EventAggregator.Publish(message);
            //Navigation.NavigateTo<PenaltiesUpdateViewModel>();
        }
    }

    private void HandleDeleting()
    {
        if (SelectedItem is not null)
        {
            _penaltiesRepository.Delete(SelectedItem);
            _penaltiesRepository.SaveChanges();

            _fetchedPenalties = FetchDataGridData();
            Penalties = _fetchedPenalties;
        }
    }
}