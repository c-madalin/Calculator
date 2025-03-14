using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            DataContext = new CalculatorVM();
        }
        

        public void Standard_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new StandardPage();
        }

        public void Programmer_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ProgrammerPage();
        }
        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = (this.WindowState == WindowState.Maximized) ? WindowState.Normal : WindowState.Maximized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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