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
        /// This method is for create new Invoice statement.
        /// </summary>
        /// <returns></returns>
        public string CreateNewInvoice(DateTime date)
        {
            return "INSERT INTO ItemDesc (InvoiceDate) VALUES (#" + date + "#)";
        }


        /// <summary>
        /// This method is search for Items from ItemDesc
        /// </summary>
        /// <returns></returns>
        public string SearchInventory()
        {
            return "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc";
        }
    }
}
