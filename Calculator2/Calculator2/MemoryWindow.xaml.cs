using System.Collections.Generic;
using System.Windows;

namespace Calculator2
{
    public partial class MemoryWindow : Window
    {
        public MemoryWindow(IEnumerable<double> memoryStack, double currentDisplayValue)
        {
            InitializeComponent();
            DataContext = new MemoryViewModel(new MemoryManager(memoryStack), currentDisplayValue);
        }
    }
}