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
        /// Constructor
        /// </summary>
        public wndMain()
        {
            InitializeComponent();

            mainLogic = new clsMainLogic();
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
        private void MsearchInvoice_Click(object sender, RoutedEventArgs e)
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
                clsMainLogic.ItemDesc itemDesc = (clsMainLogic.ItemDesc)CinvoiceList.SelectedItem;
                mainLogic.AddInvoiceItem(itemDesc);

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

            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// This method updates list of LineItem on the main screen
        /// </summary>
        public void UpdateMainLineItmes()
        {

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
