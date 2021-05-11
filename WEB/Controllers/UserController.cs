﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using taka.Models.DatabaseInteractive;

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
            return RedirectToAction("Index", "Home");
        }
        public ActionResult BuyNow(int [] id)
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
            return RedirectToAction("ShoppingCart", "User", new { idUser = idUser});
        }
    }
}