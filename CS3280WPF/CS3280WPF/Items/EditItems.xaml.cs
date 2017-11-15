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
        private void addItemButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (itemCodeTextBox.IsEnabled)
                {
                    itemLogic.addItem(itemCodeTextBox.Text, itemDescTextBox.Text, Double.Parse(itemPriceTextBox.Text));
                    fillDataGrid();
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
        private void fillDataGrid()
        {
            itemDataGrid.ItemsSource = new DataView(itemLogic.getNewData().Tables[0]);
        }

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

        private void editItemButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!addMode)
                {
                    itemLogic.setItem(itemCodeTextBox.Text, itemDescTextBox.Text, Double.Parse(itemPriceTextBox.Text));
                    fillDataGrid();
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
        private void fillTextBoxes()
        {
            DataRowView dataRow = (DataRowView)itemDataGrid.SelectedItem;
            itemCodeTextBox.Text = dataRow.Row.ItemArray[0].ToString();
            itemDescTextBox.Text = dataRow.Row.ItemArray[1].ToString();
            itemPriceTextBox.Text = (dataRow.Row.ItemArray[2].ToString());
        }

        private void removeItemButton_Click(object sender, RoutedEventArgs e)
        {
            fillTextBoxes();
            try
            {
                if (!addMode)
                {

                    fillDataGrid();
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

        private void clearButtonSelect(object sender, RoutedEventArgs e)
        {
            clear();
        }
    }
}
