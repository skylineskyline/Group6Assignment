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
        /// Constructor
        /// </summary>
        public wndMain()
        {
            InitializeComponent();
            this.Closing += ClosingWindowForm;

            mainLogic = new clsMainLogic();
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
                //Load all ItemDesc record for combo box;
                //var items = mainLogic.GetAllItemDesc();
                //CinvoiceList.ItemsSource = items;

                RunningWindowStatus();

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
                MessageBox.Show("", "");
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
                }

                UpdateMainItemsInvoceList();

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
                mainLogic.SaveInvoice();
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
            List<clsMainLogic.ItemDescInfo> addedList = mainLogic.GetAddedItem();

            var addedListItems = mainLogic.GetAddedItem();
            dgDisplaytems.ItemsSource = addedListItems;
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
        /// This method exits program.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
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
                TbInvoiceNumber.IsEnabled = false;

                DpInvoiceInsertDate.Visibility = Visibility.Hidden;
                CinvoiceList.IsEnabled = false;
                BaddItem.IsEnabled = false;
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
        private void RunningWindowStatus()
        {
            try
            {
                TbInvoiceNumber.Visibility = Visibility.Visible;
                TbInvoiceNumber.IsEnabled = true;
                TbInvoiceNumber.Text = "TBD";

                DpInvoiceInsertDate.Visibility = Visibility.Visible;
                DpInvoiceInsertDate.SelectedDate = DateTime.Now;
                CinvoiceList.IsEnabled = true;

                LoadItems();
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
