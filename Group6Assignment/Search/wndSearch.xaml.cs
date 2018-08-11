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
using Group6Assignment.Main;

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

        /// <summary>
        /// The method will initialize component, AMD update the forms
        /// </summary>
        public wndSearch()
        {
            InitializeComponent();
            clsSearchLogicClass = new clsSearchLogic();
            UpdateForm();
        }

        /// <summary>
        /// This method updates all the combo boxes.
        /// </summary>
        private void UpdateForm()
        {
            try
            {
                PopulateNumberCB();
                PopulateDateCB();
                PopulateTotalChargeCB();
                PopulateDataGrid();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This button will set the invoice number and close the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string sSelection = dgInvoices.SelectedIndex.ToString();
                int iSelection;
                Int32.TryParse(sSelection, out iSelection);

                sSelection = clsSearchLogicClass.CurrentGridData.Tables[0].Rows[iSelection][0].ToString();
                Int32.TryParse(sSelection, out iSelection);

                clsSearchLogicClass.InvoiceNumber = iSelection;

                this.Hide();
            }
            catch (Exception ex)
            {
                // Top level method to handle the error.
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This method relays the getInvoiceNumber from the logic class.
        /// </summary>
        /// <returns></returns>
        public int getInvoiceNumber()
        {
            try
            {
                return clsSearchLogicClass.InvoiceNumber;
            }
            catch (Exception ex)
            {
                // Top level method to handle the error.
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

            return 0;
        }

        /// <summary>
        /// This method takes the list in clsSearchLogic.populateNumberCB needed to fill the combo box.
        /// </summary>
        private void PopulateNumberCB()
        {
            try
            {
                cbInvoiceNumber.Items.Clear();
                List<string> comboBoxList = clsSearchLogicClass.PopulateNumberCB();
                for (int i = 0; i < comboBoxList.Count; i++)
                {
                    cbInvoiceNumber.Items.Add(comboBoxList[i]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method takes the list in clsSearchLogic.PopulateDateCB needed to fill the combo box.
        /// </summary>
        private void PopulateDateCB()
        {
            try
            {
                cbInvoiceDate.Items.Clear();
                List<string> comboBoxList = clsSearchLogicClass.PopulateDateCB();
                for (int i = 0; i < comboBoxList.Count; i++)
                {
                    cbInvoiceDate.Items.Add(comboBoxList[i]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method takes the list in clsSearchLogic.PopulateTotalChargeCB needed to fill the combo box.
        /// </summary>
        private void PopulateTotalChargeCB()
        {
            try
            {
                cbTotalCharge.Items.Clear();
                List<string> comboBoxList = clsSearchLogicClass.PopulateTotalChargeCB();
                for (int i = 0; i < comboBoxList.Count; i++)
                {
                    cbTotalCharge.Items.Add(comboBoxList[i]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method updates the datagrid depending on the selected combo box items.
        /// </summary>
        private void UpdateDataGrid()
        {
            try
            {
                dgInvoices.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = clsSearchLogicClass.UpdateDataGrid(sInvoice, sDate, sCharge).Tables[0] });
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method takes the list in clsSearchLogic.PopulateDataGrid needed to fill the DataGrid
        /// </summary>
        private void PopulateDataGrid()
        {
            try
            {

                dgInvoices.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = clsSearchLogicClass.PopulateDataGrid().Tables[0] });
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method updates the combo boxes with the available options from the narrowed down dataset.
        /// </summary>
        private void UpdateComboBoxes()
        {
            cbInvoiceNumber.Items.Clear();
            cbInvoiceDate.Items.Clear();
            cbTotalCharge.Items.Clear();

            for (int i = 0; i < clsSearchLogicClass.CurrentGridData.Tables[0].Rows.Count; i++)
            {
                cbInvoiceNumber.Items.Add(clsSearchLogicClass.CurrentGridData.Tables[0].Rows[i][0]);
                cbInvoiceDate.Items.Add(clsSearchLogicClass.CurrentGridData.Tables[0].Rows[i][1]);
                cbTotalCharge.Items.Add(clsSearchLogicClass.CurrentGridData.Tables[0].Rows[i][2]);
            }
        }

        /// <summary>
        /// This method sets the selected invoice number and narrows down the dataset and datagrid with the selected Invoice number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbInvoiceNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbInvoiceNumber.SelectedIndex != -1)
                {
                    sInvoice = cbInvoiceNumber.SelectedItem.ToString();
                    UpdateDataGrid();
                    UpdateComboBoxes();
                }
            }
            catch (Exception ex)
            {
                // Top level method to handle the error.
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This method sets the selected Date and narrows down the dataset and datagrid with the selected Date.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbInvoiceDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbInvoiceDate.SelectedIndex != -1)
                {
                    sDate = cbInvoiceDate.SelectedItem.ToString();
                    UpdateDataGrid();
                    UpdateComboBoxes();
                }
            }
            catch (Exception ex)
            {
                // Top level method to handle the error.
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This method sets the selected Total Charge and narrows down the dataset and datagrid with the selected Total Charge.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbTotalCharge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbTotalCharge.SelectedIndex != -1)
                {
                    sCharge = cbTotalCharge.SelectedItem.ToString();
                    UpdateDataGrid();
                    UpdateComboBoxes();
                }
            }
            catch (Exception ex)
            {
                // Top level method to handle the error.
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This button resets the form to its initial state.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sInvoice = null;
                sDate = null;
                sCharge = null;
                cbInvoiceDate.SelectedIndex = -1;
                cbInvoiceDate.SelectedIndex = -1;
                cbTotalCharge.SelectedIndex = -1;
                UpdateForm();
            }
            catch (Exception ex)
            {
                // Top level method to handle the error.
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This button sets Invoice number to -1 and hides the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Set the variable iInvoiceNumber inside clsSearchLogic to -1
                clsSearchLogicClass.InvoiceNumber = -1; //returns a -1 to show that no selection was made.
                this.Hide();
            }
            catch (Exception ex)
            {
                // Top level method to handle the error.
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// This method closes window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                this.Hide();
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                // Top level method to handle the error.
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// This method handles the top level exceptions
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);

            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("Error.txt", Environment.NewLine +
                                             "HandleError Exception: " + ex.Message);
            }
        }
    }// end wndSearch
}
