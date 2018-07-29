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
        /// This method is search for Items from ItemDesc table
        /// </summary>
        /// <returns></returns>
        public string SQLGetAllItemDesc()
        {
            return "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc";
        }


        /// <summary>
        /// This method adds line items to an Invoice table
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="itemDesc"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        public string SQLAddInvoiceItemDesc(string itemCode, string itemDesc, decimal cost )
        {
            return "INSERT INTO ItemDesc(ItemCode, ItemDesc, Cost) VALUE ('" + itemCode + "','" + itemDesc + "'," + cost + ")";
        }


        /// <summary>
        /// This method updates invoice item to an Invoice table.
        /// </summary>
        /// <param name="invoiceDate"></param>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public string SQLEditInvoice(DateTime invoiceDate, int invoiceNum)
        {
            //return "UPDATE ItemDesc SET = ItemCode ='" + itemCode + "', ItemDesc ='" + itemDesc + "', Cost = " + cost + "WHERE Item;
            return "UPDATE Invoice SET InvoiceDate =#" + invoiceDate + "# WHERE InvoiceNum = " + invoiceNum;
        }


        /// <summary>
        /// This method deletes all line items by specific Invoice number.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public string SQLDeleteLineItems(int invoiceNum)
        {
            return "DELETE FROM LineItems WHERE InvoiceNum =" + invoiceNum; 
        }


        /// <summary>
        /// This method deletes all invoice items by specific Invoice number.
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public string SQLDeleteInvoice(int invoiceNumber)
        {
            return "DELETE FROM Invoices WHERE InvoiceNumber = " + invoiceNumber;
        }

    }
}
