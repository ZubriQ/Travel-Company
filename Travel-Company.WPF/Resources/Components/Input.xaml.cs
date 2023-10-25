using System;
using System.Windows.Controls;

namespace Travel_Company.WPF.Resources.Components;

/// <summary>
/// Interaction logic for Input.xaml
/// </summary>
public partial class Input : UserControl
{
    public string? ComponentName { get; set; }

    protected override void OnInitialized(EventArgs e)
    {
        InitializeComponent();
        Placeholder.Text = ComponentName;
        base.OnInitialized(e);
    }

    private void Input_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (ComponentInput.Text.Length > 0)
        {
            Placeholder.Text = string.Empty;
            return;
        }
        Placeholder.Text = ComponentName;
    }
}
