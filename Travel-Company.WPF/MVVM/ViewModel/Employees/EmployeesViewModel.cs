using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Travel_Company.WPF.Data.Base;
using Travel_Company.WPF.Models;

namespace Travel_Company.WPF.MVVM.ViewModel.Employees;

public sealed class EmployeesViewModel : Core.ViewModel
{
    private readonly IRepository<TourGuide, int> _employeesRepository;

    public EmployeesViewModel(IRepository<TourGuide, int> repository)
    {
        _employeesRepository = repository;
        _employees = _employeesRepository
            .GetQuaryable()
            .Include(e => e.Street)
            .ToList();
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
}