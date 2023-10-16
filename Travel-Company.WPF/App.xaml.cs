using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using Travel_Company.WPF.Core;
using Travel_Company.WPF.MVVM.View;
using Travel_Company.WPF.MVVM.ViewModel;
using Travel_Company.WPF.Services.Navigation;

namespace Travel_Company.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static ServiceProvider _serviceProvider = null!;

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton(provider => new MainWindow()
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<Func<Type, ViewModel>>(
                serviceProvider => viewModelType => (ViewModel)serviceProvider.GetRequiredService(viewModelType));

            _serviceProvider = services.BuildServiceProvider();
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
