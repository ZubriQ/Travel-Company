﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.Windows;
using Travel_Company.WPF.Core;
using Travel_Company.WPF.Data;
using Travel_Company.WPF.Data.Base;
using Travel_Company.WPF.Models;
using Travel_Company.WPF.MVVM.View;
using Travel_Company.WPF.MVVM.ViewModel;
using Travel_Company.WPF.MVVM.ViewModel.Catalogs;
using Travel_Company.WPF.MVVM.ViewModel.Clients;
using Travel_Company.WPF.MVVM.ViewModel.Employees;
using Travel_Company.WPF.MVVM.ViewModel.Groups;
using Travel_Company.WPF.MVVM.ViewModel.Penalties;
using Travel_Company.WPF.MVVM.ViewModel.Routes;
using Travel_Company.WPF.Services.Authorization;
using Travel_Company.WPF.Services.Navigation;
using WPFLocalizeExtension.Engine;

namespace Travel_Company.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private static ServiceProvider _serviceProvider = null!;

    public static Settings Settings { get; set; } = new();
    public static EventAggregator EventAggregator { get; } = new();

    public App()
    {
        InitializeLocalization();

        IServiceCollection services = new ServiceCollection();

        services.AddDbContext<TravelCompanyDbContext>(
            options => options.UseSqlite("Data Source=TravelCompany.db;"));

        services.AddSingleton(provider => new MainWindow()
        {
            DataContext = provider.GetRequiredService<MainViewModel>()
        });
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<Func<Type, ViewModel>>(
            serviceProvider => viewModelType => (ViewModel)serviceProvider.GetRequiredService(viewModelType));
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        InitializeViewModels(services);
        InitializeDbServices(services);

        _serviceProvider = services.BuildServiceProvider();

        var navigationService = _serviceProvider.GetRequiredService<INavigationService>() as NavigationService;
        navigationService?.Initialize();
    }

    private static void InitializeViewModels(IServiceCollection services)
    {
        services.AddSingleton<MainViewModel>();

        // Pages
        services.AddSingleton<LoginViewModel>();

        services.AddTransient<EmployeesViewModel>();
        services.AddTransient<EmployeesCreateViewModel>();
        services.AddSingleton<EmployeesUpdateViewModel>();

        services.AddTransient<ClientsViewModel>();
        services.AddTransient<ClientsCreateViewModel>();
        services.AddSingleton<ClientsUpdateViewModel>();

        services.AddTransient<PenaltiesViewModel>();
        services.AddTransient<PenaltiesCreateViewModel>();
        services.AddSingleton<PenaltiesUpdateViewModel>();

        services.AddTransient<GroupsViewModel>();
        services.AddTransient<GroupsCreateViewModel>();
        services.AddSingleton<GroupsUpdateViewModel>();

        services.AddTransient<RoutesViewModel>();
        services.AddTransient<RoutesCreateViewModel>();
        services.AddSingleton<RoutesUpdateViewModel>();

        // Catalogs
        services.AddTransient<CatalogsViewModel>();
        services.AddTransient<CatalogsCreateViewModel>();
        services.AddTransient<CatalogsUpdateViewModel>();
    }

    private static void InitializeDbServices(IServiceCollection services)
    {
        services.AddScoped<IAuthorizationService, AuthorizationService>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
        DbInitializer.Seed(_serviceProvider);
    }

    public static ViewModel GetStartupView()
    {
        return _serviceProvider.GetRequiredService<LoginViewModel>();
    }

    private void InitializeLocalization()
    {
        //LocalizeDictionary.Instance.Culture = CultureInfo.CurrentCulture;
        LocalizeDictionary.Instance.Culture = new CultureInfo("ru-RU");
    }
}