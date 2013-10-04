using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Apartment.Models;
using System.Data.Linq.SqlClient;


namespace Apartment.BLL
{
    public class InventoryBLL
    {
        public IQueryable<Inventory> GetInventory()
        {
            IQueryable<Inventory> _InventoryItems;

            using (AMPortal_Entities DBEntity = new AMPortal_Entities())
            {
                _InventoryItems = from I in DBEntity.Inventories
                                  select I;
            }

            return _InventoryItems;
        }

        public Inventory GetItem(string ItemID)
        {
            Inventory _Item;

            using (AMPortal_Entities DBEntity = new AMPortal_Entities())
            {
                _Item = (from I in DBEntity.Inventories
                         //where SqlMethods.Like(I.Item, (ItemID+"%"))
                         where I.Item.Contains(ItemID)
                         select I).First();
            }

            return _Item;
        }
    }
}