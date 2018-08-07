using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group6Assignment.Items
{
    class clsItemsSQL
    {
        /// <summary>
        /// This method gets all items from the ItemDesc table order by ascending ItemCode.
        /// </summary>
        /// <returns></returns>
        public string SelectAllItems()
        {
            string sSQL = "SELECT ItemCode, ItemDesc, Cost " +
                          "FROM ItemDesc " +
                          "ORDER BY ItemCode ASC";
            return sSQL;
        }

        /// <summary>
        /// This method gets all ItemCodes(ItemCodes column) from ItemDesc table by ascending order.
        /// </summary>
        /// <returns></returns>
        public string SelectAllItemCode()
        {
            string sSQL = "SELECT ItemCode " +
                          "FROM ItemDesc " +
                          "ORDER BY ItemCode ASC";
            return sSQL;
        }

        /// <summary>
        /// This method gets all ItemDesc searched by a specific keyword in ItemDesc by user.
        /// </summary>
        /// <param name="sUserInput"></param>
        /// <returns></returns>
        public string SelectAllSearchedItemDesc(string sUserInput)
        {
            string sSQL = String.Format(
                          "SELECT ItemDesc " +
                          "FROM ItemDesc " +
                          "WHERE ItemDesc LIKE {0} " +
                          "ORDER BY ItemCode"
                          , sUserInput);
            return sSQL;
        }

        /// <summary>
        /// This method gets all Costs(Cost column) from the ItemDesc table.
        /// </summary>
        /// <returns></returns>
        public string SelectAllCost()
        {
            string sSQL = "SELECT Cost " +
                          "FROM ItemDesc " +
                          "ORDER BY Cost ASC";
            return sSQL;
        }

        /// <summary>
        /// This method gets all of data of the item by row by searching an ItemCode.
        /// </summary>
        /// <param name="sItemCode"></param>
        /// <returns></returns>
        public string SelectItemBy_ItemCode(string sItemCode)
        {
            string sSQL = String.Format(
                          "SELECT ItemCode, ItemDesc, Cost " +
                          "FROM ItemDesc " +
                          "WHERE ItemCode = {0}"
                          , sItemCode);
            return sSQL;
        }

        /// <summary>
        /// This method gets all of data of the item by row by searching a keyword for the ItemDesc.
        /// </summary>
        /// <param name="sItemDesc"></param>
        /// <returns></returns>
        public string SelectItemBy_ItemDesc(string sItemDesc)
        {
            string sSQL = String.Format(
                        "SELECT ItemCode, ItemDesc, Cost " +
                        "FROM ItemDesc " +
                        "WHERE ItemDesc LIKE {0} " +
                        "ORDER BY ItemCode ASC"
                        , sItemDesc);
            return sSQL;
        }

        /// <summary>
        /// This method gets all of data of the item by row by searching a cost.
        /// </summary>
        /// <param name="dCost"></param>
        /// <returns></returns>
        public string SelectItemBy_Cost(decimal dCost)
        {
            string sSQL = "SELECT ItemCode, ItemDesc, Cost " +
                        "FROM ItemDesc " +
                        "WHERE Cost = " + dCost +
                        " ORDER BY ItemCode ASC";
            return sSQL;
        }

        /// <summary>
        /// This method adds a new item to the ItemDesc table by row.
        /// </summary>
        /// <param name="sItemCode"></param>
        /// <param name="sItemDesc"></param>
        /// <param name="dCost"></param>
        /// <returns></returns>
        public string AddItem(string sItemCode, string sItemDesc, decimal dCost)
        {
            string sSQL = String.Format(
                        "INSERT INTO ItemDesc (ItemCode, ItemDesc, Cost) " +
                        "VALUES ('{0}', '{1}', {2})",
                        sItemCode, sItemDesc, dCost);
            return sSQL;
        }
        
        /// <summary>
        /// This method updates/edits an existing item from the ItemDesc table by row.
        /// It updates only ItemDesc and Cost, not ItemCode because ItemCode is a primary key.
        /// </summary>
        /// <param name="sItemCode"></param>
        /// <param name="sItemDesc"></param>
        /// <param name="dCost"></param>
        /// <returns></returns>
        public string EditItem(string sItemCode, string sItemDesc, decimal dCost)
        {
            string sSQL = String.Format(
                        "UPDATE ItemDesc " +
                        "SET ItemDesc = '{1}', Cost = {2} " +
                        "WHERE ItemCode = '{0}'",
                        sItemCode, sItemDesc, dCost);
            return sSQL;
        }

        /// <summary>
        /// This method deletes an existing item from the ItemDesc table by row.
        /// It takes an ItemCode as a parameter to select which item to delete.
        /// </summary>
        /// <param name="sItemCode"></param>
        /// <returns></returns>
        public string DeleteItem(string sItemCode)
        {
            string sSQL = "DELETE FROM ItemDesc " +
                        "WHERE ItemCode = " + "'" +sItemCode + "'";
            return sSQL;
        }
    }
}
