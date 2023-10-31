using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Travel_Company.WPF.Resources.Components
{
    /// <summary>
    /// Interaction logic for SearchBar.xaml
    /// </summary>
    public partial class SearchBar : UserControl
    {
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set
            {
                SetValue(TextProperty, value);
            }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(SearchBar), new PropertyMetadata(""));

        public string Placeholder { get; set; } = null!;

        protected override void OnInitialized(EventArgs e)
        {
            InitializeComponent();
            SearchBarPlaceholder.Text = Placeholder;
            base.OnInitialized(e);
        }

        private void Input_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SearchBarComponent_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchBarComponent.Text.Length > 0)
            {
                SearchBarPlaceholder.Text = string.Empty;
                return;
            }
            SearchBarPlaceholder.Text = Placeholder;
        }
    }
}