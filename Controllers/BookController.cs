using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using taka.Models.Enitities;
using taka.Models.DAO;

namespace taka.Controllers
{
    public class BookController : Controller
    {
        private BookUtils bookUtils = new BookUtils();

        // GET: Book
        public ActionResult Index()
        {
            return View(bookUtils.Books.ToList());
        }

        public ActionResult Add()
        {
            Taka db = bookUtils.getDatabase();
            ViewBag.listCategories = db.Categories.ToList(); ;
            ViewBag.listPublishers = db.Publishers.ToList();
            ViewBag.listLanguages = db.Languages.ToList();
            ViewBag.listAuthors = db.Authors.ToList();
            ViewBag.listTypes = db.Types.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Add(Book book)
        {
            bookUtils.InsertBook(book.Title, (decimal)book.Price, (int)book.Page, (int)book.Year, (int)book.Quantity, book.Description,
                                (int)book.idCategory, (int)book.idType, (int)book.idPublisher, (int)book.idLanguage, (int)book.idAuthor);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int ID)
        {
            Taka db = bookUtils.getDatabase();
            ViewBag.listCategories = db.Categories.ToList(); ;
            ViewBag.listPublishers = db.Publishers.ToList();
            ViewBag.listLanguages = db.Languages.ToList();
            ViewBag.listAuthors = db.Authors.ToList();
            ViewBag.listTypes = db.Types.ToList();

            Book book = bookUtils.FindBookById(ID);
            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            bookUtils.UpdateBook(book);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int ID)
        {
            Taka db = bookUtils.getDatabase();
            Book book = bookUtils.FindBookById(ID);
            return View(book);
        }

        public ActionResult Delete(int ID)
        {
            Taka db = bookUtils.getDatabase();
            Book book = bookUtils.FindBookById(ID);
            return View(book);
        }

        [HttpPost]
        public ActionResult Delete(Book book)
        {
            bookUtils.DeleteBook(book);
            return RedirectToAction("Index");
        }
    }
}