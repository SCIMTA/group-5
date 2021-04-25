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
        private Taka db = new Taka();
        // GET: Book
        public ActionResult Index()
        {
            //BookDAO dao = new BookDAO();
            //IQueryable<Book> lst = dao.ListBook();
            var lst = db.Books.ToList();
            return View(lst);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Book book)
        {
            BookDAO dao = new BookDAO();
            //var categorylist = db.Categories.ToList();
            //ViewBag.idCategory = new SelectList(categorylist, "ID", "Name");
            dao.InsertBook(book.Title, (decimal)book.Price, (int)book.Page, (int)book.Year, (int)book.Quantity, book.Description,
                                ViewBag.idCategory, (int)book.idType, (int)book.idPublisher, (int)book.idLanguage, (int)book.idAuthor);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int ID)
        {
            BookDAO dao = new BookDAO();

            Book book = dao.FindBookById(ID);

            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            BookDAO dao = new BookDAO();
            dao.UpdateBook(book);
            return RedirectToAction("Index");
        }
    }
}