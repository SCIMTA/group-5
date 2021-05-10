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
    public class AdminController : Controller
    {

        TakaDB dB = new TakaDB();
        // GET: Admin
        public ActionResult Index()
        {
            bool isLogin = Session[C.SESSION.UserInfo] != null;
            return View();
        }
        public ActionResult Edit(int id = -1)
        {
            ViewBag.listCategories = dB.GetCategories();
            ViewBag.listAuthors = dB.GetAuthors();
            ViewBag.listPublishers = dB.GetPublishers();
            ViewBag.listLanguages = dB.GetLanguages();
            ViewBag.listTypes = dB.GetTypes();
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


        [HttpPost]
        public ActionResult AddBook(
            IEnumerable<HttpPostedFileBase> Images,
            string Title,
            int Price,
            int idCategory,
            int idAuthor,
            int idPublisher,
            int idLanguage,
            int idType,
            string Page,
            int Quantity,
            string Description)
        {
            Book book = dB.AddBook(Images,Title, Price, idCategory, idAuthor, idPublisher, idLanguage, idType, Page, Quantity, Description);
            return RedirectToAction("Detail", "Home",new { id = book.ID});
        }

        public ActionResult Add()
        {
            ViewBag.listCategories = dB.GetCategories();
            ViewBag.listPublishers = dB.GetPublishers();
            ViewBag.listAuthors = dB.GetAuthors();
            ViewBag.listLanguages = dB.GetLanguages();
            ViewBag.listTypes = dB.GetTypes();
            return View();
        }

        public ActionResult Manager(string tab = "statistic", string text = "")
        {
            ViewBag.listUsers = dB.GetUsers();
            ViewBag.listCategories = dB.GetCategories();
            ViewBag.listPublishers = dB.GetPublishers();
            ViewBag.listAuthors = dB.GetAuthors();
            ViewBag.listTypes = dB.GetTypes();
            ViewBag.listLanguages = dB.GetLanguages();
            ViewBag.tab = tab;
            return View();
        }

        public ActionResult UpdateUser(string phone, string email, string fullname, string gender, string birthday)
        {
            dB.UpdateUser(phone, email, fullname, gender, birthday);
            return RedirectToAction("Manager", "Admin", new { tab = "user" });
        }

        public ActionResult UpdateCategory(int id, string name)
        {
            dB.UpdateCategory(id, name);
            return RedirectToAction("Manager", "Admin", new { tab = "category" });
        }

        public ActionResult AddCategory(string name)
        {
            dB.AddCategory(name);
            return RedirectToAction("Manager", "Admin", new { tab = "category" });
        }

        public ActionResult RemoveCategory(int id)
        {
            dB.RemoveCategory(id);
            return RedirectToAction("Manager", "Admin", new { tab = "category" });
        }

        public ActionResult UpdateLanguage(int id, string name)
        {
            dB.UpdateLanguage(id, name);
            return RedirectToAction("Manager", "Admin", new { tab = "language" });
        }

        public ActionResult AddLanguage(string name)
        {
            dB.AddLanguage(name);
            return RedirectToAction("Manager", "Admin", new { tab = "language" });
        }

        public ActionResult RemoveLanguage(int id)
        {
            dB.RemoveLanguage(id);
            return RedirectToAction("Manager", "Admin", new { tab = "language" });
        }

        public ActionResult UpdatePublisher(int id, string name)
        {
            dB.UpdatePublisher(id, name);
            return RedirectToAction("Manager", "Admin", new { tab = "publisher" });
        }

        public ActionResult AddPublisher(string name)
        {
            dB.AddPublisher(name);
            return RedirectToAction("Manager", "Admin", new { tab = "publisher" });
        }

        public ActionResult RemovePublisher(int id)
        {
            dB.RemovePublisher(id);
            return RedirectToAction("Manager", "Admin", new { tab = "publisher" });
        }


        public ActionResult UpdateAuthor(int id, string name)
        {
            dB.UpdateAuthor(id, name);
            return RedirectToAction("Manager", "Admin", new { tab = "author" });
        }

        public ActionResult AddAuthor(string name)
        {
            dB.AddAuthor(name);
            return RedirectToAction("Manager", "Admin", new { tab = "author" });
        }

        public ActionResult RemoveAuthor(int id)
        {
            dB.RemoveAuthor(id);
            return RedirectToAction("Manager", "Admin", new { tab = "author" });
        }


        public ActionResult UpdateType(int id, string name)
        {
            dB.UpdateType(id, name);
            return RedirectToAction("Manager", "Admin", new { tab = "type" });
        }

        public ActionResult AddType(string name)
        {
            dB.AddType(name);
            return RedirectToAction("Manager", "Admin", new { tab = "type" });
        }

        public ActionResult RemoveType(int id)
        {
            dB.RemoveType(id);
            return RedirectToAction("Manager", "Admin", new { tab = "type" });
        }
    }
}