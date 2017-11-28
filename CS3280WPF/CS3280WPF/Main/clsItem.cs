using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CS3280WPF
{
    /// <summary>
    /// Holds data for an item.
    /// </summary>
    public class clsItem : INotifyPropertyChanged
    {
        #region Attribute(s)
        /// <summary>
        /// ItemCode from the database.
        /// </summary>
        String _ItemCode;

        /// <summary>
        /// Property for ItemCode from the database.
        /// </summary>
        public String ItemCode
                {
            get
            {
                return _ItemCode;
            }

            set
            {
                _ItemCode = value;

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ItemCode"));
                }
            }
        }

        /// <summary>
        /// ItemDesc from the database.
        /// </summary>
        String _ItemDesc;

        /// <summary>
        /// Property for ItemDesc from the database.
        /// </summary>
        public String ItemDesc
        {
            get
            {
                return _ItemDesc;
            }

            set
            {
                _ItemDesc = value;

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ItemDesc"));
                }
            }
        }

        /// <summary>
        /// ItemQuantity from InvoiceItem table.
        /// Only used in the InvoiceItems collection, not the Items collection.
        /// </summary>
        String _ItemQuantity;

        /// <summary>
        /// Property for ItemQuantity from InvoiceItem table.
        /// Only used in the InvoiceItems collection, not the Items collection.
        /// </summary>
        public String ItemQuantity
        {
            get
            {
                return _ItemQuantity;
            }

            set
            {
                _ItemQuantity = value;

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ItemQuantity"));
                }
            }
        }

        /// <summary>
        /// Cost from the database.
        /// Or calculated cost from cost * quantity.
        /// </summary>
        String _Cost;

        /// <summary>
        /// Property for Cost from the database.
        /// Or calculated cost from cost * quantity.
        /// </summary>
        public String Cost
        {
            get
            {
                return _Cost;
            }

            set
            {
                _Cost = value;

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Cost"));
                }
            }
        }

        #endregion





        #region Method(s)  

        /// <summary>
        /// This is the contract you make with the compiler because we are implementing the interface so
        /// we must have this event defined.  We will raise this event anytime one of our properties changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        



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
