/***************************************************************************************************
* Group5Assignment
* wndItems.cs
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

using System.Collections.ObjectModel; //Must be added to use ObservableCollection
using System.ComponentModel;          //Must be added for interface INotifyPropertyChanged
using System.Reflection; //For exception handling

namespace Group6Assignment.Items
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        #region Attributes
        /// <summary>
        /// A new instance of the ObservableCollection<clsItem> class that contains elements copied from the ObservableCollection<clsItem>.
        /// Get Items DataGrid data.
        /// </summary>
        IEnumerable<clsItem> itemsData;
        /// <summary>
        /// An object of clsItemsLogic
        /// </summary>
        clsItemsLogic objItemsLogic;
        #endregion

        #region Constructor
        /// <summary>
        /// A constructor for an Items window
        /// It is going to pull ItemsDesc Table data in database.
        /// </summary>
        public wndItems()
        {
            InitializeComponent();

            objItemsLogic = new clsItemsLogic();
        }
        #endregion

        #region Event handler
        private void wndItemsBinding_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dgItemDescTable.ItemsSource = objItemsLogic.GetItemCollection();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// If this AddItem button is clicked, it will check if that item already exists or not.
        /// If it does not exist, it will add a row. If it already exists, it will not add that item 
        /// and give a message to the user.
        /// It will disable "Add/Edit/Delete an item" buttons and show a grid for adding an item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                gAdd.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// If this EditItem button is clicked, it will check if that item exists or not.
        /// If it exists, it will edit an item description or a cost.
        /// If it does not exist, it will give a message to the user.
        /// It will disable "Add/Edit/Delete an item" buttons and show a grid for editing an item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //gEdit.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// If this DeleteItem button is clicked, it will check if that item exists or not.
        /// If it exists, it will delete an item by row.
        /// If it does not exist, it wil give a message to the user.
        /// It will disable "Add/Edit/Delete an item" buttons and show a grid for deleting an item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //gDelete.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// It will save changes and reflect the change to the DataGrid when the user adds an item,
        /// hide a grid for adding an item 
        /// and enable "Add/Edit/Delete an item" buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                objItemsLogic.AddItem_byRow(txtAddItemCode.Text, txtAddItemDesc.Text, Convert.ToDecimal(txtAddCost.Text));
                dgItemDescTable.ItemsSource = objItemsLogic.GetItemCollection();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// When the user clicks "Add an item" button but decides not to do it,
        /// the user clicks this "Cancel" button.
        /// It will clear any data that the user entered in textboxes
        /// and enable "Add/Edit/Delete an item" buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //TextBox txtContent = (TextBox)sender;
                //objItemsLogic.Clear(txtContent);

                //objItemsLogic.Clear(txtAddItemCode);
                //objItemsLogic.Clear(txtAddItemDesc);
                //objItemsLogic.Clear(txtAddCost);
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// It will save changes and reflect the change to the DataGrid when the user edits an item,
        /// hide a grid for editing an item
        /// and enable "Add/Edit/Delete an item" buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// When the user clicks "Edit an item" button but decides not to do it,
        /// the user clicks this "Cancel" button.
        /// It will clear any data that the user entered in textboxes
        /// and enable "Add/Edit/Delete an item" buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// It will save changes and reflect the change to the DataGrid when the user deletes an item,
        /// hide a grid for deleting an item
        /// and enable "Add/Edit/Delete an item" buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// When the user clicks "Delete an item" button but decides not to do it,
        /// the user clicks this "Cancel" button.
        /// It will clear any data that the user entered in textboxes
        /// and enable "Add/Edit/Delete an item" buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// If the user clicks this "Cancel" button,
        /// it will close Items window and go back to Main window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItemCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        ///// <summary>
        ///// It will be triggered when the Items window is closed.
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void ItemsWindow_Closed(object sender, EventArgs e)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //        HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
        //            MethodInfo.GetCurrentMethod().Name, ex.Message);
        //    }
        //}


        /// <summary>
        /// It will be triggered when the Items window is closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var window = new Main.wndMain();
            window.Show();
        }
        #endregion

        #region Exception Handler
        /// <summary>
        /// Handles the error
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
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }
        #endregion
    }
}
