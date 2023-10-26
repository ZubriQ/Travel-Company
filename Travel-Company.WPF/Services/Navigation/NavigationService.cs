﻿using System;
using Travel_Company.WPF.Core;

namespace Travel_Company.WPF.Services.Navigation;

public class NavigationService : ObservableObject, INavigationService
{
    private readonly Func<Type, ViewModel> _viewModelFactory;

    private ViewModel _currentView;
    public ViewModel CurrentView
    {
        get => _currentView;
        private set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }

    public NavigationService(Func<Type, ViewModel> viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;
    }

    public void Initialize()
    {
        _currentView = App.GetStartupView();
    }

    public void NavigateTo<TViewModel>() where TViewModel : ViewModel
    {
        CurrentView = _viewModelFactory.Invoke(typeof(TViewModel));
    }
}