using System;
using System.Windows;
using System.Windows.Controls;

namespace Travel_Company.WPF.Resources.Components;

/// <summary>
/// Interaction logic for Password.xaml
/// </summary>
public partial class Password : UserControl
{
    private readonly string _componentName = "Password";

    protected override void OnInitialized(EventArgs e)
    {
        InitializeComponent();
        Placeholder.Text = _componentName;
        base.OnInitialized(e);
    }

    private void ComponentPassword_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (ComponentPassword.Password.Length > 0)
        {
            Placeholder.Text = string.Empty;
            return;
        }
        Placeholder.Text = _componentName;
    }
}
