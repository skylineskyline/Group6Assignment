/***************************************************************************************************
* Group5Assignment
* wndSearch.cs
* Dongmin Kim, Kyle Kippen, Goeun Kwak
* CS3280 Group assignment - Jewelry Invoice.
*
***************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;

namespace Group6Assignment.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {

        /// <summary>
        /// This class handles logic done behind the UI
        /// </summary>
        clsSearchLogic clsSearchLogicClass;

        /// <summary>
        /// This string holds the selected invoice
        /// </summary>
        private string sInvoice = null;

        /// <summary>
        /// This string holds the selected date
        /// </summary>
        private string sDate = null;

        /// <summary>
        /// This string holds the selected total charge
        /// </summary>
        private string sCharge = null;


        public wndSearch()
        {

        }

        /// <summary>
        /// This method updates all the combo boxes.
        /// </summary>
        private void UpdateForm()
        {
         
        }

        /// <summary>
        /// This button will set the invoice number and close the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
         
        }

        /// <summary>
        /// This method relays the getInvoiceNumber from the logic class.
        /// </summary>
        /// <returns></returns>
        public int getInvoiceNumber()
        {
           
            return 0;
        }

        /// <summary>
        /// This method takes the list in clsSearchLogic.populateNumberCB needed to fill the combo box.
        /// </summary>
        private void PopulateNumberCB()
        {
           
        }

        /// <summary>
        /// This method takes the list in clsSearchLogic.PopulateDateCB needed to fill the combo box.
        /// </summary>
        private void PopulateDateCB()
        {
          
        }

        /// <summary>
        /// This method takes the list in clsSearchLogic.PopulateTotalChargeCB needed to fill the combo box.
        /// </summary>
        private void PopulateTotalChargeCB()
        {
         
        }

        /// <summary>
        /// This method updates the datagrid depending on the selected combo box items.
        /// </summary>
        private void UpdateDataGrid()
        {
          
        }

        /// <summary>
        /// This method takes the list in clsSearchLogic.PopulateDataGrid needed to fill the DataGrid
        /// </summary>
        private void PopulateDataGrid()
        {
         
        }

        /// <summary>
        /// This method updates the combo boxes with the available options from the narrowed down dataset.
        /// </summary>
        private void UpdateComboBoxes()
        {
          
        }

        /// <summary>
        /// This method sets the selected invoice number and narrows down the dataset and datagrid with the selected Invoice number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbInvoiceNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        /// <summary>
        /// This method sets the selected Date and narrows down the dataset and datagrid with the selected Date.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbInvoiceDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
        }

        /// <summary>
        /// This method sets the selected Total Charge and narrows down the dataset and datagrid with the selected Total Charge.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbTotalCharge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        /// <summary>
        /// This button resets the form to its initial state.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
          
        }

        /// <summary>
        /// This button sets Invoice number to -1 and hides the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
           
        }

        /// <summary>
        /// This method hides the window instead of closing it down when the X is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
          
        }

        /// <summary>
        /// This method handles the top level exceptions
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
          
        }

    }// end wndSearch
}
