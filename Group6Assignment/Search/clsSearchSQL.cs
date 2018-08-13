/***************************************************************************************************
* Group5Assignment
* clsSearchSQL.cs
* Dongmin Kim, Kyle Kippen, Goeun Kwak
* CS3280 Group assignment - Jewelry Invoice.
*
***************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Group6Assignment.Search
{
    class clsSearchSQL
    {
        /// <summary>
        /// Connection string to the database.
        /// </summary>
        private string sConnectionString;

        /// <summary>
        /// Constructor that sets the connection string to the database
        /// </summary>
		public clsSearchSQL()
        {
            sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\Invoice.mdb";
        }


        /// <summary>
        /// This method returns all invoices from the DataBase.
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet PopulateDataGrid()
        {
            DataSet ds = new DataSet();

            string sSQL = "SELECT i.InvoiceNum AS \"Invoice Number\", Format ([InvoiceDate], 'mm/dd/yyyy ') AS \"Invoice date\", SUM(id.cost) AS TotalCharge " +
                          "FROM ItemDesc id, LineItems li, Invoices i " +
                          "WHERE i.InvoiceNum = li.InvoiceNum " +
                          "AND li.ItemCode = id.ItemCode " +
                          "GROUP BY i.InvoiceNum, i.InvoiceDate; ";

            ds = ExecuteSQLStatement(sSQL);

            return ds;
        }

        /// <summary>
        /// This method takes the selected combo box items and returns a filtered dataset for the datagrid.
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="date"></param>
        /// <param name="charge"></param>
        /// <returns></returns>
        public DataSet UpdateDataGrid(int invoice, string date, double charge)
        {
            DataSet ds = new DataSet();

            string sSQL = "SELECT i.InvoiceNum, Format ([InvoiceDate], 'mm/dd/yyyy ') AS \"Invoice date\", SUM(id.cost) AS TotalCharge " +
                          "FROM ItemDesc id, LineItems li, Invoices i" +
                          "WHERE i.InvoiceNum = li.InvoiceNum" +
                          "AND li.ItemCode = id.ItemCode" +
                          "GROUP BY i.InvoiceNum, i.InvoiceDate;";


            if (invoice != -1 && date != null && charge != -1)// if everything was chosen
            {
                sSQL = "SELECT i.InvoiceNum, Format ([InvoiceDate], 'mm/dd/yyyy ') AS \"Invoice date\", SUM(id.cost) AS TotalCharge " +
                       "FROM ItemDesc id, LineItems li, Invoices i" +
                       "WHERE i.InvoiceNum = li.InvoiceNum" +
                       "AND li.ItemCode = id.ItemCode" +
                       "AND i.InvoiceNum = " + invoice +
                       "AND InvoiceDate = #" + date +
                       "# GROUP BY i.InvoiceNum, i.InvoiceDate;" +
                       "HAVING SUM(id.cost) = " + charge + "; ";
            }
            else if (invoice != -1 && date != null && charge == -1)// if invoice and date were chosen
            {
                sSQL = "SELECT i.InvoiceNum, Format([InvoiceDate],'mm/dd/yyyy ') AS \"Invoice date\", Sum(id.cost) AS TotalCharge " +
                       "FROM ItemDesc AS id, LineItems AS li, Invoices AS i " +
                       "WHERE(((i.InvoiceNum) =[li].[InvoiceNum]) AND((li.ItemCode) =[id].[ItemCode])) " +
                       "AND i.InvoiceNum = " + invoice +
                       "AND InvoiceDate = #" + date +
                       "# GROUP BY i.InvoiceNum, i.InvoiceDate;";
            }
            else if (invoice != -1 && date == null && charge != -1)// if invoice and charge were chosen
            {
                sSQL = "SELECT i.InvoiceNum, Format([InvoiceDate],'mm/dd/yyyy ') AS \"Invoice date\", Sum(id.cost) AS TotalCharge " +
                       "FROM ItemDesc AS id, LineItems AS li, Invoices AS i " +
                       "WHERE(((i.InvoiceNum) =[li].[InvoiceNum]) AND((li.ItemCode) =[id].[ItemCode])) " +
                       "AND i.InvoiceNum = " + invoice +
                       " GROUP BY i.InvoiceNum, i.InvoiceDate " +
                       "HAVING SUM(id.cost) = " + charge + ";"; 
            }
            else if (invoice == -1 && date != null && charge != -1)// if date and charge were chosen
            {
                sSQL = "SELECT i.InvoiceNum, Format ([InvoiceDate], 'mm/dd/yyyy ') AS \"Invoice Date\", SUM(id.cost) AS TotalCharge " +
                       "FROM ItemDesc id, LineItems li, Invoices i WHERE i.InvoiceNum = li.InvoiceNum " +
                       "AND li.ItemCode = id.ItemCode " +
                       "AND InvoiceDate = #" + date +
                       "# GROUP BY i.InvoiceNum, i.InvoiceDate HAVING SUM(id.cost) = " + charge + ";";

            }
            else if (invoice != -1 && date == null && charge == -1)// if invoice was chosen
            {
                sSQL = "SELECT i.InvoiceNum, Format([InvoiceDate],'mm/dd/yyyy ') AS \"Invoice date\", Sum(id.cost) AS TotalCharge " +
                       "FROM ItemDesc AS id, LineItems AS li, Invoices AS i " +
                       "WHERE(((i.InvoiceNum) =[li].[InvoiceNum]) AND((li.ItemCode) =[id].[ItemCode])) " +
                       "AND i.InvoiceNum = " + invoice +
                       " GROUP BY i.InvoiceNum, i.InvoiceDate;";
            }
            else if (invoice == -1 && date != null && charge == -1)// if Date was chosen
            {
                sSQL = "SELECT i.InvoiceNum, Format ([InvoiceDate], 'mm/dd/yyyy ') AS \"Invoice Date\", SUM(id.cost) AS TotalCharge " +
                       "FROM ItemDesc id, LineItems li, Invoices i WHERE i.InvoiceNum = li.InvoiceNum " +
                       "AND li.ItemCode = id.ItemCode " +
                       "AND InvoiceDate = #" + date +
                       "# GROUP BY i.InvoiceNum, i.InvoiceDate;";
            }
            else if (invoice == -1 && date == null && charge != -1)// if Charge was chosen
            {
                sSQL = "SELECT i.InvoiceNum, Format([InvoiceDate], 'mm/dd/yyyy '), SUM(id.cost) AS TotalCharge FROM ItemDesc id, LineItems li, Invoices i " +
                       "WHERE i.InvoiceNum = li.InvoiceNum AND li.ItemCode = id.ItemCode " +
                       "GROUP BY i.InvoiceNum, i.InvoiceDate HAVING SUM(id.cost) = "  + charge + ";";
            }

            ds = ExecuteSQLStatement(sSQL);

            return ds;
        }

        /// <summary>
        /// This SQL pulls all Invoice Numbers from the database
        /// </summary>
        /// <returns></returns>
        public DataSet PopulateNumberCB()
        {
            DataSet ds = new DataSet();

            string sql = "SELECT InvoiceNum FROM Invoices";
            ds = ExecuteSQLStatement(sql);

            return ds;
        }

        /// <summary>
        /// This SQL pulls all invoice dates from the database
        /// </summary>
        /// <returns></returns>
        public DataSet PopulateDateCB()
        {
            DataSet ds = new DataSet();
            // v  I added a space in the format to fit in the combobox
            string sql = "SELECT DISTINCT Format ([InvoiceDate], 'mm/dd/yyyy ') AS Dates FROM Invoices";
            ds = ExecuteSQLStatement(sql);

            return ds;
        }

        /// <summary>
        /// This SQL pulls all total charges from the database
        /// </summary>
        /// <returns></returns>
        public DataSet PopulateTotalChargeCB()
        {
            DataSet ds = new DataSet();
            string sql = "SELECT Distinct SUM(id.cost) " +
                          "FROM ItemDesc id, LineItems li, Invoices i " +
                          "WHERE i.InvoiceNum = li.InvoiceNum " +
                          "AND li.ItemCode = id.ItemCode " +
                          "GROUP BY i.InvoiceNum, i.InvoiceDate;";
            ds = ExecuteSQLStatement(sql);

            return ds;
            
        }

        /// <summary>
        /// This method takes an SQL statment that is passed in and executes it.  The resulting values
        /// are returned in a DataSet.  The number of rows returned from the query will be put into
        /// the reference parameter iRetVal.
        /// </summary>
        /// <param name="sSQL">The SQL statement to be executed.</param>
        /// <returns>Returns a DataSet that contains the data from the SQL statement.</returns>
        public DataSet ExecuteSQLStatement(string sSQL) 
        {
        try
        {
            //Create a new DataSet
            DataSet ds = new DataSet();

            using (OleDbConnection conn = new OleDbConnection(sConnectionString))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                {

                    //Open the connection to the database
                    conn.Open();

                    //Add the information for the SelectCommand using the SQL statement and the connection object
                    adapter.SelectCommand = new OleDbCommand(sSQL, conn);
                    adapter.SelectCommand.CommandTimeout = 0;

                    //Fill up the DataSet with data
                    adapter.Fill(ds);
                }
            }

            //return the DataSet
            return ds;
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
        }

        /// <summary>
        /// This method takes an SQL statment that is passed in and executes it.  The resulting single 
        /// value is returned.
        /// </summary>
        /// <param name="sSQL">The SQL statement to be executed.</param>
        /// <returns>Returns a string from the scalar SQL statement.</returns>
		public string ExecuteScalarSQL(string sSQL)
        {
            try
            {
                //Holds the return value
                object obj;

                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {

                        //Open the connection to the database
                        conn.Open();

                        //Add the information for the SelectCommand using the SQL statement and the connection object
                        adapter.SelectCommand = new OleDbCommand(sSQL, conn);
                        adapter.SelectCommand.CommandTimeout = 0;

                        //Execute the scalar SQL statement
                        obj = adapter.SelectCommand.ExecuteScalar();
                    }
                }

                //See if the object is null
                if (obj == null)
                {
                    //Return a blank
                    return "";
                }
                else
                {
                    //Return the value
                    return obj.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method takes an SQL statment that is a non query and executes it.
        /// </summary>
        /// <param name="sSQL">The SQL statement to be executed.</param>
        /// <returns>Returns the number of rows affected by the SQL statement.</returns>
        public int ExecuteNonQuery(string sSQL)
        {
            try
            {
                //Number of rows affected
                int iNumRows;

                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    //Open the connection to the database
                    conn.Open();

                    //Add the information for the SelectCommand using the SQL statement and the connection object
                    OleDbCommand cmd = new OleDbCommand(sSQL, conn);
                    cmd.CommandTimeout = 0;

                    //Execute the non query SQL statement
                    iNumRows = cmd.ExecuteNonQuery();
                }

                //return the number of rows affected
                return iNumRows;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}