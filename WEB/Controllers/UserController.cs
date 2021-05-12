using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using taka.Models.DatabaseInteractive;
using taka.Models.Enitities;

namespace taka.Controllers
{
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
            TempData[taka.Utils.C.TEMPDATA.Message] = "Thêm vào giỏ hàng thành công";
            return RedirectToAction("Detail", "Home", new { id = idBook });
        }
        public ActionResult BuyNow(int[] id)
        {
            ViewBag.count = id.Length;

            return View();
        }
        public ActionResult ShoppingCart(int idUser)
        {
            List<Models.Enitities.Cart> listCarts = db.GetListCarts(idUser);
            return View(listCarts);
        }
        public ActionResult DeleteCartItem(int idUser, int idBook)
        {
            db.DeleteCartItem(idUser, idBook);
            return RedirectToAction("ShoppingCart", "User", new { idUser = idUser });
        }

        public ActionResult Infor(int id)
        {
            User user = db.GetUserDetail(id);
            return View(user);
        }
        public ActionResult EditUser(int id)
        {
            User user = db.GetUserDetail(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult EditUser(int id, string phone, string email, string fullname, string gender, string birthday)
        {
            User user = db.GetUserDetail(id);

            //if (ModelState.IsValid)
            //{
            //    user.Email = email;
            //    user.Fullname = fullname;
            //    user.Gender = gender;
            //    user.Birthday = birthday;

            //    db.EditUser(user);
            //    return View("Infor", "User", new { id = id});
            //}
            //else
            //{
            //    return View(user);
            //}
            db.EditUser(user);
            return View();
        }
    }
}