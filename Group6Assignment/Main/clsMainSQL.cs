/***************************************************************************************************
* Group5Assignment
* clsMainSQL.cs
* Dongmin Kim, Kyle Kippen, Goeun Kwak
* CS3280 Group assignment - Jewelry Invoice.
*
***************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group6Assignment.Main
{
    public class clsMainSQL
    {
        /// <summary>
        /// This method inserts new invoice to an Invoice table.
        /// </summary>
        /// <returns></returns>
        public string SQLCreateNewInvoice(DateTime date)
        {
            return "INSERT INTO Invoices (InvoiceDate) VALUES (#" + date + "#)";
        }


        /// <summary>
        /// This method gets max number of Invoice number.
        /// </summary>
        /// <returns></returns>
        public string SQLGetMaxInvoiceNumber()
        {
            return "SELECT MAX(InvoiceNum) FROM Invoices";
        }



        public string SQLInsertLineItmes(int invoiceNum, int lineItemNum, string itemCode)
        {
            return "INSERT INTO Lineitems (InvoiceNum, LineItemNum, ItemCode) VALUES (" + invoiceNum + "," + lineItemNum + ",'" + itemCode + "')";
        }



        /// <summary>
        /// This method is search for Items from ItemDesc table
        /// </summary>
        /// <returns></returns>
        public string SQLGetAllItemDesc()
        {
            return "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc ORDER BY ItemCode ASC";
            
        }

        /// <summary>
        /// Select data by InvoiceNumber in LineItems table.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public string SQLGetCurrentInvoiceLineItems(int invoiceNum)
        {
            string sSql = "SELECT ID.ItemCode, ItemDesc, Cost " +
                        "FROM ItemDesc AS ID INNER JOIN LineItems AS LI ON ID.ItemCode = LI.ItemCode " +
                        "WHERE LI.InvoiceNum = " + invoiceNum;

            return sSql;
        }
        

        /// <summary>
        /// Select data by ItemCode in ItemDesc table.
        /// </summary>
        /// <returns></returns>
        public string SQLGetItemDesc(string itemCode)
        {
            return "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc WHERE ItemCode ='" + itemCode + "'";
        }


        /// <summary>
        /// This method adds line items to an Invoice table
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="itemDesc"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        public string SQLAddInvoiceItemDesc(string itemCode, string itemDesc, decimal cost)
        {
            return "INSERT INTO ItemDesc(ItemCode, ItemDesc, Cost) VALUE ('" + itemCode + "','" + itemDesc + "'," + cost + ")";
        }


        /// <summary>
        /// This method updates invoice item to an Invoice table.
        /// </summary>
        /// <param name="invoiceDate"></param>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public string SQLEditInvoice( int invoiceNum, DateTime invoiceDate)
        {
            //return "UPDATE ItemDesc SET = ItemCode ='" + itemCode + "', ItemDesc ='" + itemDesc + "', Cost = " + cost + "WHERE Item;
            return "UPDATE Invoices SET InvoiceDate =#" + invoiceDate + "# WHERE InvoiceNum = " + invoiceNum;
        }


        /// <summary>
        /// This method deletes all line items by specific Invoice number.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public string SQLDeleteLineItems(int invoiceNum)
        {
            return "DELETE FROM LineItems WHERE InvoiceNum = " + invoiceNum;
        }


        /// <summary>
        /// This method deletes all invoice items by specific Invoice number.
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public string SQLDeleteInvoice(int invoiceNum)
        {
            return "DELETE FROM Invoices WHERE InvoiceNum = " + invoiceNum;
        }

    }
}
