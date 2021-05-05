using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using taka.Models.DatabaseInteractive;
using taka.Models.Enitities;
using taka.Utils;

namespace taka.Controllers
{
    public class HomeController : Controller
    {

        TakaDB dB = new TakaDB();

        public ActionResult Index(int page = 1, string text = "", int cate = 0, int sort = 0)
        {
            ViewBag.ListCate = dB.GetCategories();
            ViewBag.Cate = cate;
            ViewBag.Sort = sort;
            ViewBag.CurrentPage = page;
            if (cate != 0)
            {
                ViewBag.TextCate = dB.findTextCategory(cate);
            }
            if (sort != 0)
            {
                ViewBag.TextSort = sort == 1 ? "Giá thấp nhất" : "Giá cao nhất";
            }
            ListBook listBook = dB.GetListBook(page, text, cate, sort);
            ViewBag.ListPage = HelperFunctions.getNumPage(page, listBook.pages);
            ViewBag.maxPage = listBook.pages;
            ViewBag.TextSearch = text;
            return View(listBook.books);
        }

        [HttpPost]
        public ActionResult Login(string username, string password, string callbackUrl)
        {
            return Redirect(callbackUrl);
        }

        public ActionResult Logout()
        {
            Session["isLogin"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Detail(int id = -1)
        {
            try
            {
                if (id == -1)
                    throw new Exception("Not found");
                var item = dB.GetBookDetail(id);
                return View(item);
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home");
            }

        }

        public ActionResult Edit(int id = -1)
        {
            ViewBag.listCategories = dB.GetCategories();
            ViewBag.listAuthors = dB.GetAuthors();
            ViewBag.listPublishers = dB.GetPublishers();
            ViewBag.listLanguages = dB.GetLanguages();
            return Detail(id);
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
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home");
            }

        }

        public ActionResult Add()
        {

            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}