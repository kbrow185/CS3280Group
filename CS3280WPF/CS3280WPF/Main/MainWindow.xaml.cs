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
using CS3280WPF.Items;
using CS3280WPF.Search;
using System.Reflection;


/* Kyler's TODO / notes:
 * Create invoice display.
 * 
 * Clicking "New Invoice" should prompt the user to save the currently selected invoice,
 * if applicable.
 * 
 * New Invoice and displaying an invoice can be done in the same display.
 * 
 * Delete Invoice needs to prompt the user before deleting.
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
        /// Called when the "Edit Items Table" menu button is clicked.
        /// 
        /// Displays the EditItems window.
        /// </summary>
        /// <param name="sender">Edit Items Table Menu Button</param>
        /// <param name="e"></param>
        private void muiEditItemsTable_Click(object sender, RoutedEventArgs e)
        {
            /* Notes:
             * After closing, refresh the items in a currently selected invoice, if there is one.
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
            /* Notes:
             * SearchInvoices should return the Invoice_ID of the selected invoice to MainWindow for display.
             */

            try
            {
                //Hide MainWindow.
                this.Hide();

                //Display SearchInvoices window.
                searchInvoices = new SearchInvoices();
                searchInvoices.ShowDialog();

                //After closing SearchInvoices, return to MainWindow.
                this.Show();
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
