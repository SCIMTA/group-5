﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using taka.Models.DatabaseInteractive;

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
            TempData[taka.Utils.C.TEMPDATA.Message] = "Thêm vào giỏ hàng thành công";
            return RedirectToAction("Detail", "Home", new { id = idBook });
        }
        public ActionResult BuyNow(int[] id)
        {
            
            return View();
        }
        public ActionResult ShoppingCart(int idUser)
        {
            List<Models.Enitities.Cart> listCarts = db.GetListCarts(idUser);
            return View(listCarts);
        }
        [HttpPost]
        public JsonResult ChangeQuantity(int idCart, int quantity)
        {
            try
            {
                db.ChangeQuantity(idCart, quantity);
                return Json(new { status = 1, JsonRequestBehavior.AllowGet });
            }
            catch (Exception)
            {
                return Json(new { status = 0, JsonRequestBehavior.AllowGet });
            }
        }
        public ActionResult DeleteCartItem(int idUser, int idBook)
        {
            db.DeleteCartItem(idUser, idBook);
            return RedirectToAction("ShoppingCart", "User", new { idUser = idUser });
        }
    }
}