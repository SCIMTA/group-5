using System;
using System.Collections.Generic;
using System.Linq;
using taka.Models.Enitities;
using taka.Utils;

namespace taka.Models.DatabaseInteractive
{
    public class ListBook
    {
        public int pages { get; set; }
        public Category cate { get; set; }

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
            int pageSize = 16;
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

            int maxPage = listItem.Count() / pageSize + 1;
            return new ListBook(maxPage, listItem.Skip((page - 1) * pageSize).Take(pageSize).ToList());
        }

        public List<ListBook> GetHomePage()
        {
            int pageSize = 10;
            var listItem = takaDB.Books;
            List<ListBook> list = new List<ListBook>();
            foreach (var cate in GetCategories())
            {
                ListBook listBook =new ListBook(0, listItem.Where(m => m.idCategory == cate.ID).OrderBy(m => m.ID).Take(pageSize).ToList());
                listBook.cate = cate;
                list.Add(listBook);
            }
            return list;
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

        public List<Enitities.Type> GetTypes()
        {
            return takaDB.Types.ToList();
        }

        public List<User> GetUsers()
        {
            return takaDB.Users.Where(x => !x.Phone.Equals("admin")).ToList();
        }

        public string findTextCategory(int id)
        {
            return takaDB.Categories.Where(x => x.ID == id).First().Name;
        }

        public Book GetBookDetail(int id)
        {
            return takaDB.Books.Where(x => x.ID == id).First();
        }

        public User Register(string phone, string password, string email = "", string gender = "", string fullname = "", string birthday = "")
        {
            if (takaDB.Users.Where(x => x.Phone == phone).Count() > 0)
                return null;
            User user = new User();
            user.Phone = phone.Replace("+84", "0");
            user.Password = HelperFunctions.sha256(password);
            user.Email = email;
            user.Fullname = fullname;
            user.Gender = gender;
            user.Birthday = birthday.Length == 0 ? DateTime.Now.ToShortDateString() : birthday;
            takaDB.Users.Add(user);
            takaDB.SaveChanges();
            return user;
        }

        public void UpdateUser(string phone, string email, string fullname, string gender, string birthday)
        {
            User user = takaDB.Users.Where(x => x.Phone == phone).First();
            if (user == null)
                return;
            user.Email = email;
            user.Fullname = fullname;
            user.Gender = gender;
            user.Birthday = birthday.Length == 0 ? DateTime.Now.ToShortDateString() : birthday;
            takaDB.SaveChanges();
        }

        public void UpdateCategory(int id, string name)
        {
            Category cate = takaDB.Categories.Where(x => x.ID == id).First();
            if (cate == null)
                return;
            cate.Name = name;
            takaDB.SaveChanges();
        }

        public void AddCategory(string name)
        {
            if (takaDB.Categories.Where(e => e.Name.Equals(name)).Count() > 0)
                return;
            Category cate = new Category();
            cate.Name = name;
            takaDB.Categories.Add(cate);
            takaDB.SaveChanges();
        }

        public void RemoveCategory(int id)
        {
            try
            {
                Category cate = takaDB.Categories.Where(x => x.ID == id).First();
                takaDB.Categories.Remove(cate);
                takaDB.SaveChanges();
            }
            catch (Exception)
            {

            }
        }

        public void UpdateLanguage(int id, string name)
        {
            Language lang = takaDB.Languages.Where(x => x.ID == id).First();
            if (lang == null)
                return;
            lang.Name = name;
            takaDB.SaveChanges();
        }

        public void AddLanguage(string name)
        {
            if (takaDB.Languages.Where(e => e.Name.Equals(name)).Count() > 0)
                return;
            Language lang = new Language();
            lang.Name = name;
            takaDB.Languages.Add(lang);
            takaDB.SaveChanges();
        }

        public void RemoveLanguage(int id)
        {
            try
            {
                Language lang = takaDB.Languages.Where(x => x.ID == id).First();
                takaDB.Languages.Remove(lang);
                takaDB.SaveChanges();
            }
            catch (Exception)
            {

            }
        }

        public void UpdatePublisher(int id, string name)
        {
            Publisher pub = takaDB.Publishers.Where(x => x.ID == id).First();
            if (pub == null)
                return;
            pub.Name = name;
            takaDB.SaveChanges();
        }

        public void AddPublisher(string name)
        {
            if (takaDB.Publishers.Where(e => e.Name.Equals(name)).Count() > 0)
                return;
            Publisher pub = new Publisher();
            pub.Name = name;
            takaDB.Publishers.Add(pub);
            takaDB.SaveChanges();
        }

        public void RemovePublisher(int id)
        {
            try
            {
                Publisher pub = takaDB.Publishers.Where(x => x.ID == id).First();
                takaDB.Publishers.Remove(pub);
                takaDB.SaveChanges();
            }
            catch (Exception)
            {

            }
        }



        public void UpdateAuthor(int id, string name)
        {
            Author author = takaDB.Authors.Where(x => x.ID == id).First();
            if (author == null)
                return;
            author.Name = name;
            takaDB.SaveChanges();
        }

        public void AddAuthor(string name)
        {
            if (takaDB.Publishers.Where(e => e.Name.Equals(name)).Count() > 0)
                return;
            Author author = new Author();
            author.Name = name;
            takaDB.Authors.Add(author);
            takaDB.SaveChanges();
        }

        public void RemoveAuthor(int id)
        {
            try
            {
                Author author = takaDB.Authors.Where(x => x.ID == id).First();
                takaDB.Authors.Remove(author);
                takaDB.SaveChanges();
            }
            catch (Exception)
            {

            }
        }


        public void UpdateType(int id, string name)
        {
            Enitities.Type type = takaDB.Types.Where(x => x.ID == id).First();
            if (type == null)
                return;
            type.Name = name;
            takaDB.SaveChanges();
        }

        public void AddType(string name)
        {
            if (takaDB.Types.Where(e => e.Name.Equals(name)).Count() > 0)
                return;
            Enitities.Type author = new Enitities.Type();
            author.Name = name;
            takaDB.Types.Add(author);
            takaDB.SaveChanges();
        }

        public void RemoveType(int id)
        {
            try
            {
                Enitities.Type author = takaDB.Types.Where(x => x.ID == id).First();
                takaDB.Types.Remove(author);
                takaDB.SaveChanges();
            }
            catch (Exception)
            {

            }
        }


        public User Login(string phone, string password)
        {
            string hashpass = HelperFunctions.sha256(password);
            var user = takaDB.Users.Where(x => x.Phone == phone && x.Password == hashpass);
            if (user.Count() > 0)
                return user.First();
            return null;
        }

        public void AddCart(int idBook, int idUser, int quantity=1)
        {
            var find_cart = takaDB.Carts.Where(x => x.idBook == idBook && x.idUser == idUser);
            if (find_cart.Count() > 0)
            {
                find_cart.First().Quantity+= quantity;
            }
            else
            {
                Cart cart = new Cart();
                cart.idBook = idBook;
                cart.idUser = idUser;
                cart.Quantity = quantity;
                takaDB.Carts.Add(cart);
            }
            takaDB.SaveChanges();
        }

        public bool DeleteBook(int id)
        {
            return true;
        }


    }
}