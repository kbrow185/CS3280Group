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
using System.Reflection;

//Link to other windows.
using CS3280WPF.Items;
using CS3280WPF.Search;
using System.Collections.ObjectModel;



/* Kyler's notes:
 * 
 * SearchInvoices should set the static variable InvoiceNumber in MainWindow.
 * This is how MainWindow will open the selected invoice.
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

        /// <summary>
        /// Stores if the UI should be in editing mode.
        /// UI:
        /// true = editing mode.
        /// false = viewing mode.
        /// </summary>
        Boolean isEditing = false;

        /// <summary>
        /// Stores if the invoice being worked on is new (true) or existing (false).
        /// Used to determine how to access the database.
        /// 
        /// Database:
        /// true = insert.
        /// false = update.
        /// </summary>
        Boolean newInvoice = true;

        /// <summary>
        /// Stores the selected invoice's InvoiceNumber.
        /// 
        /// 0 is the default because the lowest legit number is 1.
        /// If this is 0, none has been selected.
        /// </summary>
        public static String InvoiceNumber = "0";

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
        /// Called when the "New Invoice" menu item is clicked.
        /// 
        /// Setup the MainWindow invoice display for New Invoice entry.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void muiNewInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Confirm loss of unsaved progress before leaving.
                //Confirmation is only done if the invoice is being edited.
                if (isEditing == true)
                {
                    var result = MessageBox.Show("This will cause all unsaved progress to be lost. Continue?",
                    "Lose All Unsaved Progress", MessageBoxButton.YesNo);

                    //If yes, load EditItems.
                    if (result == MessageBoxResult.Yes)
                    {
                        SetupNewInvoice();
                    }
                }
                else
                {
                    SetupNewInvoice();
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
        /// Sets up the UI for new invoice entry.
        /// </summary>
        private void SetupNewInvoice()
        {
            try
            {
                //Clear invoice number from possible previous selection.
                InvoiceNumber = "0";

                //Clear Invoice Date from possible previous selection.
                tbxInvoiceDateDay.Text = "";
                tbxInvoiceDateMonth.Text = "";
                tbxInvoiceDateYear.Text = "";

                //Enable editing mode.
                isEditing = true;

                //Setup UI for editing new invoice.
                SetupUI();

                //Set database transactions to insert (instead of update).
                newInvoice = true;

                //Display Invoice Number as TBA since no number is assigned yet.
                tbxInvoiceNumber.Text = "TBD";

                //Clear any previous invoice items from display.
                dtgInvoiceItems.ItemsSource = null;
                dtgInvoiceItems.Items.Clear();

                //Initialize InvoiceItems so items can be added to it.
                MainWindowLogic.InvoiceItems = new ObservableCollection<clsItem>();
            }
            catch (Exception ex)
            {
                //Top level method: handle the exception.
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }





        /// <summary>
        /// Called when the "Search Invoices" menu item is clicked.
        /// 
        /// Displays the SearchInvoices window.
        /// </summary>
        /// <param name="sender">Search Invoices Menu Button</param>
        /// <param name="e"></param>
        private void muiSearchInvoices_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Confirm loss of unsaved progress before leaving.
                //Confirmation is only done if the invoice is being edited.
                if (isEditing == true)
                {
                    var result = MessageBox.Show("This will cause all unsaved progress to be lost. Continue?",
                    "Lose All Unsaved Progress", MessageBoxButton.YesNo);

                    //If yes, load EditItems.
                    if (result == MessageBoxResult.Yes)
                    {
                        LoadSearchInvoices();
                    }
                }
                else
                {
                    LoadSearchInvoices();
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
        /// Loads the SearchInvoices window.
        /// </summary>
        private void LoadSearchInvoices()
        {
            try
            {
                //Clear invoice number from possible previous selection.
                //This must be done before displaying SearchInvoices as SearchInvoices can set
                //a new InvoiceNumber.
                InvoiceNumber = "0";

                //Disable editing if it was enabled.
                isEditing = false;



                //************************************************************************
                //Hardcoded InvoiceNumber is for testing only.
                //This needs to be deleted if a SearchInvoices window is created.
                //************************************************************************
                InvoiceNumber = "1";

                /*
                //************************************************************************
                //The following commented section needs to be re-enabled if a
                //SearchInvoices window is created.
                //
                //Currently, I am using a hard coded InvoiceNumber as we do not have a 
                //SearchInvoices window.
                //************************************************************************                

                //Hide MainWindow.
                this.Hide();

                //Display SearchInvoices window.
                searchInvoices = new SearchInvoices();
                searchInvoices.ShowDialog();

                //After closing SearchInvoices, return to MainWindow.
                this.Show();

                */


                //Set UI back to viewing mode, with or without selection.
                SetupUI();

                

                //Load selected invoice if one was selected.
                //If it is something other than 0, an invoice was selected.
                if (InvoiceNumber != "0")
                {
                    //Set database transactions to update (instead of insert).
                    newInvoice = false;

                    GetInvoice();

                    //Update invoice total display.
                    DisplayInvoiceTotal();
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
        /// Called when the "Edit Items" menu item is clicked.
        /// 
        /// Displays the EditItems window.
        /// </summary>
        /// <param name="sender">Edit Items Table Menu Button</param>
        /// <param name="e"></param>
        private void muiEditItems_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Confirm loss of unsaved progress before leaving.
                //Confirmation is only done if the invoice is being edited.
                if(isEditing == true)
                {
                    var result = MessageBox.Show("This will cause all unsaved progress to be lost. Continue?",
                    "Lose All Unsaved Progress", MessageBoxButton.YesNo);

                    //If yes, load EditItems.
                    if (result == MessageBoxResult.Yes)
                    {
                        LoadEditItems();
                    }
                }
                else
                {
                    LoadEditItems();
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
        /// Loads the EditItems window.
        /// </summary>
        private void LoadEditItems()
        {
            try
            {
                //Hide MainWindow.
                this.Hide();

                //Display EditItems window.
                editItems = new EditItems();
                editItems.ShowDialog();

                //After closing EditItems, return to MainWindow.
                this.Show();

                //Clear invoice number from possible previous selection.
                InvoiceNumber = "0";

                //Disable editing mode from possible previous selection.
                isEditing = false;

                //Setup UI for viewing mode, no selection.
                SetupUI();

                //Clear any previous invoice items from display.
                dtgInvoiceItems.ItemsSource = null;
                dtgInvoiceItems.Items.Clear();
            }
            catch (Exception ex)
            {
                //Throw back up.
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }




        /// <summary>
        /// Called when the "Add Item" button is clicked.
        /// 
        /// Ensures the ItemQuantity > 0 then adds the item to the InvoiceItems collection.
        /// This also displays it on the datagrid.
        /// </summary>
        /// <param name="sender">"Add Item" button.</param>
        /// <param name="e"></param>
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Stores whether the item has been added to the invoice already or not.
                //Cannot add item if itemExists == true.
                Boolean itemExists = false;

                
                //Get entered ItemQuantity.
                int ItemQuantity = 0;
                Int32.TryParse(tbxAddItemQuantity.Text, out ItemQuantity);

                //Ensure quantity is > 0.
                if (ItemQuantity == 0)
                {
                    //Alert user to increase quantity.
                    MessageBox.Show("Quantity must be greater than 0.",
                    "Quantity Error", MessageBoxButton.OK);
                }
                //Add item.
                else
                {
                    //Get selected item.
                    clsItem SelectedItem = (clsItem)cbxAddItemSelectItem.SelectedItem;


                    //See if the item has already been added.
                    foreach (clsItem ExistingItem in MainWindowLogic.InvoiceItems)
                    { 
                        //Update existing item entry.
                        if (SelectedItem.ItemCode == ExistingItem.ItemCode)
                        {
                            //Alert system that the item is already added.
                            itemExists = true;

                            //Alert user that item is already added.
                            //Confirm before updating entry.
                            var result = MessageBox.Show("This item has already been added. Update it?",
                            "Update Item", MessageBoxButton.YesNo);

                            //If yes, update the entry.
                            if (result == MessageBoxResult.Yes)
                            {
                                //Remove existing item entry.
                                MainWindowLogic.InvoiceItems.Remove(ExistingItem);

                                //Add item.
                                //Create invoice item.
                                clsItem AddItem = SelectedItem;
                                //Set quantity.
                                AddItem.ItemQuantity = ItemQuantity.ToString();
                                //Calculate and store cost.
                                AddItem.Cost = MainWindowLogic.CalculateCost(SelectedItem.ItemCode, ItemQuantity.ToString());

                                //Add invoice item to collection.
                                MainWindowLogic.InvoiceItems.Add(AddItem);

                                //Display items in datagrid.
                                dtgInvoiceItems.ItemsSource = MainWindowLogic.InvoiceItems;

                                //Update invoice total display.
                                DisplayInvoiceTotal();
                            }

                            //Stop searching for match.
                            break;
                        }
                    }


                    //Add item that is not already added.
                    if (itemExists == false)
                    {
                        //Create invoice item.
                        clsItem AddItem = SelectedItem;
                        //Set quantity.
                        AddItem.ItemQuantity = ItemQuantity.ToString();
                        //Calculate and store cost.
                        AddItem.Cost = MainWindowLogic.CalculateCost(SelectedItem.ItemCode, ItemQuantity.ToString());

                        //Add invoice item to collection.
                        MainWindowLogic.InvoiceItems.Add(AddItem);

                        //Display items in datagrid.
                        dtgInvoiceItems.ItemsSource = MainWindowLogic.InvoiceItems;

                        //Update invoice total display.
                        DisplayInvoiceTotal();
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
        /// Called when the "Edit Invoice" button is clicked.
        /// 
        /// Enables the selected invoice to be edited.
        /// This button is disabled until an invoice has been saved or selected
        /// via SearchInvoices.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Switch to editing mode instead of viewing mode.
                isEditing = true;

                //Setup UI for editing an existing invoice.
                SetupUI();
            }
            catch (Exception ex)
            {
                //Top level method: handle the exception.
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }




        /// <summary>
        /// Called when the "Save Invoice" button is clicked.
        /// 
        /// Inserts a new invoice or updates and existing one.
        /// Ensures at least 1 item has been added and a valid date entered before saving.
        /// </summary>
        /// <param name="sender">"Save Invoice" button.</param>
        /// <param name="e"></param>
        private void btnSaveInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Ensure at least 1 item has been added.
                if(dtgInvoiceItems.Items.Count < 1)
                {
                    //Alert user to add an item.
                    MessageBox.Show("Cannot save an invoice without at least 1 item.",
                    "No Items", MessageBoxButton.OK);
                }
                //Ensure all date boxes have at least 1 digit in them.
                else if (tbxInvoiceDateDay.Text.Length < 1 || tbxInvoiceDateMonth.Text.Length < 1 || tbxInvoiceDateYear.Text.Length < 1)
                {
                    //Alert user to enter a date.
                    MessageBox.Show("Cannot save an invoice without an Invoice Date.",
                    "No Date", MessageBoxButton.OK);
                }
                //Attempt an insert / update.
                else
                {
                    //Insert.
                    if (newInvoice == true)
                    {
                        //Insert using Invoice Date, store new invoice number (this will throw an exception if the date is invalid).
                        InvoiceNumber = MainWindowLogic.InsertInvoice(tbxInvoiceDateDay.Text, tbxInvoiceDateMonth.Text, tbxInvoiceDateYear.Text);

                        //Save items.
                        MainWindowLogic.SaveInvoiceItems(InvoiceNumber);
                    }
                    //Update.
                    else
                    {
                        //Update InvoiceDate (this will throw an exception if the date is invalid).
                        MainWindowLogic.UpdateInvoice(InvoiceNumber, tbxInvoiceDateDay.Text, tbxInvoiceDateMonth.Text, tbxInvoiceDateYear.Text);
                        
                        //Save items.
                        MainWindowLogic.SaveInvoiceItems(InvoiceNumber);
                    }

                    //Disable editing and refresh the UI back to viewing mode, not editing.
                    isEditing = false;
                    SetupUI();

                    //Re-display Invoice Number.
                    tbxInvoiceNumber.Text = InvoiceNumber;

                    //Re-display invoice items.
                    dtgInvoiceItems.ItemsSource = MainWindowLogic.InvoiceItems;
                }
            }
            catch (Exception ex)
            {
                //Top level method: handle the exception.
                //Alert user of invalid date entry (this is what would cause the insert/update to fail).
                MessageBox.Show("Ensure Invoice Date is formatted correctly.",
                "Insertion Error", MessageBoxButton.OK);
            }
        }




        /// <summary>
        /// Called when "Remove Selected Item" button is clicked.
        /// 
        /// Disabled until an item is selected in the datagrid.
        /// 
        /// Does not prompt user for confirmation because removed items aren't saved to the 
        /// database until the "Save Invoice" button is clicked.
        /// </summary>
        /// <param name="sender">"Remove Selected Item" button.</param>
        /// <param name="e"></param>
        private void btnRemoveSelectedItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Get selected item.
                clsItem InvoiceItem = (clsItem)dtgInvoiceItems.SelectedItem;

                //Remove from items display by removing item from collection.
                MainWindowLogic.InvoiceItems.Remove(InvoiceItem);

                //Clear combo box selection.
                cbxAddItemSelectItem.SelectedIndex = -1;
                tbxAddItemQuantity.Text = "";
                tbxAddItemCost.Text = "";

                //Disable "Add Item" button now that no item is selected.
                btnAddItem.IsEnabled = false;

                //Update invoice total display.
                DisplayInvoiceTotal();
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
            try
            {
                //Confirm before deleting.
                var result = MessageBox.Show("Are you sure you want to delete this invoice? This cannot be undone.",
                "Delete Invoice", MessageBoxButton.YesNo);

                //If yes, delete the invoice.
                if (result == MessageBoxResult.Yes)
                {
                    //Delete the selected invoice.
                    MainWindowLogic.DeleteInvoice(InvoiceNumber);


                    
                    //Clear selected InvoiceNumber.
                    InvoiceNumber = "0";

                    //Disable editing if it was enabled.
                    isEditing = false;

                    //Refresh the UI for viewing, no selection.
                    SetupUI();
                }
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
        /// Enables or disables UI elements based on the current mode (view or edit)
        /// or if an invoice has been selected.
        /// 
        /// Mode is determined by the combination of isEditing and InvoiceNumber.
        /// </summary>
        private void SetupUI()
        {
            //Clear any previous item selection.
            cbxAddItemSelectItem.SelectedIndex = -1;

            //Clear any previous invoice item selection.
            dtgInvoiceItems.SelectedIndex = -1;

            //Disable quantity entry until an item is selected.
            tbxAddItemQuantity.IsEnabled = false;

            //Editing mode for a new invoice.
            if (isEditing == true && InvoiceNumber == "0")
            {
                //Date entry.
                tbxInvoiceDateDay.IsEnabled = true;
                tbxInvoiceDateMonth.IsEnabled = true;
                tbxInvoiceDateYear.IsEnabled = true;

                //Item selection / add.
                cbxAddItemSelectItem.IsEnabled = true;
                btnAddItem.IsEnabled = false;

                //Refresh available items.
                GetItems();

                //Invoice manipulation.
                btnEditInvoice.IsEnabled = false;
                btnSaveInvoice.IsEnabled = true;
                btnRemoveSelectedItem.IsEnabled = false;
                //Cannot delete an invoice that hasn't been saved.
                btnDeleteInvoice.IsEnabled = false; 

                //Invoice display.
                dtgInvoiceItems.IsEnabled = true;
            }
            //Editing mode for an existing invoice.
            else if (isEditing == true && InvoiceNumber != "0")
            {
                //Date entry.
                tbxInvoiceDateDay.IsEnabled = true;
                tbxInvoiceDateMonth.IsEnabled = true;
                tbxInvoiceDateYear.IsEnabled = true;

                //Item selection / add.
                cbxAddItemSelectItem.IsEnabled = true;
                tbxAddItemQuantity.IsEnabled = false;
                btnAddItem.IsEnabled = false;
                btnRemoveSelectedItem.IsEnabled = false;

                //Refresh available items.
                GetItems();

                //Invoice manipulation.
                btnEditInvoice.IsEnabled = false;
                btnSaveInvoice.IsEnabled = true;                
                btnDeleteInvoice.IsEnabled = true;

                //Invoice display.
                dtgInvoiceItems.IsEnabled = true;
            }
            //Viewing mode, invoice selected.
            else if(isEditing == false && InvoiceNumber != "0")
            {
                //Clear any previous InvoiceNumber display.
                tbxInvoiceNumber.Text = "";

                //Date entry.
                tbxInvoiceDateDay.IsEnabled = false;
                tbxInvoiceDateMonth.IsEnabled = false;
                tbxInvoiceDateYear.IsEnabled = false;

                //Item selection / add.
                cbxAddItemSelectItem.IsEnabled = false;
                tbxAddItemQuantity.IsEnabled = false;
                btnAddItem.IsEnabled = false;
                btnRemoveSelectedItem.IsEnabled = false;

                //Invoice manipulation.
                btnEditInvoice.IsEnabled = true;
                btnSaveInvoice.IsEnabled = false;                
                btnDeleteInvoice.IsEnabled = true;


                //Clear any previous invoice items from display.
                dtgInvoiceItems.ItemsSource = null;
                dtgInvoiceItems.Items.Clear();

                //Enable items display.
                dtgInvoiceItems.IsEnabled = true;
            }
            //Viewing mode, NO invoice selected.
            else
            {
                //Clear any previous InvoiceNumber display.
                tbxInvoiceNumber.Text = "";

                //Clear and previous InvoiceTotal display.
                tbxInvoiceTotal.Text = "";


                //Clear any previous InvoiceDate display.                
                tbxInvoiceDateDay.Text = "";
                tbxInvoiceDateMonth.Text = "";
                tbxInvoiceDateYear.Text = "";

                //Disable Date entry.
                tbxInvoiceDateDay.IsEnabled = false;
                tbxInvoiceDateMonth.IsEnabled = false;
                tbxInvoiceDateYear.IsEnabled = false;


                //Disable Item selection / add.
                cbxAddItemSelectItem.IsEnabled = false;
                tbxAddItemQuantity.IsEnabled = false;
                btnAddItem.IsEnabled = false;
                btnRemoveSelectedItem.IsEnabled = false;


                //Disable Invoice manipulation.
                btnEditInvoice.IsEnabled = false;
                btnSaveInvoice.IsEnabled = false;                
                btnDeleteInvoice.IsEnabled = false;


                //Clear any previous items from display.
                dtgInvoiceItems.ItemsSource = null;
                dtgInvoiceItems.Items.Clear();

                //Disable items display.
                dtgInvoiceItems.IsEnabled = false;
            }
        }



        
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
                //Disable "Remove Selected Item" button until the item is selected from the datagrid.
                btnRemoveSelectedItem.IsEnabled = false;

                //Enable quantity entry now that an item is selected.
                tbxAddItemQuantity.IsEnabled = true;

                //Enable "Add Item" button now that an item is selected.
                btnAddItem.IsEnabled = true;

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
                {
                    //Get selected item.
                    clsItem Item = (clsItem)cbxAddItemSelectItem.SelectedItem;

                    //Display calculated cost.
                    tbxAddItemCost.Text = "$" + MainWindowLogic.CalculateCost(Item.ItemCode, tbxAddItemQuantity.Text);
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
        /// Calculates the cost when quantity entry loses focus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxAddItemQuantity_LostFocus_1(object sender, RoutedEventArgs e)
        {
            try
            {
                //Get selected item.
                clsItem Item = (clsItem)cbxAddItemSelectItem.SelectedItem;

                //Display calculated cost.
                tbxAddItemCost.Text = "$" + MainWindowLogic.CalculateCost(Item.ItemCode, tbxAddItemQuantity.Text);
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
        /// Handles selecting an item from the invoice.
        /// 
        /// Puts the selected item into the add item section for editing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtgInvoiceItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //Disable "Remove Selected Item" button.
                //This ensures the button is disabled when an item is not selected.
                btnRemoveSelectedItem.IsEnabled = false;


                //Get item from selected row.
                clsItem InvoiceItem = (clsItem)dtgInvoiceItems.SelectedItem;

                //Ensure an item is selected.
                if(InvoiceItem != null)
                {
                    //Find matching item in cbxAddItemSelectItem.
                    foreach (clsItem Item in cbxAddItemSelectItem.Items)
                    {
                        if (Item.ToString() == InvoiceItem.ToString())
                        {
                            //Select matching item.
                            cbxAddItemSelectItem.SelectedItem = Item;

                            //Update quantity to the one stored in InvoiceItem.
                            tbxAddItemQuantity.Text = InvoiceItem.ItemQuantity;

                            //Recalculate cost display.
                            tbxAddItemCost.Text = "$" + MainWindowLogic.CalculateCost(Item.ItemCode, tbxAddItemQuantity.Text);

                            //Stop checking once match is found.
                            break;
                        }
                    }


                    //Enable "Remove Selected Item" button.
                    //Ensures editing is enabled and an item is selected.
                    if(isEditing == true && InvoiceItem != null)
                    {
                        btnRemoveSelectedItem.IsEnabled = true;
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
        /// Called when returning from SearchInvoices if an invoice was selected
        /// to populate InvoiceNumber, InvoiceDate, and dtgInvoiceItems.
        /// </summary>
        private void GetInvoice()
        {
            try
            {
                //Create Invoice object.
                MainWindowLogic.GetInvoice(InvoiceNumber);

                //Display InvoiceNumber and InvoiceDate.
                tbxInvoiceNumber.Text = MainWindowLogic.Invoice.InvoiceNumber;


                //Store date for substringing.
                String tempInvoiceDate = MainWindowLogic.Invoice.InvoiceDate;

                //Get day.
                tbxInvoiceDateDay.Text = tempInvoiceDate.Substring(0, MainWindowLogic.Invoice.InvoiceDate.IndexOf("/"));
                
                //Remove day and time from string.
                tempInvoiceDate = tempInvoiceDate.Substring(tempInvoiceDate.IndexOf("/") + 1, tempInvoiceDate.IndexOf(" ") - 1);
                //Get month.
                tbxInvoiceDateMonth.Text = tempInvoiceDate.Substring(0, tempInvoiceDate.IndexOf("/"));

                //Remove month from string.
                tempInvoiceDate = tempInvoiceDate.Substring(tempInvoiceDate.IndexOf("/") + 1);
                //Get year.
                tbxInvoiceDateYear.Text = tempInvoiceDate;


                //Build InvoiceItems collection.
                MainWindowLogic.GetInvoiceItems(InvoiceNumber);

                //Bind ItemsSource.
                dtgInvoiceItems.ItemsSource = MainWindowLogic.InvoiceItems;
            }
            catch (Exception ex)
            {
                //Top level method: handle the exception.
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }




        /// <summary>
        /// Called when an invoice is loaded, an item is added, and when an item is removed.
        /// 
        /// Totals all the InvoiceItem costs then displays them.
        /// </summary>
        private void DisplayInvoiceTotal()
        {
            try
            {
                //Display total cost.
                tbxInvoiceTotal.Text = "$" + MainWindowLogic.CalculateInvoiceTotal();
            }
            catch (Exception ex)
            {
                //Throw back up.
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
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
