using System.Windows;
using System.Windows.Controls;

namespace Calculator_APP_
{
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new StandardPage());
        }

        private void ModeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ModeComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string mode = selectedItem.Content.ToString();

                if (mode == "Calculator Standard")
                {
                    MainContent.Content = new StandardPage();
                }
                else if (mode == "Calculator Programator")
                {
                    MainContent.Content = new ProgammingPage();
                }
            }
        }
    }
}