/***************************************************************************************************
* Group5Assignment
* clsMainLogic.cs
* Dongmin Kim, Kyle Kippen, Goeun Kwak
* CS3280 Group assignment - Jewelry Invoice.
*
***************************************************************************************************/


using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Group6Assignment.Main
{
    public class clsMainLogic
    {

        /// <summary>
        /// Variable to manage SQL.
        /// </summary>
        private clsMainSQL clsMainSQL;

        //private ItemDescInfo itemDescInfo;

        private clsDataAccess dataAccess;


        /// <summary>
        /// Variable to hold current ItemDesc list user selected. 
        /// </summary>
        private List<ItemDescInfo> listItemUserSelected;

        private decimal totalCostCal = 0;

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
        public List<ItemDescInfo> GetAllItemDesc()
        {
            dataAccess = new clsDataAccess();            
            DataSet ds = new DataSet();
            ItemDescInfo itemDescInfo;
            int iRet = 0;

            ds = dataAccess.ExecuteSQLStatement(clsMainSQL.SQLGetAllItemDesc(), ref iRet);

            List<ItemDescInfo> itemDescInfoList = new List<ItemDescInfo>();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                itemDescInfo = new ItemDescInfo();

                itemDescInfo.ItemCode = ds.Tables[0].Rows[i][0].ToString();
                itemDescInfo.ItemDesc = ds.Tables[0].Rows[i][1].ToString();
                itemDescInfo.Cost = (decimal)ds.Tables[0].Rows[i][2];
               
                itemDescInfoList.Add(itemDescInfo);               
            }

            return itemDescInfoList;

        }


        /// <summary>
        /// This method adds items in datagrid.
        /// </summary>
        public void AddItemToInvoice(ItemDescInfo selectedItem)
        {
            listItemUserSelected = new List<ItemDescInfo>();

            listItemUserSelected.Add(selectedItem);
            CalculateTotal(selectedItem);
        }


        public List<ItemDescInfo> GetAddedItem()
        {

            return listItemUserSelected;
        }


        /// <summary>
        /// This method calculates total cost of items user added in the grid list.
        /// </summary>
        private void CalculateTotal(ItemDescInfo calulateCost)
        {
            totalCostCal += calulateCost.Cost;
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

            /// <summary>
            /// Total cost of the Invoice.
            /// </summary>
            public decimal TotalCost { get; set; }

        }


        /// <summary>
        /// This class stores Invoice description from database
        /// </summary>
        public class ItemDescInfo
        {
            /// <summary>
            /// Item code of item.
            /// </summary>
            public string ItemCode { get; set; }

            /// <summary>
            /// Description of item.
            /// </summary>
            public string ItemDesc { get; set; }

            /// <summary>
            /// Cost of item.(Currency type in Database)
            /// </summary>
            public decimal Cost { get; set; }


            public override string ToString()
            {
                return "(" + ItemCode + ") - " + ItemDesc + " - $" + String.Format("{0:0.00}", Cost);
            }
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
