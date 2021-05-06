using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using taka.Models.DatabaseInteractive;

namespace taka.Controllers
{
    public class AdminController : Controller
    {
        TakaDB dB = new TakaDB();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit(int id = -1)
        {
            ViewBag.listCategories = dB.GetCategories();
            ViewBag.listAuthors = dB.GetAuthors();
            ViewBag.listPublishers = dB.GetPublishers();
            ViewBag.listLanguages = dB.GetLanguages();
            try
            {
                if (id == -1)
                    throw new Exception("Not found");
                var item = dB.GetBookDetail(id);
                return View(item);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }


        [HttpPost]
        public ActionResult EditBook(int MA_DAU_SACH,
            HttpPostedFileBase BIA_SACH,
            string TEN_DAU_SACH,
            int MA_THE_LOAI,
            int MA_NHA_XUAT_BAN,
            int MA_TAC_GIA,
            int GIA_TRI,
            int SO_TRANG,
            int TONG_SO_LUONG,
            int SO_LUONG_CON_LAI,
            string MO_TA_SACH)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id = -1)
        {
            try
            {
                if (id == -1)
                    throw new Exception("Not found");
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }

        }

        public ActionResult Add()
        {
            ViewBag.listCategories = dB.GetCategories();
            ViewBag.listAuthors = dB.GetAuthors();
            ViewBag.listPublishers = dB.GetPublishers();
            ViewBag.listLanguages = dB.GetLanguages();
            return View();
        }
    }
}