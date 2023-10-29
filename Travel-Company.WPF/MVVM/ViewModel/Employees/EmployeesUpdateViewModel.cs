using Travel_Company.WPF.Data.Dto;

namespace Travel_Company.WPF.MVVM.ViewModel.Employees;

public sealed class EmployeesUpdateViewModel : Core.ViewModel
{
    public EmployeesUpdateViewModel()
    {
        App.EventAggregator.Subscribe<TourGuideMessage>(HandleEmployeeMessage);
    }
    private void HandleEmployeeMessage(TourGuideMessage message)
    {
        // Here access
        var employee = message.TourGuide;
    }
}