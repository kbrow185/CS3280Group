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
        ItemLogic itemLogic;
        public EditItems()
        {
            itemLogic = new ItemLogic();
            InitializeComponent();
            fillDataGrid();
        }
        
        private void clear()
        {
            itemCodeTextBox.Text = "";
            itemDescTextBox.Text = "";
            itemPriceTextBox.Text = "";
            itemCodeTextBox.IsEnabled = true;
        }
        private void addItemButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                itemLogic.addItem(itemCodeTextBox.Text, itemDescTextBox.Text, Double.Parse(itemPriceTextBox.Text));
            }
            catch (FormatException)
            {
                MessageBox.Show("The information entered is the incorrect format your cost.");
            }
            catch(Exception)
            {
                MessageBox.Show("Incorrect format. Please check your values again.");
            }
        }
        private void fillDataGrid()
        {
            itemDataGrid.ItemsSource = new DataView(itemLogic.getNewData().Tables[0]);
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
                clear();
            }

        }

        private void editItemButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                itemLogic.setItem(itemCodeTextBox.Text, itemDescTextBox.Text, Double.Parse(itemPriceTextBox.Text));
            }
            catch (FormatException)
            {
                MessageBox.Show("The information entered is the incorrect format your cost.");
            }
            catch (Exception)
            {
                MessageBox.Show("Incorrect format. Please check your values again.");
            }
        }


        private void removeItemButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
