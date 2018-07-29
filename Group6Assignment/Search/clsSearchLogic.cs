/***************************************************************************************************
* Group5Assignment
* clsSearchLogic.cs
* Dongmin Kim, Kyle Kippen, Goeun Kwak
* CS3280 Group assignment - Jewelry Invoice.
*
***************************************************************************************************/


using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Group6Assignment.Search
{
    class clsSearchLogic
    {
        /// <summary>
        /// This variable holds the currently selected invoice number
        /// </summary>
        private int iInvoiceNumber;

        /// <summary>
        /// This class handles all of the SQL statements
        /// </summary>
        private clsSearchSQL clsSearchSQLClass;

        /// <summary>
        /// This data set holds the current filtered data from the database.
        /// </summary>
        public DataSet CurrentGridData;

        public clsSearchLogic()
        {

        }

        /// <summary>
        /// This method gets or sets the currently selected invoice number.
        /// </summary>
        /// <returns></returns>
        public int InvoiceNumber
        {
            get
            {
                return iInvoiceNumber;
            }
            set
            {
                iInvoiceNumber = value;

            }
        }

        /// <summary>
        /// This method takes the DataSet from clsSearchSQL.PopulateNumberCB and fills the list.
        /// </summary>
        public List<string> PopulateNumberCB()
        {
            List<string> comboList = new List<string>();
            return comboList;
        }

        /// <summary>
        /// This method takes the DataSet from clsSearchSQL.PopulateDateCB and fills the list.
        /// </summary>
        public List<string> PopulateDateCB()
        {
            List<string> comboList = new List<string>();
            return comboList;
        }

        /// <summary>
        /// This method takes the DataSet from clsSearchSQL.PopulateTotalChargeCB and fills the list.
        /// </summary>
        public List<string> PopulateTotalChargeCB()
        {
            List<string> comboList = new List<string>();
            return comboList;
        }

        /// <summary>
        /// This method will populate the datagrid.
        /// </summary>
        /// <returns></returns>
        public DataSet PopulateDataGrid()
        {
            return CurrentGridData;
        }

        /// <summary>
        /// This method updates the Data Grid with the refined search criteria.
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="date"></param>
        /// <param name="charge"></param>
        /// <returns></returns>
        public DataSet UpdateDataGrid(string invoice, string date, string charge)
        {
                return CurrentGridData;
        }

    }// end class
}// end namespace
