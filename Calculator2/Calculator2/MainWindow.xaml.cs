using System.Windows;
using System.Windows.Controls;

namespace Calculator2
{
    public partial class MainWindow : Window
    {
        private bool isInitialized = false;

        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new StandardPage();
            isInitialized = true;
        }

        public void Standard_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new StandardPage();
        }

        public void Programmer_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ProgrammerPage();
        }

        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isInitialized) return;

            if (Combo.SelectedItem is ComboBoxItem selectedItem)
            {
                switch (selectedItem.Content.ToString())
                {
                    case "Standard":
                        Main.Content = new StandardPage();
                        break;
                    case "Programator":
                        Main.Content = new ProgrammerPage();
                        break;
                }
            }
        }
    }
}