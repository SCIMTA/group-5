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
        public ActionResult AddToCart(int idBook, int idUser, int quantity)
        {
            db.AddCart(idBook, idUser, quantity);
            TempData[C.TEMPDATA.Message] = "Thêm vào giỏ hàng thành công";
            return RedirectToAction("Detail", "Home", new { id = idBook });
        }
        public ActionResult BuyNow(int[] id)
        {

            return View();
        }
        public ActionResult ShoppingCart(int idUser)
        {
            List<Cart> listCarts = db.GetListCarts(idUser);
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
        public ActionResult DeleteCartItem(int idUser, int idBook)
        {
            db.DeleteCartItem(idUser, idBook);
            return RedirectToAction("ShoppingCart", "User", new { idUser = idUser });
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
            db.UpdateUser(user.Phone, email, fullname, gender, birthday);
            return RedirectToAction("Infor", "User", new { id = user.ID });
        }
    }
}