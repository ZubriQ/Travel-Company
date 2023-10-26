using Travel_Company.WPF.Core;
using Travel_Company.WPF.Models.Users;
using Travel_Company.WPF.MVVM.ViewModel.Employees;
using Travel_Company.WPF.Services.Authorization;
using Travel_Company.WPF.Services.Navigation;

namespace Travel_Company.WPF.MVVM.ViewModel;

public sealed class LoginViewModel : Core.ViewModel
{
    private readonly IAuthorizationService _authorizationService;

    private string _username = App.Settings.UserName;
    public string Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged();
        }
    }

    private string _password = string.Empty;
    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged();
        }
    }

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

    public RelayCommand NavigateToEmployeesCommand { get; set; }

    public LoginViewModel(INavigationService navigationService, IAuthorizationService authorizationService)
    {
        Navigation = navigationService;
        _authorizationService = authorizationService;

        NavigateToEmployeesCommand = new RelayCommand(
            execute: _ =>
            {
                HandleAuthorization();
            },
            canExecute: _ => true);
    }

    private void HandleAuthorization()
    {
        if (_authorizationService.LogIn(Username, Password) is User user)
        {
            SaveAuthorizedUserData(user);
            Navigation.NavigateTo<EmployeesViewModel>();
        }
    }

    private static void SaveAuthorizedUserData(User user)
    {
        App.Settings.User = user;
        App.Settings.IsAuthorized = true;
    }
}