using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace CS3280WPF
{
    /// <summary>
    /// Business logic behind the MainWindow.
    /// </summary>
    public class clsMainWindowLogic : INotifyPropertyChanged
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
        /// Collection of all items available from the Item table in the database.
        /// </summary>
        public ObservableCollection<clsItem> Items;

        /// <summary>
        /// Collection of items from a specified invoice from the InvoiceItem table in the database.
        /// </summary>
        ObservableCollection<clsItem> _InvoiceItems;

        public ObservableCollection<clsItem> InvoiceItems
        {
            get
            {
                return _InvoiceItems;
            }

            set
            {
                _InvoiceItems = value;

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("InvoiceItems"));
                }
            }
        }


        /// <summary>
        /// Represents the currently selected invoice from the database.
        /// </summary>
        public clsInvoice Invoice;

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
        /// This is the contract you make with the compiler because we are implementing the interface so
        /// we must have this event defined.  We will raise this event anytime one of our properties changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;




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

                //Create the SQL statement to extract the items from the database.
                sSQL = "SELECT ItemCode, ItemDesc, Cost " +
                       "FROM Item ";

                //Extract the items and put them into the DataSet.
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
        /// Gets all items for the specified invoice from the table InvoiceItem in the database.
        /// Stores them in ObservableCollection InvoiceItems.
        /// </summary>
        public void GetInvoiceItems(String InvoiceNumber)
        {
            try
            {
                //Initialize / clear collection of InvoiceItems.
                InvoiceItems = new ObservableCollection<clsItem>();

                //Create the SQL statement to extract the ItemCodes for the InvoiceItems from the database.
                sSQL = "SELECT ItemCode, ItemQuantity " +
                       "FROM InvoiceItem " +
                       "WHERE InvoiceNumber = " + InvoiceNumber;

                //Extract the ItemCodes and put them into the DataSet.
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                //Stores individual items that are retrieved via their code.
                DataSet tempData = new DataSet();
                //Loop through the data and get the corresponding items from the Item table.
                for (int i = 0; i < iRet; i++)
                {
                    //Get current row ItemCode.
                    String tempItemCode = ds.Tables[0].Rows[i][0].ToString();

                    //Create the SQL statement to extract the Items for the ItemCodes for the InvoiceItems.
                    //Must have ' ' around ItemCode to indicate that it is a String.
                    sSQL = "SELECT ItemCode, ItemDesc, Cost " +
                           "FROM Item " +
                           "WHERE ItemCode = '" + tempItemCode + "'";

                    //Get corresponding item using the item codes.
                    int tempiRet = 0;
                    tempData = db.ExecuteSQLStatement(sSQL, ref tempiRet);


                    //Get invoice's quantity for current item.
                    //Used to calculate total cost: cost * quantity.
                    int tempQuantity = 1;
                    Int32.TryParse(ds.Tables[0].Rows[i][1].ToString(), out tempQuantity);

                    //Get default cost for current item.
                    //Used to calculate total cost: cost * quantity.
                    double tempCost = 0;
                    double.TryParse(tempData.Tables[0].Rows[0][2].ToString(), out tempCost);


                    //Create Item from returned values.
                    Item = new clsItem();
                    Item.ItemCode = tempData.Tables[0].Rows[0][0].ToString();
                    Item.ItemDesc = tempData.Tables[0].Rows[0][1].ToString();
                    Item.ItemQuantity = tempQuantity.ToString();
                    Item.Cost = (tempQuantity * tempCost).ToString();

                    //Add Item to collection.
                    InvoiceItems.Add(Item);
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
        /// Creates an invoice object from the specified invoice number.
        /// Used to display an invoice selected in SearchInvoices back in MainWindow.
        /// </summary>
        /// <param name="InvoiceNumber"></param>
        public void GetInvoice(String InvoiceNumber)
        {
            try
            {
                //Create the SQL statement to extract the Invoice from the database.
                sSQL = "SELECT InvoiceNumber, InvoiceDate " +
                       "FROM Invoice " +
                       "WHERE InvoiceNumber = " + InvoiceNumber;

                //Extract the invoice and put it into the DataSet.
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                //Only 1 row will be returned, so access that row's data directly.
                Invoice = new clsInvoice();
                Invoice.InvoiceNumber = ds.Tables[0].Rows[0][0].ToString();
                Invoice.InvoiceDate = ds.Tables[0].Rows[0][1].ToString();
            }
            catch (Exception ex)
            {
                //Throw back up.
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }




        /// <summary>
        /// Deletes the invoice from the database.
        /// </summary>
        /// <param name="InvoiceNumber"></param>
        public void DeleteInvoice(String InvoiceNumber)
        {
            try
            {
                //Create the SQL statement to delete the Invoice from the Invoice table.
                sSQL = "DELETE " +
                       "FROM Invoice " +
                       "WHERE InvoiceNumber = " + InvoiceNumber;

                //Execute the sql to delete the invoice from the Invoice table.
                db.ExecuteNonQuery(sSQL);



                //Create the SQL statement to delete all items for the Invoice from the InvoiceItem table.
                sSQL = "DELETE " +
                       "FROM InvoiceItem " +
                       "WHERE InvoiceNumber = " + InvoiceNumber;

                //Execute the sql to delete the InvoiceItems from the InvoiceItem table.
                db.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                //Throw back up.
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }




        /// <summary>
        /// Insert is done for a new invoice when the "Save Invoice" button is clicked.
        /// 
        /// Creates a new invoice using the InvoiceDate, returns the InvoiceNumber of the 
        /// newly created invoice.
        /// </summary>
        /// <param name="Day"></param>
        /// <param name="Month"></param>
        /// <param name="Year"></param>
        /// <returns></returns>
        public String InsertInvoice(String Day, String Month, String Year)
        {
            try
            {
                //Concatonate InvoiceDate entry into one insert statement.
                sSQL = "INSERT INTO Invoice(InvoiceDate) " +
                       "VALUES('" + Day + "/" + Month + "/" + Year + "')";


                //Get and return newly inserted invoice's InvoiceNumber by getting the last InvoiceNumber.
                String InvoiceNumber;

                //Create the SQL statement to get the new InvoiceNumber.
                sSQL = "SELECT TOP 1 InvoiceNumber " +
                       "FROM Invoice " +
                       "ORDER BY InvoiceNumber DESC";

                //Store the new InvoiceNumber from the database.
                InvoiceNumber = db.ExecuteScalarSQL(sSQL).ToString();

                return InvoiceNumber;
            }
            catch (Exception ex)
            {
                //Throw back up.
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }
        



        /// <summary>
        /// Update is done for an existing invoice when the "Save Invoice" button is clicked.
        /// 
        /// Updates the InvoiceDate for an existing invoice in the database.
        /// </summary>
        /// <param name="InvoiceNumber"></param>
        /// <param name="Day"></param>
        /// <param name="Month"></param>
        /// <param name="Year"></param>
        public void UpdateInvoice(String InvoiceNumber, String Day, String Month, String Year)
        {
            try
            {
                //Concatonate InvoiceDate entry into one update statement.
                sSQL = "UPDATE Invoice " +
                       "SET InvoiceDate = '" + Day + "/" + Month + "/" + Year + "' " +
                       "WHERE InvoiceNumber = " + InvoiceNumber;

                //Execute the SQL to update the InvoiceDate..
                db.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                //Throw back up.
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }




        /// <summary>
        /// Called by SaveInvoiceItems when "Save Invoice" button is clicked.
        /// Deletes items that were removed from the invoice from the InvoiceItems table.
        /// </summary>
        public void DeleteInvoiceItems(String InvoiceNumber)
        {            
            try
            {
                //Create the SQL statement to delete the items from the InvoiceItems table.
                sSQL = "DELETE " +
                       "FROM InvoiceItem " +
                       "WHERE InvoiceNumber = " + InvoiceNumber;

                //Execute the sql to delete the items from the InvoiceItems table.
                db.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                //Throw back up.
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// Insert is done for a new invoice when the "Save Invoice" button is clicked.
        /// </summary>
        /// <param name="InvoiceNumber"></param>
        public void InsertInvoiceItems(String InvoiceNumber)
        {
            try
            {
                //Only attempt insert if there are items.
                if(InvoiceItems.Count > 0)
                {
                    //Insert each item.
                    foreach(clsItem Item in InvoiceItems)
                    {
                        //Create the SQL statement to insert the item into the InvoiceItems table.
                        sSQL = "INSERT INTO InvoiceItem(InvoiceNumber, ItemCode, ItemQuantity) " +
                               "VALUES('" + InvoiceNumber + "', '" + Item.ItemCode + "', '" + Item.ItemQuantity + "')";

                        //Execute the sql to insert the item into the InvoiceItem table.
                        db.ExecuteNonQuery(sSQL);
                    }                    
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
        /// Clear all existing item entries for the invoice.
        /// 
        /// Insert all items for in the datagrid for the invoice.
        /// </summary>
        /// <param name="InvoiceNumber"></param>
        public void SaveInvoiceItems(String InvoiceNumber)
        {
            try
            {
                //Delete previous item entries.
                DeleteInvoiceItems(InvoiceNumber);

                //Insert items.
                InsertInvoiceItems(InvoiceNumber);
            }
            catch (Exception ex)
            {
                //Throw back up.
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        



        /// <summary>
        /// Gets the cost of 1 of the specified item from the Item table in the database.
        /// </summary>
        /// <param name="ItemCode"></param>
        /// <returns></returns>
        public double GetCost(String ItemCode)
        {
            try
            {
                //Create the SQL statement to get the Cost.
                sSQL = "SELECT Cost " +
                       "FROM Item " +
                       "WHERE ItemCode = '" + ItemCode + "'";

                //Store the cost from the database.
                double Cost = 0.00;
                Double.TryParse(db.ExecuteScalarSQL(sSQL).ToString(), out Cost);

                return Cost;
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
        public String CalculateCost(String ItemCode, String ItemQuantity)
        {
            try
            {
                //Get cost of 1 of the item.
                double Cost = GetCost(ItemCode);

                //Parse ItemQuantity.
                int Quantity = 0;
                Int32.TryParse(ItemQuantity, out Quantity);

                //Calculate total cost for the item.
                return (Cost * Quantity).ToString();
            }
            catch (Exception ex)
            {
                //Throw back up.
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }




        /// <summary>
        /// Adds the cost of all the InvoiceItems together.
        /// </summary>
        /// <returns>String InvoiceTotal.</returns>
        public String CalculateInvoiceTotal()
        {
            try
            {
                //Stores the sum of all invoice item costs.
                double InvoiceTotal = 0.00;

                //Ensure InvoiceItems has been initialized.
                if (InvoiceItems != null)
                {
                    //Add each cost.
                    foreach (clsItem InvoiceItem in InvoiceItems)
                    {
                        //Stores the cost of the currently looped to item.
                        double Cost = 0.00;

                        //Parse and store cost.
                        double.TryParse(InvoiceItem.Cost, out Cost);

                        //Sum
                        InvoiceTotal += Cost;
                    }
                }

                return InvoiceTotal.ToString();
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
