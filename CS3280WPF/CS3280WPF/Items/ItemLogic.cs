using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3280WPF.Items
{
    class ItemLogic
    {
        clsDataAccess dataAccess;
        DataSet itemSet;
        int numOfElements;
        string tableName;
        // List<List<String>> dataElements;

        public ItemLogic()
        {
            tableName = "ItemDesc";
            dataAccess = new clsDataAccess();
            getNewData();
        }

        public DataSet getNewData()
        {
            
            itemSet = dataAccess.ExecuteSQLStatement("SELECT * FROM [" + tableName + "]", ref numOfElements);
 
            return itemSet;
        }
        public void setItem(string itemCode, string itemDescription, double cost)
        {
            if(!(itemDescription.Length>0) || (cost<=0))
            {
                throw new Exception("Incorrect format. Please check your values again.");
            }
            dataAccess.ExecuteScalarSQL("UPDATE [" + tableName + "] SET ItemDesc = '" + itemDescription +
                "', Cost =" + cost + " WHERE ItemCode ='" + itemCode + "'");
        }

        public int getSize()
        {
            return numOfElements;
        }

        internal void addItem(string itemCode, string itemDescription, double cost)
        {
            if ((itemDescription.Length == 0) || (cost <= 0) || !(itemCode.Length==0))
            {
                throw new Exception("Incorrect format. Please check your values again.");
            }
            dataAccess.ExecuteScalarSQL("INSERT INTO [" + tableName + "] (ItemCode, ItemDesc, Cost)" +
            " VALUES '" + itemCode + "','" + itemDescription + "'," + cost + "");
        }
    }
}
