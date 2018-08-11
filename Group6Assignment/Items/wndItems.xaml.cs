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
using Group6Assignment.Main;

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

        /// <summary>
        /// Tells if the user is deleting a row.
        /// </summary>
        bool IsDeleting = false;
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
        /// <summary>
        /// When the wndItems window 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// Only allows letters to be input
        /// </summary>
        /// <param name="sender">sent object</param>
        /// <param name="e">key argument</param>
        private void txtLetterInput_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //Only allow letters to be entered
                if (!(e.Key >= Key.A && e.Key <= Key.Z))
                {
                    //Allow the user to use the backspace, delete, tab, enter and space
                    if (!(e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Tab || e.Key == Key.Enter || e.Key == Key.Space))
                    {
                        //No other keys allowed besides numbers, backspace, delete, tab, and enter
                        e.Handled = true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Only allows numbers to be input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDecimalInput_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!((e.Key >= Key.D0 && e.Key <= Key.D9) || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
                {
                    if (!(e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Enter || e.Key == Key.OemPeriod))
                    {
                        e.Handled = true;
                    }
                }
            }
            catch (System.Exception ex)
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
                gEdit.Visibility = Visibility.Collapsed;
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
                //Get a selected row's object from a datagrid
                clsItem objSelectedItem = (clsItem)dgItemDescTable.SelectedItem;

                if (objSelectedItem != null)
                {
                    gAdd.Visibility = Visibility.Collapsed;
                    gEdit.Visibility = Visibility.Visible;

                    txtEditItemCode.Text = objSelectedItem.ItemCode;
                    txtEditItemDesc.Text = objSelectedItem.ItemDesc;
                    txtEditCost.Text = objSelectedItem.Cost.ToString();
                }
                else
                    MessageBox.Show("Editing item is not selected in a datagrid.");
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
                gAdd.Visibility = Visibility.Collapsed;
                gEdit.Visibility = Visibility.Collapsed;

                //Get a selected row's object from a datagrid
                clsItem objSelectedItem = (clsItem)dgItemDescTable.SelectedItem;

                if (objSelectedItem != null)
                {
                    MessageBoxResult userAnswer = MessageBox.Show("Are you sure to delete this item?", "Delete Confirmation", MessageBoxButton.YesNo);
                    if (userAnswer == MessageBoxResult.Yes)
                    {
                        IsDeleting = true;
                        objItemsLogic.DeleteItem_byRow(objSelectedItem.ItemCode);

                        dgItemDescTable.ItemsSource = objItemsLogic.GetItemCollection();
                        IsDeleting = false;
                    }

                    return;
                }
                else
                    MessageBox.Show("Deleting item is not selected in a datagrid.");
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
                if ((txtAddItemCode.Text == "") || (txtAddItemDesc.Text == "") || (txtAddCost.Text == ""))
                {
                    MessageBox.Show("Please fill out all of textboxes.");
                    return;
                }

                MessageBoxResult userAnswer = MessageBox.Show("Are you sure to add this item?", "Add Confirmation", MessageBoxButton.YesNo);
                if (userAnswer == MessageBoxResult.Yes)
                {                  
                    txtAddItemCode.Text = txtAddItemCode.Text.ToUpper(); //Convert itemCode textbox input to upper case.
                    objItemsLogic.AddItem_byRow(txtAddItemCode.Text, txtAddItemDesc.Text, Convert.ToDecimal(txtAddCost.Text));
                }

                dgItemDescTable.ItemsSource = objItemsLogic.GetItemCollection();
                //objItemsLogic.ColorRow(dgItemDescTable, txtAddItemCode.Text);

                RefreshAdd();
                gAdd.Visibility = Visibility.Collapsed;
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
                RefreshAdd();
                gAdd.Visibility = Visibility.Collapsed;
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
                if ((txtEditItemCode.Text == "") || (txtEditItemDesc.Text == "") || (txtEditCost.Text == ""))
                {
                    MessageBox.Show("Please fill out all of textboxes.");
                    return;
                }

                MessageBoxResult userAnswer = MessageBox.Show("Are you sure to edit this item?", "Edit Confirmation", MessageBoxButton.YesNo);
                if (userAnswer == MessageBoxResult.Yes)
                {                   
                    txtEditItemCode.Text = txtEditItemCode.Text.ToUpper(); //Convert itemCode textbox input to upper case.
                    objItemsLogic.EditItem(txtEditItemCode.Text, txtEditItemDesc.Text, Convert.ToDecimal(txtEditCost.Text));
                }

                dgItemDescTable.ItemsSource = objItemsLogic.GetItemCollection();

                RefreshEdit();
                gEdit.Visibility = Visibility.Collapsed;
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
                RefreshEdit();
                gEdit.Visibility = Visibility.Collapsed;
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
                this.Close();
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
        //        var window = new wndMain();
        //        window.Show();

        //        //this.Close();
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
            try
            {
                var window = new Main.wndMain();
                window.Show();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Clear every textbox in an Add Grid
        /// </summary>
        public void RefreshAdd()
        {
            try
            {
                txtAddItemCode.Clear();
                txtAddItemDesc.Clear();
                txtAddCost.Clear();
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }

        /// <summary>
        /// Clear every textbox in an Edit Grid
        /// </summary>
        public void RefreshEdit()
        {          
            try
            {
                txtEditItemCode.Clear();
                txtEditItemDesc.Clear();
                txtEditCost.Clear();
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
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
