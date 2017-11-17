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
        bool addMode;

        public EditItems()
        {
            addMode = true; 
            itemLogic = new ItemLogic();
            InitializeComponent();
            fillDataGrid();
            clear();
        }
        
        private void clear()
        {
            itemCodeTextBox.Text = "";
            itemDescTextBox.Text = "";
            itemPriceTextBox.Text = "";
            itemCodeTextBox.IsEnabled = true;
            editItemButton.IsEnabled = false;
            addItemButton.IsEnabled = true;
            removeItemButton.IsEnabled = false;
            addMode = true;
        }
       
        private void fillDataGrid()
        {
            itemDataGrid.ItemsSource = new DataView(itemLogic.getNewData().Tables[0]);
        }
        /// <summary>
        /// If addmMode is enabled(No item is selected) 
        /// Checks if itemcode already exists. If so, an exception and notification is shown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addItemButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (addMode)
                {
                    if (!itemLogic.checkIfItemExists(itemCodeTextBox.Text))
                    {
                        itemLogic.addItem(itemCodeTextBox.Text, itemDescTextBox.Text, Double.Parse(itemPriceTextBox.Text));
                        fillDataGrid();
                        clear();
                    }
                    else
                    {
                        throw new Exception("Item Code already exists! Choose another.");
                    }
                    
                }
                else
                {
                    throw new Exception("Adding is currently not allowed. Hit clear to edit.");
                }
            }

            catch (FormatException)
            {
                MessageBox.Show("The information entered is the incorrect. Check your cost format.");
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }
        /// <summary>
        /// Once an item is selected in the data grid, the fields are updated about the item selected.
        /// If the item selected is empty, then an exception is thrown to clear values and reset addmode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                addMode = false;
                itemCodeTextBox.IsEnabled = false;
                editItemButton.IsEnabled = true;
                addItemButton.IsEnabled = false;
                removeItemButton.IsEnabled = true;
                fillTextBoxes();

            }
            catch(Exception)
            {
                clear();
            }

        }
        /// <summary>
        /// First checks if window is in add mode. If so then an exception is thrown.
        /// Otherwise the selected item is edited.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editItemButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!addMode)
                {
                    itemLogic.setItem(itemCodeTextBox.Text, itemDescTextBox.Text, Double.Parse(itemPriceTextBox.Text));
                    fillDataGrid();
                    clear();
                }
                else
                {
                    throw new Exception("Editing is not enabled. Did you mean add?");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("The information entered is the incorrect format your cost.");
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }

        /// <summary>
        /// Fills text boxes with information about the item selected.
        /// </summary>
        private void fillTextBoxes()
        {
            DataRowView dataRow = (DataRowView)itemDataGrid.SelectedItem;
            itemCodeTextBox.Text = dataRow.Row.ItemArray[0].ToString();
            itemDescTextBox.Text = dataRow.Row.ItemArray[1].ToString();
            itemPriceTextBox.Text = (dataRow.Row.ItemArray[2].ToString());
        }

        /// <summary>
        /// Verifies if window is in add mode. If it is not then the removeItem method is called from ItemLogic.
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeItemButton_Click(object sender, RoutedEventArgs e)
        {
            fillTextBoxes();
            try
            {
                if (!addMode)
                {
                    itemLogic.removeItem(itemCodeTextBox.Text);
                    fillDataGrid();
                    clear();
                }
                else
                {
                    throw new Exception("Please select an item to remove.");
                }
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }

        }
        /// <summary>
        /// Clears text fields.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearButtonSelect(object sender, RoutedEventArgs e)
        {
            clear();
        }
    }
}
