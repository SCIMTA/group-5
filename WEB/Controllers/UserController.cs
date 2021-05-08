using System;
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
    }
}