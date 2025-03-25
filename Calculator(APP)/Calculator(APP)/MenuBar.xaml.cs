using System;
using System.Windows;
using System.Windows.Controls;

namespace Calculator_APP_
{
    public partial class MenuBar : UserControl
    {
        public event Action<Type> PageChanged;
        public event Action CopyRequested;
        public event Action CutRequested;
        public event Action PasteRequested;

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

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            CopyRequested?.Invoke();
        }

        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            CutRequested?.Invoke();
        }

        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            PasteRequested?.Invoke();
        }
    }
}