using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using taka.Models.Enitities;

namespace taka.Models.DAO
{
    public class BookDAO
    {
        Taka db;
        public BookDAO()
        {
            db = new Taka();
        }

        public int InsertBook(string title, decimal price, int page, int year, int quantity, string description,
                                int idcategory, int idtype, int idpublisher, int idlanguage, int idauthor)
        {
            Book book = new Book();
            book.Title = title;
            book.Price = price;
            book.Page = page;
            book.Year = year;
            book.Quantity = quantity;
            book.Description = description;
            book.idCategory = idcategory;
            book.idType = idtype;
            book.idPublisher = idpublisher;
            book.idLanguage = idlanguage;
            book.idAuthor = idauthor;
            db.Books.Add(book);
            db.SaveChanges();
            return book.ID;
        }

        public IQueryable<Book> Books
        {
            get { return db.Books; }
        }

        public IQueryable<Book> ListBook()
        {
            var res = (from b in db.Books
                       where b.Price > 1000 || b.Title.Length > 2
                       orderby b.ID ascending
                       select b);
            return res;
        }

        public void UpdateBook(Book bookTmp)
        {
            Book book = db.Books.Find(bookTmp.ID);
            if (book != null)
            {
                book.Title = bookTmp.Title;
                book.Price = bookTmp.Price;
                book.Page = bookTmp.Page;
                book.Year = bookTmp.Year;
                book.Quantity = bookTmp.Quantity;
                book.Description = bookTmp.Description;

                book.idCategory = bookTmp.idCategory;
                book.idType = bookTmp.idType;
                book.idPublisher = bookTmp.idPublisher;
                book.idLanguage = bookTmp.idLanguage;
                book.idAuthor = bookTmp.idAuthor;
                db.SaveChanges();
            }
        }

        public void DeleteBook(Book bookTmp)
        {
            Book book = db.Books.Find(bookTmp.ID);
            if (book != null)
            {
                db.Books.Remove(book);
                db.SaveChanges();
            }
        }

        public Book FindBookById(int id)
        {
            return db.Books.Find(id);
        }
    }
}