using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Apartment.BLL;

namespace Apartment.Controllers
{
    public class PortalController : Controller
    {
        //
        // GET: /Portal/

        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Inventory(string ItemID)
        public ActionResult Inventory()
        {
            string ItemID = "Viscose";
            InventoryBLL IBLL = new InventoryBLL();
            ViewBag.ItemID = ItemID;
            return View("Inventory", IBLL.GetItem(ItemID));
        }

        [HttpGet]
        [ActionName("Edit")]
        //public ActionResult EditInventory(string ItemID)
        public ActionResult EditInventory()
        {
            string ItemID = "Pillow";
            InventoryBLL IBLL = new InventoryBLL();

            return View("EditInventory", IBLL.GetItem(ItemID));
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult SaveInventory(FormCollection Data)
        {

            return View("EditInventory");
        }
    }
}
