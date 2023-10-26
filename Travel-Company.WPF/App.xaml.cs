using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using Travel_Company.WPF.Core;
using Travel_Company.WPF.MVVM.View;
using Travel_Company.WPF.MVVM.ViewModel;
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

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton(provider => new MainWindow()
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<Func<Type, ViewModel>>(
                serviceProvider => viewModelType => (ViewModel)serviceProvider.GetRequiredService(viewModelType));
            InitializeViewModels(services);
            InitializeDbServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private static void InitializeViewModels(IServiceCollection services)
        {
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<LoginViewModel>();
        }
        
        private static void InitializeDbServices(IServiceCollection services)
        {
            services.AddScoped<IAuthorizationService, AuthorizationService>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        public static ViewModel GetStartupView()
        {
            return _serviceProvider.GetRequiredService<LoginViewModel>();
        }
    }
}
