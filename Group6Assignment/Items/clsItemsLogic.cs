﻿/***************************************************************************************************
* Group5Assignment
* clsItemsLogic.cs
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
        /// A SQL class for Items(ItemDesc Table)
        /// </summary>
        clsItemsSQL objItemsSQL;
        #endregion

        #region Constructor
        /// <summary>
        /// A clsItemsLogic constructor
        /// </summary>
        public clsItemsLogic()
        {
            try
            {
                db = new clsDataAccess();
                objItemsSQL = new clsItemsSQL();
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
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
            ObservableCollection<clsItem> items = new ObservableCollection<clsItem>();

            try
            {
                int iRet = 0;
                DataSet ds = new DataSet();
                ds = db.ExecuteSQLStatement(objItemsSQL.SelectAllItems(), ref iRet);

                //for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
                for (int i = 0; i < iRet; i++)
                {
                    //Fill out attributes for each item object
                    clsItem objItem = new clsItem();
                    objItem.ItemCode = ds.Tables[0].Rows[i][0].ToString();
                    objItem.ItemDesc = ds.Tables[0].Rows[i][1].ToString();
                    objItem.Cost = Convert.ToDecimal(ds.Tables[0].Rows[i][2].ToString());

                    //Add an item object to an ObservableCollection<clsItem>
                    items.Add(objItem);
                }
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }

            return items;
        }
        #endregion

        #region List
        /// <summary>
        /// A list of ItemCodes from the ItemDesc table.
        /// It is going to be used to check if a same itemCode already exists or not in an inventory
        /// when the user adds/edits/deletes an item.
        /// </summary>
        /// <returns></returns>
        public List<String> GetItemCodeList_ItemDesc()
        {
            try
            {
                List<string> itemCodeList = new List<string>();

                DataSet ds = new DataSet();
                int iRet = 0;

                ds = db.ExecuteSQLStatement(objItemsSQL.SelectAllItemCode_ItemDesc(), ref iRet);
                for (int i = 0; i < iRet; i++)
                {
                    string sItemCode = ds.Tables[0].Rows[i][0].ToString();

                    itemCodeList.Add(sItemCode);
                }

                return itemCodeList;
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }

        /// <summary>
        /// A list of itemCodes from the LineItems table.
        /// It is going to be used to check if an itemCode exists or not in any invoice
        /// when the user tries to delete an item.
        /// </summary>
        /// <returns></returns>
        public List<String> GetItemCodeList_LineItems()
        {
            try
            {
                List<string> itemCodeList = new List<string>();

                DataSet ds = new DataSet();
                int iRet = 0;

                ds = db.ExecuteSQLStatement(objItemsSQL.SelectAllItemCode_LineItems(), ref iRet);
                for (int i = 0; i < iRet; i++)
                {
                    string sItemCode = ds.Tables[0].Rows[i][0].ToString();

                    itemCodeList.Add(sItemCode);
                }

                return itemCodeList;
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
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
        public void AddItem_byRow(string itemCode, string itemDesc, decimal cost)
        {
            try
            {
                List<string> existingCode = GetItemCodeList_ItemDesc();

                int count = 0;
                foreach (string pk in existingCode)
                {
                    if (itemCode == pk)
                    {
                        count++;
                    }
                }

                if (count == 0)
                {
                    db.ExecuteNonQuery(objItemsSQL.AddItem(itemCode, itemDesc, cost));
                }
                else
                {
                    MessageBox.Show("This item code already exists in an inventory.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }

        /// <summary>
        /// A method to edit an item.
        /// sCode parameter is to select which item to edit.
        /// Only Desc and Cost are allowed to edit.
        /// </summary>
        /// <param name="sCode"></param>
        /// <param name="sDesc"></param>
        /// <param name="dCost"></param>
        public void EditItem(string itemCode, string itemDesc, decimal cost)
        {
            try
            {
                List<string> existingCode = GetItemCodeList_ItemDesc();

                int count = 0;
                foreach (string pk in existingCode)
                {
                    if (itemCode == pk)
                    {
                        count++;
                    }
                }

                if (count != 0)
                {
                    db.ExecuteNonQuery(objItemsSQL.EditItem(itemCode, itemDesc, cost));
                }
                else
                {
                    MessageBox.Show("This item code does not exist in an inventory.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }

        /// <summary>
        /// A method to delete an item by row.
        /// It is not allowed to delete an item which is in an invoice.
        /// Warn the user with a message that tells which invoices that item is used on.
        /// </summary>
        /// <param name="sCode"></param>
        public void DeleteItem_byRow(string itemCode)
        {
            try
            {
                List<string> existingCode = GetItemCodeList_ItemDesc();
                int count = 0;
                foreach (string pk in existingCode)
                {
                    if (itemCode == pk)
                    {
                        count++;
                    }
                }
              
                if (count != 0) //If the item exists in an inventory
                {
                    List<string> existingInInvoice = GetItemCodeList_LineItems();
                    int invoiceCount = 0;
                    foreach (string lineItemCode in existingInInvoice)
                    {
                        if (itemCode == lineItemCode)
                        {
                            invoiceCount++;
                        }
                    }

                    if (invoiceCount == 0) //if itemCode does not exist in any invoice
                    {
                        db.ExecuteNonQuery(objItemsSQL.DeleteItem(itemCode));
                    }
                    else
                    {
                        DataSet ds = new DataSet();
                        int iRet = 0;
                        string lineItemCode = "";

                        ds = db.ExecuteSQLStatement(objItemsSQL.GetLineItem_byRow(itemCode), ref iRet);
                        for (int i = 0; i < iRet; i++)
                        {
                            lineItemCode = ds.Tables[0].Rows[i][0].ToString();
                        }

                        MessageBox.Show("Cannot delete this item. It is used on invoice " + lineItemCode);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }

        /// <summary>
        /// When a new row is added and an existing row is edited,
        /// this method will color that row's background color to yellow.
        /// </summary>
        /// <param name="dg"></param>
        /// <param name="sCode"></param>
        public void ColorRow(DataGrid dg, string pkCode)
        {
            try
            {
                //clsItem objItem = new clsItem();
                //var row = dg.ItemContainerGenerator.ContainerFromItem(objItem) as DataGridRow;

                ////SolidColorBrush brush = new SolidColorBrush(Colors.Yellow);
                //if (objItem.ItemCode == pkCode)
                //{
                //    //row.Background = brush;
                //    row.Background = Brushes.Yellow;
                //}
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
