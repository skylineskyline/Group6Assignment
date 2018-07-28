using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group6Assignment.Main
{
    public class clsMainLogic
    {

        /// <summary>
        /// Variable to manage SQL.
        /// </summary>
        private clsMainSQL clsMainSQL;


        /// <summary>
        /// Variable to hold current ItemDesc list user selected. 
        /// </summary>
        private List<ItemDesc> listItemDesc;


        /// <summary>
        /// Constructor
        /// </summary>
        public clsMainLogic()
        {
            clsMainSQL = new clsMainSQL();
        }


        /// <summary>
        /// This method gets all records from ItemDesc table for showing up the item list in item combo box.
        /// </summary>
        public void GetAllItemDesc()
        {
            clsMainSQL.SQLGetAllItemDesc();
        }






        /// <summary>
        /// This method adds items in datagrid.
        /// </summary>
        public void AddInvoiceItem(ItemDesc selectedItem)
        {

        }


        /// <summary>
        /// This method calculates total cost of items user added in the grid list.
        /// </summary>
        private void CalculateTotal()
        {
           
        }



        /// <summary>
        /// This method edits Invoice items user input.
        /// </summary>
        public void EditInvoice()
        {

        }


        /// <summary>
        /// This method deletes Invoice items user input from database.
        /// </summary>
        public void DeleteInvoice()
        {

        }


        /// <summary>
        /// This method saves Invoice items user selected.
        /// </summary>
        public void SaveInvoice()
        {

        }


        /// <summary>
        /// This class stores Invoices info from database. 
        /// </summary>
        public class Invoices
        {

            /// <summary>
            /// Invoice number of invoice. (Primary, AutoNumber set).
            /// </summary>
            public int InvoiceNumber { get; set; }

            /// <summary>
            /// Invoice date of invoice.
            /// </summary>
            public DateTime InvoiceDate { get; set; }
            
        }


        /// <summary>
        /// This class stores Invoice description from database
        /// </summary>
        public class ItemDesc
        {
            /// <summary>
            /// Item code of item.
            /// </summary>
            public string ItemCode { get; set; }

            /// <summary>
            /// Description of item.
            /// </summary>
            public string ItemDescription { get; set; }

            /// <summary>
            /// Cost of item.(Currency type in Database)
            /// </summary>
            public decimal Cost { get; set; }
        }



        /// <summary>
        /// his class stores LineItems from database
        /// </summary>
        /// </summary>
        public class LineItmes
        {
            /// <summary>
            /// Invoice number of invoice. (FK-identify it from Invoices table, NOT AutoNumber set).
            /// </summary>
            public int InvoiceNum { get; set; }

            /// <summary>
            /// Line Item Number which corresponds to the position the item is in the dategrid.
            /// </summary>
            public int LineItemNum { get; set; }


            /// <summary>
            /// code of Item (FK-identify from ItemDesc table)
            /// </summary>
            public string ItemCode { get; set; }
        }

    }
}
