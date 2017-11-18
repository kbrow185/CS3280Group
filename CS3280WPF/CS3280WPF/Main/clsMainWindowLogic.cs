using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;

namespace CS3280WPF.Main
{
    /// <summary>
    /// Business logic behind the MainWindow.
    /// </summary>
    public class clsMainWindowLogic
    {
        /* TODO / Notes:
         * When returning from SearchInvoices, pass in an InvoiceNum then populate the 
         * form with the selected invoice.
         */



        #region Attribute(s)
        /// <summary>
        /// Data access class. Used to query the Reservation database.
        /// </summary>
        clsDataAccess db;

        /// <summary>
        /// Holds an SQL statement.
        /// </summary>
        string sSQL;

        /// <summary>
        /// Number of return values from SQL query.
        /// </summary>
        int iRet = 0;

        /// <summary>
        /// Holds the return values from SQL query.
        /// </summary>
        DataSet ds = new DataSet();



        /// <summary>
        /// Instances of this are created from databse information then loaded into its
        /// corresponding collection.
        /// </summary>
        clsItem Item;

        /// <summary>
        /// Collection of items from the database.
        /// </summary>
        public ObservableCollection<clsItem> Items;

        #endregion




        /// <summary>
        /// Constructor to initialize the database.
        /// </summary>
        public clsMainWindowLogic()
        {
            try
            {
                //Initialize database.
                db = new clsDataAccess();
            }
            catch (Exception ex)
            {
                //Throw back up.
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        



        #region Method(s)

        /// <summary>
        /// Gets all items from the table Item in the database.
        /// Stores them in ObservableCollection Items.
        /// </summary>
        public void GetItems()
        {
            try
            {
                //Initialize / clear collection of Items.
                Items = new ObservableCollection<clsItem>();

                //Create the SQL statement to extract the flights from the database.
                sSQL = "SELECT ItemCode, ItemDesc, Cost " +
                       "FROM Item ";

                //Extract the flights and put them into the DataSet
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                //Loop through the data and create item classes.
                for (int i = 0; i < iRet; i++)
                {
                    Item = new clsItem();
                    Item.ItemCode = ds.Tables[0].Rows[i][0].ToString();
                    Item.ItemDesc = ds.Tables[0].Rows[i][1].ToString();
                    Item.Cost = ds.Tables[0].Rows[i][2].ToString();

                    //Add to collection.
                    Items.Add(Item);
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
        /// Calculates the cost of an item from it's initial cost * quanitity.
        /// </summary>
        /// <param name="cost">Cost of 1 of the selected item as stored in the database.</param>
        /// <param name="quantity">Selected by the user.</param>
        /// <returns></returns>
        public double CalculateCost(int quantity, double cost)
        {
            try
            {
                return cost * quantity;
            }
            catch (Exception ex)
            {
                //Throw back up.
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        #endregion
    }
}
