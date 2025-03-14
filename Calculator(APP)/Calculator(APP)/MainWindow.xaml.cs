using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calculator_APP_
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new StandardPage();
            MenuBar menuBar = this.FindName("MenuBar") as MenuBar;
            if (menuBar != null)
            {
                menuBar.PageChanged += ChangePage;
            }
        }
        public void Standard_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new StandardPage();
        }
        public void Programmer_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ProgrammerPage();
        }
        

        private void ChangePage(Type pageType)
        {
            Main.Content = Activator.CreateInstance(pageType);
        }
    }
}