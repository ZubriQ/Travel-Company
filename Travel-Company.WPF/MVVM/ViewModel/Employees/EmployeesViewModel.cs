using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Travel_Company.WPF.Core;
using Travel_Company.WPF.Data.Base;
using Travel_Company.WPF.Data.Dto;
using Travel_Company.WPF.Models;
using Travel_Company.WPF.Services.Navigation;

namespace Travel_Company.WPF.MVVM.ViewModel.Employees;

public sealed class EmployeesViewModel : Core.ViewModel
{
    private readonly IRepository<TourGuide, int> _employeesRepository;

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

    private List<TourGuide> _employees;
    public List<TourGuide> Employees
    {
        get => _employees;
        set
        {
            _employees = value;
            OnPropertyChanged();
        }
    }

    private TourGuide _selectedTourGuide = null!;
    public TourGuide SelectedTourGuide
    {
        get => _selectedTourGuide;
        set
        {
            _selectedTourGuide = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand NavigateToEmployeesUpdateCommand { get; set; }

    public EmployeesViewModel(IRepository<TourGuide, int> repository, INavigationService navigationService)
    {
        _employeesRepository = repository;
        _employees = _employeesRepository
            .GetQuaryable()
            .Include(e => e.Street)
            .ToList();

        Navigation = navigationService;

        NavigateToEmployeesUpdateCommand = new RelayCommand(
            execute: _ =>
            {
                if (SelectedTourGuide is not null)
                {
                    var message = new TourGuideMessage { TourGuide = SelectedTourGuide };
                    App.EventAggregator.Publish(message);
                    Navigation.NavigateTo<EmployeesUpdateViewModel>();
                }
            },
            canExecute: _ => true);
    }
}