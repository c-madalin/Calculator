using System;
using System.Windows;
using System.Windows.Controls;

namespace Calculator_APP_
{
    public partial class MenuBar : UserControl
    {
        // Definim un eveniment care va fi folosit pentru a anunța schimbarea paginii
        public event Action<Type> PageChanged;

        public MenuBar()
        {
            InitializeComponent();
        }

        public void Standard_Click(object sender, RoutedEventArgs e)
        {
            PageChanged?.Invoke(typeof(StandardPage));
        }

        public void Programmer_Click(object sender, RoutedEventArgs e)
        {
            PageChanged?.Invoke(typeof(ProgrammerPage));
        }
        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Calculator WPF\nCreated by: [Cazan Madalin Cristian]\nGroup: [10LF331]", "About");
        }
    }
}
