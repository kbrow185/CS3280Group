using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CS3280WPF
{
    /// <summary>
    /// Holds data for an item.
    /// </summary>
    public class clsItem
    {
        #region Attribute(s)
        /// <summary>
        /// ItemCode from the database.
        /// </summary>
        public String ItemCode;

        /// <summary>
        /// ItemDesc from the database.
        /// </summary>
        public String ItemDesc;

        /// <summary>
        /// Cost from the database.
        /// </summary>
        public String Cost;
        #endregion





        #region Method(s)   

        /// <summary>
        /// Used to display the ItemCode and ItemDesc in cbxSelectItem.
        /// </summary>
        /// <returns>Flight_Number</returns>
        public override string ToString()
        {
            try
            {
                return ItemCode + " - " + ItemDesc;
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
