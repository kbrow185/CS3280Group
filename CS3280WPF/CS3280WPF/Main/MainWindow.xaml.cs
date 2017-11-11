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

namespace CS3280WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            clsDataAccess dataAccess = new clsDataAccess();
            //InitializeComponent();
            
            Items.EditItems items = new Items.EditItems();
            items.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          
        }
    }
}
