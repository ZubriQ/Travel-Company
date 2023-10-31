using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
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
using Travel_Company.WPF.Services.Authorization;
using Travel_Company.WPF.Services.Navigation;

namespace Travel_Company.WPF
{
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
            IServiceCollection services = new ServiceCollection();

            services.AddDbContext<TravelCompanyDbContext>(options => options.UseSqlite("Data Source=TravelCompany.db;"));

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
            services.AddSingleton<EmployeesCreateViewModel>();
            services.AddSingleton<EmployeesUpdateViewModel>();
            services.AddSingleton<ClientsViewModel>();
            services.AddSingleton<ClientsCreateViewModel>();
            services.AddSingleton<ClientsUpdateViewModel>();
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
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            DbInitializer.Seed(_serviceProvider);

            base.OnStartup(e);
        }

        public static ViewModel GetStartupView()
        {
            return _serviceProvider.GetRequiredService<LoginViewModel>();
        }
    }
}