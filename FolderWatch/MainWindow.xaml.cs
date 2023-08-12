using FolderWatch.ModelViews;
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

namespace FolderWatch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = new MainWindowView();
        }
        private void ComboBoxSelectionChanged_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (DataContext is MainWindow viewModel)
            {
                viewModel.ComboBoxSelectionChanged.Execute(e.Parameter);
            }
        }
    }
}
