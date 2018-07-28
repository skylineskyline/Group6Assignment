/***************************************************************************************************
* Group5Assignment
* clsMainLogic.cs
* Dongmin Kim, Kyle Kippen, Goeun Kwak
* CS3280 Group assignment - Jewelry Invoice.
*
***************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Reflection;
using System.Collections.ObjectModel; //Must be added to use ObservableCollection
using System.ComponentModel;          //Must be added for interface INotifyPropertyChanged
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace Group6Assignment.Items
{
    class clsItemsLogic
    {
        #region Attributes
        /// <summary>
        /// A class which connects to the clsDataAccess class
        /// </summary>
        clsDataAccess db;
        /// <summary>
        /// DataSet type
        /// </summary>
        DataSet ds;
        /// <summary>
        /// A SQL class for Items(ItemDesc Table)
        /// </summary>
        clsItemsSQL SQL_Items;
        #endregion

        #region Constructor
        /// <summary>
        /// A clsItemsLogic constructor
        /// </summary>
        public clsItemsLogic()
        {
            db = new clsDataAccess();
            ds = new DataSet();
            SQL_Items = new clsItemsSQL();
        }
        #endregion

        #region ObservableCollection<clsItem>
        /// <summary>
        /// An ObservableCollection to hold clsItem objects.
        /// 
        /// I'm going to assign every item object to this ObservableCollection using a related sql method.
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<clsItem> GetItemCollection()
        {
            ObservableCollection<clsItem> itemCltn = new ObservableCollection<clsItem>();

            
    
            return itemCltn;
        }
        #endregion

        #region List
        /// <summary>
        /// A list of ItemCode.
        /// It is going to be used to check if a same itemCode already exists or not 
        /// when the user adds/edits/deletes an item.
        /// </summary>
        /// <returns></returns>
        public List<String> GetItemCodeList()
        {
            List<String> itemCodeList = new List<String>();



            return itemCodeList;
        }
        #endregion

        #region Methods
        /// <summary>
        /// A method to add an item by row.
        /// It takes ItemCode, ItemDesc, and Cost as parameters from the user.
        /// </summary>
        /// <param name="sCode"></param>
        /// <param name="sDesc"></param>
        /// <param name="dCost"></param>
        public void AddItem_byRow(string sCode, string sDesc, decimal dCost)
        {

        }

        /// <summary>
        /// A method to edit an item.
        /// sCode parameter is to select which item to edit.
        /// Only Desc and Cost are allowed to edit.
        /// </summary>
        /// <param name="sCode"></param>
        /// <param name="sDesc"></param>
        /// <param name="dCost"></param>
        public void EditItem(string sCode, string sDesc, decimal dCost)
        {

        }

        /// <summary>
        /// A method to delete an item by row.
        /// </summary>
        /// <param name="sCode"></param>
        public void DeleteItem_byRow(string sCode)
        {

        }

        /// <summary>
        /// When the user adds/edits/deletes an item and clicks "Save" button,
        /// this method updates DataGrid.
        /// </summary>
        public void UpdateDataGrid(DataGrid dg)
        {

        }

        /// <summary>
        /// When a new row is added and an existing row is edited,
        /// this method will color that row's background color to yellow.
        /// </summary>
        /// <param name="dg"></param>
        /// <param name="sCode"></param>
        public void ColorRow(DataGrid dg, string sCode)
        {
            //clsItem objItem = new clsItem();
            //var row = dg.ItemContainerGenerator.ContainerFromItem(objItem) as DataGridRow;

            ////SolidColorBrush brush = new SolidColorBrush(Colors.Yellow);
            //if (objItem.ItemCode == sCode)
            //{
            //    //row.Background = brush;
            //    row.Background = Brushes.Yellow;
            //}
        }

        /// <summary>
        /// When the user adds/edits/deletes an item and clicks "Cancel" button,
        /// this method clears data in textbox.
        /// </summary>
        public void Clear(TextBox txt)
        {
            txt.Text = " ";
        }

        /// <summary>
        /// Enable a button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void EnableButton(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            btn.IsEnabled = true; ;
        }

        /// <summary>
        /// Disable a button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DisableButton(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            btn.IsEnabled = false;
        }

        //public void Visible()
        //{

        //}
        //public void Hidden()
        //{

        //}

        /// <summary>
        /// Validate user input in a textbox.
        /// </summary>
        /// <param name="sInput"></param>
        /// <returns></returns>
        public bool ValidateInput(string sInput)
        {
            bool bValid = false;
            return bValid;
        }
        #endregion
    }
}
