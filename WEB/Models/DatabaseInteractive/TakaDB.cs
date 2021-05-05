using System.Collections.Generic;
using System.Linq;
using taka.Models.Enitities;
using taka.Utils;

namespace taka.Models.DatabaseInteractive
{
    public class ListBook
    {
        public int pages { get; set; }

        public List<Book> books { get; set; }

        public ListBook(int pages, List<Book> books)
        {
            this.pages = pages;
            this.books = books;
        }
    }
    public class TakaDB
    {
        TakaDBContext takaDB;

        public TakaDB()
        {
            takaDB = new TakaDBContext();
        }

        public ListBook GetListBook(int page = 1, string text = "", int cate = 0, int sort = 0)
        {
            int pageSize = 20;
            var removeUnicode = HelperFunctions.RemoveUnicode(text);
            var listItem = takaDB.Books.Where(m => m.KeySearch.Contains(removeUnicode));
            if (cate != 0)
            {
                listItem = listItem.Where(m => m.idCategory == cate);
            }

            if (sort != 0)
            {
                if (sort == 1)
                    listItem = listItem.OrderBy(m => m.Price);
                else
                    listItem = listItem.OrderByDescending(m => m.Price);
            }
            else
                listItem = listItem.OrderBy(m => m.ID);

            int maxPage = listItem.Count() / 20 + 1;
            return new ListBook(maxPage, listItem.Skip((page - 1) * pageSize).Take(pageSize).ToList());
        }

        public List<Category> GetCategories()
        {
            return takaDB.Categories.ToList();
        }

        public List<Author> GetAuthors()
        {
            return takaDB.Authors.ToList();
        }
        public List<Publisher> GetPublishers()
        {
            return takaDB.Publishers.ToList();
        }
        public List<Language> GetLanguages()
        {
            return takaDB.Languages.ToList();
        }
        public string findTextCategory(int id)
        {
            return takaDB.Categories.Where(x => x.ID == id).First().Name;
        }

        public Book GetBookDetail(int id)
        {
            return takaDB.Books.Where(x => x.ID == id).First();
        }
    }
}