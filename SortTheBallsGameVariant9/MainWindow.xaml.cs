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

namespace SortTheBallsGameVariant9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AboutApp_Click(object sender, RoutedEventArgs e)
        {
            AboutApp window = new AboutApp();
            window.Show();
        }
        private void AboutAuthor_Click(object sender, RoutedEventArgs e)
        {
            AboutAuthor window = new AboutAuthor();
        }
    }
}
