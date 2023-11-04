using System.Collections.Generic;
using Travel_Company.WPF.Core;
using Travel_Company.WPF.Data.Base;
using Travel_Company.WPF.Models;
using Travel_Company.WPF.Services.Navigation;

namespace Travel_Company.WPF.MVVM.ViewModel.Employees;

public sealed class EmployeesCreateViewModel : Core.ViewModel
{
    private readonly IRepository<Street, long> _streetsRepository = null!;
    private readonly IRepository<TourGuide, int> _employeesRepository = null!;

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

    private TourGuide _employee = new();
    public TourGuide Employee
    {
        get => _employee;
        set
        {
            _employee = value;
            OnPropertyChanged();
        }
    }

    private List<Street> _streets = null!;
    public List<Street> Streets
    {
        get => _streets;
        set
        {
            _streets = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand CreateEmployeeCommand { get; set; }
    public RelayCommand CancelCommand { get; set; }

    public EmployeesCreateViewModel(
        IRepository<Street, long> streetsRepo,
        IRepository<TourGuide, int> employeesRepo,
        INavigationService navigation)
    {
        _streetsRepository = streetsRepo;
        Streets = _streetsRepository.GetAll();
        _employeesRepository = employeesRepo;

        Navigation = navigation;

        CreateEmployeeCommand = new RelayCommand(
            execute: _ =>
            {
                // TODO: Data validation.

                _employeesRepository.Insert(Employee);
                _employeesRepository.SaveChanges();

                Navigation.NavigateTo<EmployeesViewModel>();
            },
            canExecute: _ => true);

        CancelCommand = new RelayCommand(
           execute: _ => Navigation.NavigateTo<EmployeesViewModel>(),
           canExecute: _ => true);
    }
}