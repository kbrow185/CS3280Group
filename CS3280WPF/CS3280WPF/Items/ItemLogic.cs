using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Handles logic for edit items window. Connects and updates database
/// </summary>
namespace CS3280WPF.Items
{
    class ItemLogic
    {
        clsDataAccess dataAccess;
        DataSet itemSet;
        int numOfElements;
        string tableName;
        int invoiceInquiryLength;
        ObservableCollection<string> itemCodes;

        /// <summary>
        /// Constructtor, sets up table name and initializes clsDataAccess as well as initializes dataset.
        /// </summary>
        public ItemLogic()
        {
            itemCodes = new ObservableCollection<string>();
            invoiceInquiryLength = 0;
            tableName = "ItemDesc";
            dataAccess = new clsDataAccess();
            getNewData();
            for (int i = 0; i < numOfElements; i++)
            {
                itemCodes.Add(Convert.ToString(itemSet.Tables[0].Rows[i][0]));
            }
        }
        /// <summary>
        /// Initializes dataset Itemset and gathers database information.
        /// </summary>
        /// <returns></returns>
        public DataSet getNewData()
        {
            itemSet = dataAccess.ExecuteSQLStatement("SELECT * FROM [" + tableName + "]", ref numOfElements);
            return itemSet;
        }

        /// <summary>
        /// Checks whether fields are correct to update the item. If not an exception is thrown,
        /// otherwise the item is udpdated.
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="itemDescription"></param>
        /// <param name="cost"></param>
        public void setItem(string itemCode, string itemDescription, double cost)
        {
            if(!(itemDescription.Length>0) || (cost<=0))
            {
                throw new Exception("Incorrect format. Please check your values again.");
            }
            dataAccess.ExecuteScalarSQL("UPDATE [" + tableName + "] SET ItemDesc = '" + itemDescription +
                "', Cost =" + cost + " WHERE ItemCode ='" + itemCode + "'");
            getNewData();
        }
        /// <summary>
        /// returns size of database.
        /// </summary>
        /// <returns></returns>
        public int getSize()
        {
            return numOfElements;
        }
        /// <summary>
        /// Checks whether fields are correct to add the item. If not an exception is thrown,
        /// otherwise the item is added to the list then updated.
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="itemDescription"></param>
        /// <param name="cost"></param>
        internal void addItem(string itemCode, string itemDescription, double cost)
        {
            if ((itemDescription.Length == 0) || (cost <= 0) || (itemCode.Length==0))
            {
                throw new Exception("Incorrect format. Please check your values again.");
            }
            dataAccess.ExecuteScalarSQL("INSERT INTO [" + tableName + "] (ItemCode, ItemDesc, Cost)" +
            " VALUES ('" + itemCode + "','" + itemDescription + "'," + cost + ")");
            itemCodes.Add(itemCode);
            getNewData();
        }
        /// <summary>
        /// First checks if item code is utilized elesewhere in the database. 
        /// If it is then an exception is thrown with every invoice found with that itemcode.
        /// Otherwise the item is removed from the database.
        /// </summary>
        /// <param name="itemCode"></param>
        public void removeItem(string itemCode)
        {
            
            DataSet invoices = dataAccess.ExecuteSQLStatement("SELECT * FROM LineItems WHERE ItemCode ='" +itemCode +"'", ref invoiceInquiryLength);
            if(invoiceInquiryLength==0)
            {
                dataAccess.ExecuteScalarSQL("DELETE FROM [" + tableName + "] WHERE itemCode = '" + itemCode+"'");
                itemCodes.Remove(itemCode);
                getNewData();
            }
            else
            {
                String usedList = "";

                for(int i = 0; i<invoiceInquiryLength; i++)
                {
                    usedList += (invoices.Tables[0].Rows[i][0] + ", ");
                }

                throw new Exception("Unable to delete inquiry. Item used by invoice/s: " + usedList);
            }


         
        }
        public bool checkIfItemExists(string itemCode)
        {
            return itemCodes.Contains(itemCode);
        }
    }
}
