using Travel_Company.WPF.Data.Dto;
using Travel_Company.WPF.Models;

namespace Travel_Company.WPF.MVVM.ViewModel.Employees;

public sealed class EmployeesUpdateViewModel : Core.ViewModel
{
    public EmployeesUpdateViewModel()
    {
        App.EventAggregator.Subscribe<TourGuideMessage>(HandleEmployeeMessage);
    }

    private void HandleEmployeeMessage(TourGuideMessage message)
    {
        Employee = message.TourGuide;
    }

    private TourGuide _employee;
    public TourGuide Employee
    {
        get => _employee;
        set
        {
            _employee = value;
            OnPropertyChanged();
        }
    }
}