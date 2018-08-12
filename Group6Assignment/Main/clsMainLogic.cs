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


        private List<LineItmes> listLineItmes;

        /// <summary>
        /// Variable to hold total cost.
        /// </summary>
        private decimal totalCostCal = 0;
        

        /// <summary>
        /// Variable and Property to hold current Invocie.
        /// </summary>
        public Invoices CurrentInvoice { get; set; }


        /// <summary>
        /// Constructor
        /// </summary>
        public clsMainLogic(Invoices newInvoice)
        {
            
            try
            {
                clsMainSQL = new clsMainSQL();            
                
                //If newInvoice is null, creat the new invoice
                if (newInvoice == null)
                {
                    CurrentInvoice = new Invoices();
                    //Greate the new invoice's list.
                    listItemUserSelected = new List<ItemDescInfo>();
                }

                else
                {
                    CurrentInvoice = new Invoices();                    
                    CurrentInvoice.InvoiceNumber = newInvoice.InvoiceNumber;
                    CurrentInvoice.InvoiceDate = newInvoice.InvoiceDate;
                }

            }
            catch (Exception)
            {

                throw;
            }
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
        /// Get LineItems.
        /// </summary>
        public void GetLineItmes(int invoiceNum)
        {
            //listLineItmes = new List<LineItmes>();            
            listItemUserSelected = new List<ItemDescInfo>();
            
            dataAccess = new clsDataAccess();
            DataSet ds = new DataSet();
            int iRet = 0;

            ds = dataAccess.ExecuteSQLStatement(clsMainSQL.SQLGetCurrentInvoiceLineItems(invoiceNum), ref iRet);
            
            foreach (DataRow datas in ds.Tables[0].Rows)
            {
                listItemUserSelected.Add(new ItemDescInfo()
                {
                    ItemCode = (string)datas[0],
                    ItemDesc = (string)datas[1],
                    Cost = (decimal)datas[2]
                });
            }

            totalCostCal = 0;
            foreach (var cost in listItemUserSelected)
            {
                totalCostCal += cost.Cost;
            }
        }


        /// <summary>
        /// This method adds items in datagrid.
        /// </summary>
        public void AddItemToInvoice(ItemDescInfo selectedItem)
        {
            listItemUserSelected.Add(selectedItem);
            totalCostCal += selectedItem.Cost;            
        }



        /// <summary>
        /// This method returns items user added in the main list.
        /// </summary>
        /// <returns></returns>
        public List<ItemDescInfo> GetAddedItem()
        {         
            return listItemUserSelected;
        }


        /// <summary>
        /// This method calculates total cost of items user added in the grid list.
        /// </summary>
        public decimal CalculateTotal()
        {
            return totalCostCal;
        }


        /// <summary>
        /// //Get max number(lastest Invoice number) of Invoice Number from Invoices table
        /// </summary>
        /// <returns></returns>
        public int GetMaxInvoiceNum()
        {
            int iMaxInvoiceNum = 0;
            dataAccess = new clsDataAccess();
            DataSet ds = new DataSet();
            int iRet = 0;

            //Get max number(lastest Invoice number) of Invoice Number from Invoices table
            ds = dataAccess.ExecuteSQLStatement(clsMainSQL.SQLGetMaxInvoiceNumber(), ref iRet);
            iMaxInvoiceNum = (int)ds.Tables[0].Rows[0][0];

            return iMaxInvoiceNum;
        }


        /// <summary>
        /// This method saves Invoice items user selected.
        /// </summary>
        public void SaveInvoice(DateTime date)
        {
            int iMaxInvoiceNum = 0;
            int iLineNumber = 1;
            
            dataAccess = new clsDataAccess();         

            //Insert Invoice number and its date into the Invoices table.
            dataAccess.ExecuteNonQuery(clsMainSQL.SQLCreateNewInvoice(date));

            //Get max number of Invoice number.
            iMaxInvoiceNum = GetMaxInvoiceNum();
            

            for (int i = 0; i < listItemUserSelected.Count; i++)
            {
                dataAccess.ExecuteNonQuery(clsMainSQL.SQLInsertLineItmes(iMaxInvoiceNum, iLineNumber, listItemUserSelected[i].ItemCode));
                iLineNumber++;
            }

            CurrentInvoice.InvoiceNumber = iMaxInvoiceNum;
            CurrentInvoice.InvoiceDate = date;
            CurrentInvoice.TotalCost = CalculateTotal();
        }




        public void DeleteItemInList(ItemDescInfo deleteItems)
        {
            listItemUserSelected.Remove(deleteItems);
            totalCostCal -= deleteItems.Cost;
        }





        /// <summary>
        /// This method edits Invoice items user input.
        /// </summary>
        public void EditInvoice(int editInvoiceNum, DateTime editDate)
        {
            int iLineNumber = 1;
            dataAccess = new clsDataAccess();
            DataSet ds = new DataSet();
           

            //Delete LineItems then rewrite.
            dataAccess.ExecuteNonQuery(clsMainSQL.SQLDeleteLineItems(editInvoiceNum));
           
            //Update Invoice Date in Invoces table.
            dataAccess.ExecuteNonQuery(clsMainSQL.SQLEditInvoice(editInvoiceNum, editDate));

            //Rewirte LineItmems.
            for (int i = 0; i < listItemUserSelected.Count; i++)
            {
                dataAccess.ExecuteNonQuery(clsMainSQL.SQLInsertLineItmes(editInvoiceNum, iLineNumber, listItemUserSelected[i].ItemCode));
                iLineNumber++;
            }

            CurrentInvoice.InvoiceNumber = editInvoiceNum;
            CurrentInvoice.InvoiceDate = editDate;
            CurrentInvoice.TotalCost = CalculateTotal();
        }


        /// <summary>
        /// This method deletes Invoice items user input from database.
        /// </summary>
        public void DeleteInvoice(int deleteInvoiceNum)
        {
            dataAccess = new clsDataAccess();
            DataSet ds = new DataSet();

            //Delete LineItems then rewrite.
            dataAccess.ExecuteNonQuery(clsMainSQL.SQLDeleteLineItems(deleteInvoiceNum));

            //Update Invoice Date in Invoces table.
            dataAccess.ExecuteNonQuery(clsMainSQL.SQLDeleteInvoice(deleteInvoiceNum));

            Reset();
        }



        /// <summary>
        /// This method resets all varialbes to initiliazation status.
        /// </summary>
        public void Reset()
        {
            listItemUserSelected = null;
            CurrentInvoice = null;
            totalCostCal = 0;
            
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

            public static implicit operator Invoices(clsMainLogic v)
            {
                throw new NotImplementedException();
            }
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
