using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Travel_Company.WPF.Core;
using Travel_Company.WPF.Core.Enums;

namespace Travel_Company.WPF.Resources.Components;

/// <summary>
/// Interaction logic for GroupsForm.xaml
/// </summary>
public partial class GroupForm : UserControl
{
    public FormState State { get; set; }

    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register("RelayCommand", typeof(RelayCommand), typeof(GroupForm));

    public RelayCommand RelayCommand
    {
        get => (RelayCommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    protected override void OnInitialized(EventArgs e)
    {
        InitializeComponent();
        InitializeForm();
        base.OnInitialized(e);
    }

    private void InitializeForm()
    {
        switch (State)
        {
            case FormState.Updating:
                InitializeUpdating();
                break;
            case FormState.Inserting:
                InitializeInserting();
                break;
        }
    }

    private void InitializeUpdating()
    {
        TextBlockTitle.Text = "Edit Tourist Group";
        ButtonProceed.Content = "Update";
        SetCommand("UpdateCommand");
    }

    private void InitializeInserting()
    {
        TextBlockTitle.Text = "Add Tourist Group";
        ButtonProceed.Content = "Create";
        SetCommand("CreateCommand");
    }

    private void SetCommand(string commandName)
    {
        Binding binding = new()
        {
            Path = new PropertyPath(commandName)
        };
        ButtonProceed.SetBinding(CommandProperty, binding);
    }
}
