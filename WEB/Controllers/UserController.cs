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
        public ActionResult AddToCart(int idBook, int quantity)
        {
            User user = (User)Session[C.SESSION.UserInfo];
            db.AddCart(idBook, user.ID, quantity);
            TempData[C.TEMPDATA.Message] = "Thêm vào giỏ hàng thành công";
            return RedirectToAction("Detail", "Home", new { id = idBook });
        }
        public ActionResult BuyNow(int[] id)
        {
            var listItems = db.GetBillItems(id);
            User user = (User)Session[C.SESSION.UserInfo];
            ViewBag.addresses = db.GetAddresses(user.ID);
            return View(listItems);
        }
        [HttpPost]
        public ActionResult CheckOut(int[] id)
        {
            return RedirectToAction("Index", "Home");
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
    }
}