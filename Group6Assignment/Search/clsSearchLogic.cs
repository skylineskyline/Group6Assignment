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
            clsSearchSQLClass = new clsSearchSQL();
        }

        /// <summary>
        /// This method gets or sets the currently selected invoice number.
        /// </summary>
        /// <returns></returns>
        public int InvoiceNumber
        {
            get
            {
                try
                {
                    return iInvoiceNumber;
                }
                catch (Exception ex)
                {
                    throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                }
            }

            set
            {
                try
                {
                    iInvoiceNumber = value;
                }
                catch (Exception ex)
                {
                    throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                }
            }
        }

        /// <summary>
        /// This method takes the DataSet from clsSearchSQL.PopulateNumberCB and fills the list.
        /// </summary>
        public List<string> PopulateNumberCB()
        {
            List<string> comboList = new List<string>();
            DataSet ds = new DataSet();
            try
            {
                ds = clsSearchSQLClass.PopulateNumberCB();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    comboList.Add(ds.Tables[0].Rows[i][0].ToString());
                }

                return comboList;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method takes the DataSet from clsSearchSQL.PopulateDateCB and fills the list.
        /// </summary>
        public List<string> PopulateDateCB()
        {
            List<string> comboList = new List<string>();
            DataSet ds = new DataSet();
            try
            {
                ds = clsSearchSQLClass.PopulateDateCB();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    comboList.Add(ds.Tables[0].Rows[i][0].ToString());
                }

                return comboList;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method takes the DataSet from clsSearchSQL.PopulateTotalChargeCB and fills the list.
        /// </summary>
        public List<string> PopulateTotalChargeCB()
        {
            List<string> comboList = new List<string>();
            DataSet ds = new DataSet();
            try
            {
                ds = clsSearchSQLClass.PopulateTotalChargeCB();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    comboList.Add(ds.Tables[0].Rows[i][0].ToString());
                }

                return comboList;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method will populate the datagrid.
        /// </summary>
        /// <returns></returns>
        public DataSet PopulateDataGrid()
        {
            try
            {
                CurrentGridData = clsSearchSQLClass.PopulateDataGrid();

                return CurrentGridData;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
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
            try
            {
                List<string> list = new List<string>();
                int ii = -1;
                double ic = -1;
                if (invoice != null)
                    Int32.TryParse(invoice, out ii);

                if (charge != null)
                {
                    string sStrippedCharge = charge.Trim('$');
                    Double.TryParse(sStrippedCharge, out ic);
                }

                CurrentGridData = clsSearchSQLClass.UpdateDataGrid(ii, date, ic);
                return CurrentGridData;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

    }// end class
}// end namespace
