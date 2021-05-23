using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using taka.Models.DatabaseInteractive;
using taka.Models.Enitities;
using taka.Utils;

namespace taka.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        TakaDB db = new TakaDB();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Purchased()
        {
            User user = (User)Session[C.SESSION.UserInfo];
            List<Order> processingOrder = db.GetProcessingOrders(user.ID);
            List<Order> doneOrder = db.GetDoneOrders(user.ID);
            ViewBag.ProcessingOrders = processingOrder;
            ViewBag.ProcessingOrdersAddresses = processingOrder.Select(x => db.GetAddressByIdAddress(x.IDAddress)).ToList();
            ViewBag.DoneOrders = doneOrder;
            ViewBag.DoneOrdersAddresses = doneOrder.Select(x => db.GetAddressByIdAddress(x.IDAddress)).ToList();
            return View();
        }
        public ActionResult AddToCart(int idBook, int quantity)
        {
            User user = (User)Session[C.SESSION.UserInfo];
            db.AddCart(idBook, user.ID, quantity);
            TempData[C.TEMPDATA.Message] = "Thêm vào giỏ hàng thành công";
            return RedirectToAction("Detail", "Home", new { id = idBook });
        }
        public ActionResult BuyNow(int idBook, int quantity)
        {
            User user = (User)Session[C.SESSION.UserInfo];
            return RedirectToAction("Payment", "User", new { idCarts = db.AddCart(idBook, user.ID, quantity).ID });
        }
        public ActionResult Payment(int[] idCarts)
        {
            var listItems = db.GetBillItems(idCarts);
            User user = (User)Session[C.SESSION.UserInfo];
            ViewBag.addresses = db.GetListAddressByUserId(user.ID);
            return View(listItems);
        }
        [HttpPost]
        public ActionResult CheckOut(int[] id_cart, int id_address, int totalPrice, int shipFee, string shipper, string fullName, string phone, string address, string message)
        {
            User user = (User)Session[C.SESSION.UserInfo];
            db.CheckOut(id_cart, id_address, totalPrice, shipper, user.ID, fullName, phone, address, message, shipFee);
            return RedirectToAction("Purchased", "User");
        }
        public ActionResult ShoppingCart()
        {
            User user = (User)Session[C.SESSION.UserInfo];
            List<Cart> listCarts = db.GetListCarts(user.ID);
            return View(listCarts);
        }
        [HttpPost]
        public JsonResult ChangeQuantity(int idCart, int quantity)
        {
            try
            {
                db.ChangeQuantity(idCart, quantity);
                return Json(new { status = 1 });
            }
            catch (Exception)
            {
                return Json(new { status = 0 });
            }
        }
        public ActionResult DeleteCartItem(int idBook)
        {
            User user = (User)Session[C.SESSION.UserInfo];
            db.DeleteCartItem(user.ID, idBook);
            return RedirectToAction("ShoppingCart", "User", new { idUser = user.ID });
        }

        public ActionResult Infor()
        {
            User user = (User)Session[C.SESSION.UserInfo];
            return View(user);
        }

        public ActionResult EditUser()
        {
            User user = (User)Session[C.SESSION.UserInfo];
            return View(user);
        }

        [HttpPost]
        public ActionResult EditUser(string email, string fullname, string gender, string birthday)
        {
            User user = (User)Session[C.SESSION.UserInfo];
            Session[C.SESSION.UserInfo] = db.UpdateUser(user.Phone, email, fullname, gender, birthday);
            return RedirectToAction("Infor", "User", new { id = user.ID });
        }

        public ActionResult AddressDetails()
        {
            User user = (User)Session[C.SESSION.UserInfo];
            List<Address> listadr = db.GetListAddressByUserId(user.ID);
            return View(listadr);
        }

        public ActionResult AddAddress()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAddress(string name, string phone, string address)
        {
            User user = (User)Session[C.SESSION.UserInfo];
            db.AddAddress(user.ID, name, phone, address);
            return RedirectToAction("AddressDetails", "User");
        }

        public ActionResult EditAddress(int idAddress)
        {
            User user = (User)Session[C.SESSION.UserInfo];
            Address adr = db.GetAddressByIdAddress(idAddress);
            return View(adr);
        }
        [HttpPost]
        public ActionResult EditAddress(int idAddress, string name, string phone, string address)
        {
            User user = (User)Session[C.SESSION.UserInfo];
            db.EditAddress(idAddress, user.ID, name, phone, address);
            return RedirectToAction("AddressDetails", "User");
        }
        [HttpPost]
        public ActionResult DeleteAddress(int idAddress)
        {
            User user = (User)Session[C.SESSION.UserInfo];
            db.DeleteAddress(idAddress);
            return RedirectToAction("AddressDetails", "User");
        }
    }
}