/***************************************************************************************************
* Group5Assignment
* wndMain.xaml.cs
* Dongmin Kim, Kyle Kippen, Goeun Kwak
* CS3280 Group assignment - Jewelry Invoice.
*
***************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
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
using Group6Assignment.Main;
using System.Reflection;
using System.ComponentModel;
using Group6Assignment.Items;
using Group6Assignment.Search;


namespace Group6Assignment.Main
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        /// <summary>
        /// Variable to manage creating, delete, editing, and calculate Invoice.
        /// </summary>
        private clsMainLogic mainLogic;


        /// <summary>
        /// Variable to open Items window.
        /// </summary>
        private Items.wndItems openItems;

        /// <summary>
        /// Variable to open search window.
        /// </summary>
        private Search.wndSearch openSearch;

        
        /// <summary>
        /// Variable to check add or edit mode on screen.
        /// </summary>
        private bool IsAddOrEditStatus = false;


        /// <summary>
        /// Variable to hold just saved the invoice by user.
        /// </summary>
        private clsMainLogic.Invoices currentInvoice;


        /// <summary>
        /// Constructor
        /// </summary>
        public wndMain()
        {
            InitializeComponent();
            this.Closing += ClosingWindowForm;

            
            openItems = new Items.wndItems();
            openSearch = new Search.wndSearch();

            StartWindow();
        }


        /// <summary>
        /// Event handler method for open update(edit) invoice window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MeditInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var windowItem = new wndItems();
                windowItem.Show();
                this.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Event handler method for open search invoice window.
        /// </summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void MsearchInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var windowSearch = new wndSearch();
                openSearch.Show();
                this.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Event handler method for create new invoice.
        /// After it is fired, user can enter(add) invoice items.
        /// Inactivate 'Create invoices' button after this button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BcreateInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CreateWindowStatus();
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Event handler method for edit invoice user created.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BeditInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EditWindowStatus();
                LoadInvocie();
            }
            catch (Exception)
            {

                throw;
            }
}



        /// <summary>
        /// Event handler method for delete invoice user created.
        /// When user tries to delete invoice the one created, show 'Yes or OK', and 'Cancel' message box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BdeleteInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
             
                if (MessageBox.Show("Are you sure you want to delete this invoice?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    //Do no stuff
                    return;
                }

                else
                {
                    //Do yes stuff
                    mainLogic.DeleteInvoice(currentInvoice.InvoiceNumber);
                                       
                    currentInvoice = null;
                    LoadInvocie();
                    StartWindow();
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Event handler method for add item user clicked.
        /// When user selects an item(from LineItem table), then clicks 'Add Item' button, selected item is stored in list variable.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                //clsMainLogic.ItemDesc itemDesc = (clsMainLogic.ItemDesc)CinvoiceList.SelectedItem;

                if(CinvoiceList.SelectedItem is clsMainLogic.ItemDescInfo selectedItem)
                {
                    mainLogic.AddItemToInvoice(selectedItem);
                    UpdateMainItemsInvoceList(); 
                }                
            }
            catch (Exception)
            {

                throw;
            }
        }



        /// <summary>
        /// Event handler method for save item(s) user selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BsaveInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {                
                //If current save mode is add mode
                if (!IsAddOrEditStatus)
                {
                    //Save the new invoice.
                    mainLogic.SaveInvoice(DpInvoiceInsertDate.SelectedDate.Value.Date);
                }

                //If current save mode is edit mode
                else
                {
                    mainLogic.EditInvoice(currentInvoice.InvoiceNumber, DpInvoiceInsertDate.SelectedDate.Value.Date);
                }
                
                //Copy current invoice data just added for edit and delete invoice.
                currentInvoice = mainLogic.CurrentInvoice;
                AfterSaveWindowStatus();

            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Event handler method that is getting Invoice information user selected from combo box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CinvoiceList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //BaddItem.IsEnabled = false;
                BaddItem.IsEnabled = CinvoiceList.SelectedItem is clsMainLogic.ItemDescInfo;
                
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// This method updates list of LineItem on the main screen
        /// </summary>
        public void UpdateMainItemsInvoceList()
        {
            try
            {                
                dgDisplaytems.ItemsSource = null;
                //Update DataGrid list.
                dgDisplaytems.ItemsSource = mainLogic.GetAddedItem();

                //Update total cost.
                TbTotalCost.Text = String.Format("${0:0.00}", mainLogic.CalculateTotal());

            }
            catch (Exception ex)
            {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodBase.GetCurrentMethod().Name, ex.Message);
            }
                      
        }



        /// <summary>
        /// This event handler closes window form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClosingWindowForm(object sender, CancelEventArgs e)
        {
            try
            {               
                    //e.Cancel = true;              
            }
            catch (Exception ex)
            {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

       




        /// <summary>
        /// This method restricts item drop-down list, add button, invoice number, and date
        /// until user clicks 'Create Invoice' button.
        /// </summary>
        private void StartWindow()
        {
            try
            {
                TbInvoiceNumber.Visibility = Visibility.Hidden;
                DpInvoiceInsertDate.Visibility = Visibility.Hidden;
                CinvoiceList.Visibility = Visibility.Hidden;
                BaddItem.Visibility = Visibility.Hidden;
                CinvoiceList.IsEnabled = false;
                BaddItem.IsEnabled = false;
                cClearInvoice.IsEnabled = false;
                BeditInvoice.IsEnabled = false;
                BdeleteInvoice.IsEnabled = false;
            }
            catch (Exception ex)
            {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodBase.GetCurrentMethod().Name, ex.Message);
            }

        }



        /// <summary>
        /// This method allows user can click Invoice data, item drop-down list.
        /// </summary>
        private void CreateWindowStatus()
        {
            try
            {
                //Create the new Invoice.
                mainLogic = new clsMainLogic(null);
                currentInvoice = new clsMainLogic.Invoices();
                

                TbInvoiceNumber.Visibility = Visibility.Visible;
                TbInvoiceNumber.IsEnabled = true;
                TbInvoiceNumber.Text = "TBD";

                DpInvoiceInsertDate.Visibility = Visibility.Visible;
                DpInvoiceInsertDate.SelectedDate = DateTime.Now;
                CinvoiceList.Visibility = Visibility.Visible;
                BaddItem.Visibility = Visibility.Visible;
                CinvoiceList.IsEnabled = true;
                BsaveInvoice.IsEnabled = true;
                cClearInvoice.IsEnabled = true;

                IsAddOrEditStatus = false;

                LoadItems();
                UpdateMainItemsInvoceList();
            }
            catch (Exception ex)
            {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            
        }


        /// <summary>
        /// This methosd sets up screen status after save Invoice. 
        /// </summary>
        private void AfterSaveWindowStatus()
        {
            try
            {
                TbInvoiceNumber.Text = mainLogic.GetMaxInvoiceNum().ToString();
                BeditInvoice.IsEnabled = true;
                BdeleteInvoice.IsEnabled = true;
                DpInvoiceInsertDate.IsEnabled = false;
                CinvoiceList.IsEnabled = false;
                BaddItem.IsEnabled = false;
                BsaveInvoice.IsEnabled = false;
                cClearInvoice.IsEnabled = false;
                IsAddOrEditStatus = false;
            }
            catch (Exception ex)
            {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodBase.GetCurrentMethod().Name, ex.Message);
            }

        }


        /// <summary>
        /// This method sets up the screen status of edit mode.
        /// </summary>
        private void EditWindowStatus()
        {
            try
            {
                BcreateInvoice.IsEnabled = true;
                BeditInvoice.IsEnabled = true;
                BdeleteInvoice.IsEnabled = true;
                TbInvoiceNumber.Text = "";
                DpInvoiceInsertDate.IsEnabled = true;
                CinvoiceList.IsEnabled = true;
                BsaveInvoice.IsEnabled = true;
                cClearInvoice.IsEnabled = true;
                IsAddOrEditStatus = true;
            }
            catch (Exception ex)
            {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodBase.GetCurrentMethod().Name, ex.Message);
            }

        }



        /// <summary>
        /// This method fills out combo box drop-down list from Item list.
        /// Then user clicks item(s) in order to add to the Invoice list.
        /// </summary>
        private void LoadItems()
        {
            try
            {
                var loadItems = mainLogic.GetAllItemDesc();
                CinvoiceList.ItemsSource = loadItems;

            }
            catch (Exception ex)
            {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// This method loads Invoice for edit and delete.
        /// </summary>
        private void LoadInvocie()
        {

            try
            {
                if (currentInvoice == null)
                {
                    dgDisplaytems.ItemsSource = null;
                    TbTotalCost.Text = String.Format("${0:0.00}", mainLogic.CalculateTotal());
                }

                else
                {
                    mainLogic = new clsMainLogic(currentInvoice);

                    TbInvoiceNumber.Text = currentInvoice.InvoiceNumber.ToString();
                    DpInvoiceInsertDate.SelectedDate = currentInvoice.InvoiceDate;

                    mainLogic.GetLineItmes(currentInvoice.InvoiceNumber);

                    dgDisplaytems.ItemsSource = null;
                    var addedListItems = mainLogic.GetAddedItem();
                    //Update DataGrid list.
                    dgDisplaytems.ItemsSource = addedListItems;

                    TbTotalCost.Text = String.Format("${0:0.00}", mainLogic.CalculateTotal());
                }
               
            }
            catch (Exception)
            {

                throw;
            }

        }



        /// <summary>
        /// Clear all current Invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cDeleteItemInList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //It is disable delete('X') button in the Item list.
                if(!IsAddOrEditStatus)
                {
                    return;
                }

                else
                {
                    Button button = (Button)sender;
                    clsMainLogic.ItemDescInfo deleteLineItem = (clsMainLogic.ItemDescInfo)button.DataContext;
                    mainLogic.DeleteItemInList(deleteLineItem);
                    UpdateMainItemsInvoceList();
                }            

            }
            catch (Exception ex)
            {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodBase.GetCurrentMethod().Name, ex.Message);
            }

        }



        /// <summary>
        /// This method exits program.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodBase.GetCurrentMethod().Name, ex.Message);
            }

        }



        /// <summary>
        /// Handle the error.
        /// </summary>
        /// <param name="sClass">The class in which the error occurred in.</param>
        /// <param name="sMethod">The method in which the error occurred in.</param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                //Would write to a file or database here.
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine +
                                             "HandleError Exception: " + ex.Message);
            }
        }

    
    }
}
