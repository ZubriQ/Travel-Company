using System.Collections.Generic;
using Travel_Company.WPF.Data.Base;
using Travel_Company.WPF.Data.Dto;
using Travel_Company.WPF.Models;

namespace Travel_Company.WPF.MVVM.ViewModel.Employees;

public sealed class EmployeesUpdateViewModel : Core.ViewModel
{
    private readonly IRepository<Street, long> _streetsRepository = null!;

    public EmployeesUpdateViewModel(IRepository<Street, long> repository)
    {
        _streetsRepository = repository;
        Streets = _streetsRepository.GetAll();
        App.EventAggregator.Subscribe<TourGuideMessage>(HandleEmployeeMessage);
    }

    private void HandleEmployeeMessage(TourGuideMessage message)
    {
        Employee = message.TourGuide!;
    }

    private TourGuide _employee = null!;
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
}