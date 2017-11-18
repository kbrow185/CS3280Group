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

//Link to other windows.
using CS3280WPF.Main;
using CS3280WPF.Items;
using CS3280WPF.Search;
using System.Reflection;


/* Kyler's TODO / notes:
 * Create invoice display.
 * 
 * REMOVE ITEM?!
 * 
 * Clicking "New Invoice" should prompt the user to save the currently selected invoice,
 * if applicable. Set invoice number display to "TBD".
 * 
 * Save Invoice should return invoice number for display.
 * 
 * grdMain should be disabled until New Invoice is clicked or Search Invoice returns a value.
 * ---Create an enableUI() method that sets up the UI based on new or existing invoice.
 * Save Invoice needs to call an UpdateInvoice() method instead a NewInvoice() method.
 * 
 * Clicking day, month, or year captions should set the keyboard focus to their respective textbox.
 * 
 * numbers only:
 * Date
 * Quantity
 * 
 * 
 * Delete Invoice needs to prompt the user before deleting.
 * 
 * SearchInvoices should return the InvoiceNum of the selected invoice to MainWindow for display.
 * 
 * 
 */


namespace CS3280WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Attribute(s)

        /// <summary>
        /// EditItems window reference.
        /// Used to display the EditItems window.
        /// </summary>
        EditItems editItems;

        /// <summary>
        /// SearchInvoices window reference.
        /// Used to display the SearchInvoices window.
        /// </summary>
        SearchInvoices searchInvoices;

        /// <summary>
        /// Business logic for the MainWindow.
        /// </summary>
        clsMainWindowLogic MainWindowLogic;

        #endregion




        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindow()
        {
            try
            {
                //Ensure that the application will close.
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

                InitializeComponent();

                //Initialize business logic.
                MainWindowLogic = new clsMainWindowLogic();
            }
            catch (Exception ex)
            {
                //Top level method: handle the exception.
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }





        #region Click Handlers

        /// <summary>
        /// Called when the "New Invoice" menu button is clicked.
        /// 
        /// Setup the MainWindow invoice display for New Invoice entry.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void muiNewInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Enable UI.
                grdMain.IsEnabled = true;

                //Display Invoice Number as TBA since no number is assigned yet.
                tbxInvoiceNumber.Text = "TBD";

                //Refresh available items.
                GetItems();

                //Disable quantity entry until an item is selected.
                tbxAddItemQuantity.IsEnabled = false;
            }
            catch (Exception ex)
            {
                //Top level method: handle the exception.
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }





        /// <summary>
        /// Called when the "Search Invoices" menu button is clicked.
        /// 
        /// Displays the SearchInvoices window.
        /// </summary>
        /// <param name="sender">Search Invoices Menu Button</param>
        /// <param name="e"></param>
        private void muiSearchInvoices_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Hide MainWindow.
                this.Hide();

                //Display SearchInvoices window.
                searchInvoices = new SearchInvoices();
                searchInvoices.ShowDialog();

                //After closing SearchInvoices, return to MainWindow.
                this.Show();


                //**************THE FOLLOWING REQUIRE AN INVOICE TO BE RETURNED***************
                //Enable UI.
                //grdMain.isEnabled = true;

                //Refresh available items.
                //GetItems();

                //Disable quantity entry until an item is selected.
                //tbxAddItemQuantity.IsEnabled = false;
            }
            catch (Exception ex)
            {
                //Top level method: handle the exception.
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }




        /// <summary>
        /// Called when the "Edit Items" menu button is clicked.
        /// 
        /// Displays the EditItems window.
        /// </summary>
        /// <param name="sender">Edit Items Table Menu Button</param>
        /// <param name="e"></param>
        private void muiEditItems_Click(object sender, RoutedEventArgs e)
        {
            /* Notes:
             * 
             * ALTER THAT ALL UNSAVED PROGRESS WILL BE LOST. SAVE INVOICE FIRST!
             * 
             * Could return to the saved invoice, if there's enough time to code that logic in. 
             * 
             */

            try
            {
                //Hide MainWindow.
                this.Hide();

                //Display EditItems window.
                editItems = new EditItems();
                editItems.ShowDialog();

                //After closing EditItems, return to MainWindow.
                this.Show();

                //Refresh available items.
                GetItems();

                //Disable quantity entry until an item is selected.
                tbxAddItemQuantity.IsEnabled = false;
            }
            catch (Exception ex)
            {
                //Top level method: handle the exception.
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }




        /// <summary>
        /// Called when the "Add Item" button is clicked.
        /// </summary>
        /// <param name="sender">"Add Item" button.</param>
        /// <param name="e"></param>
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            /* Notes:
             * Cannot add item that is already in the invoice.
             * 
             * Quantity must be 1 or greater.
             * 
             */
            try
            {

            }
            catch (Exception ex)
            {
                //************INPUT VALIDATION*****************

                //Top level method: handle the exception.
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }




        /// <summary>
        /// Called when the "Save Invoice" button is clicked.
        /// </summary>
        /// <param name="sender">"Save Invoice" button.</param>
        /// <param name="e"></param>
        private void btnSaveInvoice_Click(object sender, RoutedEventArgs e)
        {
            /* Notes:
             * Must have at least 1 item added.
             * Must have a date entered. Date must pass validation!
             * 
             */
            try
            {

            }
            catch (Exception ex)
            {
                //************* THIS SHOULD HAVE INPUT VALIDATION*************

                //Top level method: handle the exception.
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }




        /// <summary>
        /// Called when "Remove Selected Item" button is clicked.
        /// </summary>
        /// <param name="sender">"Remove Selected Item" button.</param>
        /// <param name="e"></param>
        private void btnRemoveSelectedItem_Click(object sender, RoutedEventArgs e)
        {
            /* Notes:
             * Disabled until an item is selected in the datagrid.
             * Prompt user before removing.
             * 
             */
            try
            {

            }
            catch (Exception ex)
            {
                //Top level method: handle the exception.
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }




        /// <summary>
        /// Called when "Delete Invoice" button is clicked.
        /// </summary>
        /// <param name="sender">"Delete Invoice" button.</param>
        /// <param name="e"></param>
        private void btnDeleteInvoice_Click(object sender, RoutedEventArgs e)
        {
            /* Notes:
             * Prompt user before deleting.
             * 
             */
            try
            {

            }
            catch (Exception ex)
            {
                //Top level method: handle the exception.
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        #endregion




        #region Method(s)

        /// <summary>
        /// Called when "New Invoice" button is clicked.
        /// Also called when returning from EditItems or SeachInvoices to
        /// refresh any changes / populate the combo box.
        /// </summary>
        private void GetItems()
        {
            try
            {
                //Get available items from database.
                MainWindowLogic.GetItems();

                //Bind them to cbxAddItemSelectItem.
                cbxAddItemSelectItem.ItemsSource = MainWindowLogic.Items;
            }
            catch (Exception ex)
            {
                //Top level method: handle the exception.
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }




        /// <summary>
        /// Reset quantity to 1 and calculate cost when any new item is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxAddItemSelectItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //Enable quantity entry now that an item is selected.
                tbxAddItemQuantity.IsEnabled = true;

                //Get selected item.
                clsItem Item = (clsItem)cbxAddItemSelectItem.SelectedItem;

                //Null check prevents this from running when changing to no selection.
                if(Item != null)
                {
                    //Display 1 as default quantity.
                    tbxAddItemQuantity.Text = "1";

                    //Display cost of 1 of selected item.
                    tbxAddItemCost.Text = "$" + Item.Cost;
                }
                //Blank out the quantity and cost when nothing is selected.
                else
                {
                    tbxAddItemQuantity.Text = "";
                    tbxAddItemCost.Text = "";
                }
            }
            catch (Exception ex)
            {
                //Top level method: handle the exception.
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }




        /// <summary>
        /// Used to calculate the cost of the selected item for the stated quantity
        /// of that item.
        /// 
        /// The cost is then displayed.
        /// </summary>
        /// <returns></returns>
        private void CalculateCost()
        {
            try {
                //Get selected item.
                clsItem Item = (clsItem)cbxAddItemSelectItem.SelectedItem;

                //Ensure an item is selected before running.
                if(Item != null)
                {
                    //Get quantity.
                    int tempQuantity = 1;
                    Int32.TryParse(tbxAddItemQuantity.Text.ToString(), out tempQuantity);

                    //Get cost of 1.
                    double tempCost = 0.00;
                    double.TryParse(Item.Cost, out tempCost);

                    //Display the calculated cost.
                    tbxAddItemCost.Text = "$" + MainWindowLogic.CalculateCost(tempQuantity, tempCost).ToString();
                }
            }
            catch (Exception ex)
            {
                //Throw back up.
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }




        /// <summary>
        /// KeyDown KeyEventHandler for any box that requires numbers only as input.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumbersOnly_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //Only allow numbers: top or numpad.            
                //If it is a number, it is typed into the TextBox.
                if (!((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)))
                {
                    //Allow backspace, delete, enter, and escape.
                    if (!(e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Enter || e.Key == Key.Escape))
                    {
                        //Set the key to handled if it is NOT a number, backspace, delete, enter, or escape.
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                //Top level method: handle the exception.
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }




        /// <summary>
        /// Called when typing in the Quantity.
        /// 
        /// Allows only numbers to be entered and calculates the cost as the quantity is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxAddItemQuantity_KeyDown(object sender, KeyEventArgs e)
        {
           try
            {
                NumbersOnly_KeyDown(sender, e);

                //Calculate cost when enter is pressed.
                if(e.Key == Key.Enter)
                    CalculateCost();
            }
            catch (Exception ex)
            {
                //Top level method: handle the exception.
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }



        /// <summary>
        /// Calculates the cost when quantity entry loses focus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxAddItemQuantity_LostFocus_1(object sender, RoutedEventArgs e)
        {
            try
            {
                CalculateCost();
            }
            catch (Exception ex)
            {
                //Top level method: handle the exception.
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }




        /// <summary>
        /// Ensure only numbers entered into day.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxInvoiceDateDay_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                NumbersOnly_KeyDown(sender, e);

                //Focus on month when enter or tab pressed.
                if (e.Key == Key.Enter || e.Key == Key.Tab)
                    Keyboard.Focus(tbxInvoiceDateMonth);
            }
            catch (Exception ex)
            {
                //Top level method: handle the exception.
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Ensure only numbers entered into month.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxInvoiceDateMonth_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                NumbersOnly_KeyDown(sender, e);

                //Focus on year when enter or tab pressed.
                if (e.Key == Key.Enter || e.Key == Key.Tab)
                    Keyboard.Focus(tbxInvoiceDateYear);
            }
            catch (Exception ex)
            {
                //Top level method: handle the exception.
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Ensure only numbers entered into year.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxInvoiceDateYear_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                NumbersOnly_KeyDown(sender, e);
            }
            catch (Exception ex)
            {
                //Top level method: handle the exception.
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }




        /// <summary>
        /// Handles errors by displaying the class and method they occured in
        /// as well as the error message then writing the error to the log.
        /// </summary>
        /// <param name="sClass">The class in which the error occurred in.</param>
        /// <param name="sMethod">The method in which the error occurred in.</param>
        public void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                //Display error to user.
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);

                //Write error to log.
                System.IO.File.AppendAllText("error_log.txt", sClass + "." + sMethod + " -> " + sMessage);

                //Close GameMenu window.
                this.Close();
            }
            catch (Exception ex)
            {
                //Write exception handling exception to the log.
                System.IO.File.AppendAllText("error_log.txt", Environment.NewLine +
                                             "HandleError Exception: " + ex.Message);

                //If the exception handling has an exception, the game needs to die. Kill the program.
                Application.Current.Shutdown();
            }
        }





        #endregion
    }
}
