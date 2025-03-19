using System.Windows;
using System.Windows.Controls;

namespace Calculator_APP_
{
    public partial class MainWindow : Window
    {
        public static string SelectedText { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new StandardPage();
            MenuBar? menuBar = this.FindName("MenuBar") as MenuBar;
            if (menuBar != null)
            {
                menuBar.PageChanged += ChangePage;
                menuBar.CopyRequested += CopyResultText;
            }
        }

        private void ChangePage(Type pageType)
        {
            Main.Content = Activator.CreateInstance(pageType);
        }

        private void CopyResultText()
        {
            var standardPage = Main.Content as StandardPage;
            if (standardPage != null)
            {
                var resultTextBox = standardPage.FindName("ResultTextBox") as TextBox;
                if (resultTextBox != null)
                {
                    Clipboard.SetText(resultTextBox.Text);
                }
            }
        }
    }
}