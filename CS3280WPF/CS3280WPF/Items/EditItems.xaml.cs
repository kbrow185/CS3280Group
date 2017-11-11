using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace CS3280WPF.Items
{
    /// <summary>
    /// Interaction logic for EditItems.xaml
    /// </summary>
    public partial class EditItems : Window
    {
        clsDataAccess dataAccess;
        DataSet itemSet;
        int numOfElements;
        String[] columns;
        List<List<String>> dataElements;
        public EditItems()
        {

            columns = new string[] { "ItemCode", "Item Description", "Cost"};
            dataAccess = new clsDataAccess();
           
            InitializeComponent();
            refreshData();
            // itemDataGrid.ItemsSource = dataElements;
        }

        private void addItemButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void refreshData()
        {
            string tableName = "ItemDesc";
            dataElements = new List<List<String>>();
            itemSet = dataAccess.ExecuteSQLStatement("SELECT * FROM [" + tableName + "]", ref numOfElements);
            itemDataGrid.ItemsSource = new DataView(itemSet.Tables[0]);
        }

        private void itemDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                itemCodeTextBox.IsEnabled = false;
                DataRowView dataRow = (DataRowView)itemDataGrid.SelectedItem;
                int index = itemDataGrid.CurrentCell.Column.DisplayIndex;

                itemCodeTextBox.Text = dataRow.Row.ItemArray[0].ToString();
                itemDescTextBox.Text = dataRow.Row.ItemArray[1].ToString();
                itemPriceTextBox.Text = (dataRow.Row.ItemArray[2].ToString() + "$");
            }
            catch(Exception)
            {
                itemCodeTextBox.Text = "";
                itemDescTextBox.Text = "";
                itemPriceTextBox.Text = "";
                itemCodeTextBox.IsEnabled = true;
            }

        }

        private void editItemButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removeItemButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
