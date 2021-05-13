using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using taka.Models.Enitities;
using taka.Utils;

namespace taka.Models.DatabaseInteractive
{
    public class BillInfo
    {
        int id { get; set; }
        int price { get; set; }
        int quantity { get; set; }
    }
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

        public ListBook GetListBook(int page = 1, string text = "", int cate = 0, int sort = 0, int pageSize = 16, int type = 0, int language = 0)
        {
            var removeUnicode = HelperFunctions.RemoveUnicode(text);
            var listItem = takaDB.Books.Where(x => x.isHidden != 1);
            listItem = listItem.Where(m => m.KeySearch.Contains(removeUnicode));
            if (cate != 0)
                listItem = listItem.Where(m => m.idCategory == cate);

            if (type != 0)
                listItem = listItem.Where(m => m.idType == type);

            if (language != 0)
                listItem = listItem.Where(m => m.idLanguage == language);

            if (sort != 0)
            {
                if (sort == 1)
                    listItem = listItem.OrderBy(m => m.Price);
                else
                    listItem = listItem.OrderByDescending(m => m.Price);
            }
            else
                listItem = listItem.OrderByDescending(m => m.ID);

            int maxPage = listItem.Count() / pageSize + 1;
            return new ListBook(maxPage, listItem.Skip((page - 1) * pageSize).Take(pageSize).ToList());
        }

        public List<ListBook> GetHomePage()
        {
            int pageSize = 10;
            var listItem = takaDB.Books.Where(x => x.isHidden != 1);
            List<ListBook> list = new List<ListBook>();
            foreach (var cate in GetCategories())
            {
                ListBook listBook = new ListBook(0, listItem.Where(m => m.idCategory == cate.ID).OrderBy(m => m.ID).Take(pageSize).ToList());
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
            return takaDB.Users.Where(x => !x.ID.Equals(C.ID_ADMIN)).ToList();
        }

        public string findTextCategory(int id)
        {
            return takaDB.Categories.Where(x => x.ID == id).First().Name;
        }

        public Book GetBookDetail(int id)
        {
            return takaDB.Books.Where(x => x.ID == id && x.isHidden != 1).First();
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

        public User GetUserDetail(int id)
        {
            var user = takaDB.Users.Where(x => x.ID == id).First();
            return user;
        }

        public User UpdateUser(string phone, string email, string fullname, string gender, string birthday)
        {
            User user = takaDB.Users.Where(x => x.Phone == phone).First();
            if (user == null)
                return null;
            user.Email = email;
            user.Fullname = fullname;
            user.Gender = gender;
            user.Birthday = birthday.Length == 0 ? DateTime.Now.ToShortDateString() : birthday;
            takaDB.SaveChanges();
            return user;
        }
        public void BanUser(int ID, int ban = 0)
        {
            User user = takaDB.Users.Where(x => x.ID == ID).First();
            if (user == null)
                return;
            user.is_ban = ban;
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
        public User LoginWithGoogle(string gooogleId, string fullname, string email)
        {
            var user = takaDB.Users.Where(x => x.google_id == gooogleId);
            if (user.Count() > 0)
                return user.First();
            User newUser = new User();
            newUser.google_id = gooogleId;
            newUser.Fullname = fullname;
            newUser.Email = email;
            takaDB.Users.Add(newUser);
            takaDB.SaveChanges();
            return newUser;
        }

        public void AddCart(int idBook, int idUser, int quantity = 1)
        {
            var find_cart = takaDB.Carts.Where(x => x.idBook == idBook && x.idUser == idUser);
            if (find_cart.Count() > 0)
            {
                find_cart.First().Quantity += quantity;
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
        public List<Cart> GetListCarts(int idUser)
        {
            var listCarts = takaDB.Carts.Where(x => x.idUser == idUser).ToList();
            return listCarts;
        }
        public void DeleteCartItem(int idUser, int idBook)
        {
            Cart deleteItem = takaDB.Carts.Where(x => x.idUser == idUser && x.idBook == idBook).First();
            takaDB.Carts.Remove(deleteItem);
            takaDB.SaveChanges();
        }
        public bool DeleteBook(int id, bool permanently = false)
        {
            var item = takaDB.Books.Where(x => x.ID == id).First();
            if (item != null)
                if (!permanently)
                {
                    item.isHidden = 1;
                    takaDB.SaveChanges();
                }
                else
                {
                    takaDB.Carts.RemoveRange(takaDB.Carts.Where(x => x.idBook == id));
                    takaDB.Images.RemoveRange(takaDB.Images.Where(x => x.idBook == id));
                    takaDB.Rates.RemoveRange(takaDB.Rates.Where(x => x.idBook == id));
                    takaDB.OrderDetails.RemoveRange(takaDB.OrderDetails.Where(x => x.idBook == id));
                }
            return true;
        }


        public Book AddBook(IEnumerable<HttpPostedFileBase> Images, string Title,
            int Price,
            int idCategory,
            int idAuthor,
            int idPublisher,
            int idLanguage,
            int idType,
            string Page,
            string Date,
            int Quantity,
            string Description)
        {
            Book book = new Book();
            book.Title = Title;
            book.Price = Price;
            book.idCategory = idCategory;
            book.idAuthor = idAuthor;
            book.idPublisher = idPublisher;
            book.idLanguage = idLanguage;
            book.idType = idType;
            book.Page = Page;
            book.Date = Date;
            book.Quantity = Quantity;
            book.Description = Description;
            book.RateCount = 0;
            book.RatePoint = 0;
            takaDB.Books.Add(book);
            takaDB.SaveChanges();
            if (Images != null && Images.Count() > 0)
            {
                foreach (var image in Images)
                {
                    try
                    {
                        MemoryStream target = new MemoryStream();
                        image.InputStream.CopyTo(target);
                        byte[] data = target.ToArray();
                        var client = new RestClient("http://128.199.108.177:8001/upload_image");
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("Content-Type", "multipart/form-data");
                        request.AlwaysMultipartFormData = true;
                        request.AddFile("book_cover", data, "image.jpeg");
                        IRestResponse response = client.Execute(request);
                        string resJsonRaw = response.Content;
                        JObject json = JObject.Parse(resJsonRaw);
                        Image imgObj = new Image();
                        imgObj.idBook = book.ID;
                        imgObj.Url = json.GetValue("url").ToString();
                        takaDB.Images.Add(imgObj);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            takaDB.SaveChanges();
            return book;
        }


        public Book EditBook(int ID,
            IEnumerable<int> images_delete,
            IEnumerable<HttpPostedFileBase> Images,
            string Title,
            int Price,
            int idCategory,
            int idAuthor,
            int idPublisher,
            int idLanguage,
            int idType,
            string Page,
            string Date,
            int Quantity,
            string Description)
        {
            if (images_delete != null)
                takaDB.Images.RemoveRange(takaDB.Images.Where(x => images_delete.Contains(x.ID)));
            Book book = takaDB.Books.Where(x => x.ID == ID).First();
            book.Title = Title;
            book.Price = Price;
            book.idCategory = idCategory;
            book.idAuthor = idAuthor;
            book.idPublisher = idPublisher;
            book.idLanguage = idLanguage;
            book.idType = idType;
            book.Page = Page;
            book.Date = Date;
            book.Quantity = Quantity;
            book.Description = Description;
            takaDB.SaveChanges();
            if (Images != null && Images.Count() > 0)
            {
                foreach (var image in Images)
                {
                    try
                    {
                        MemoryStream target = new MemoryStream();
                        image.InputStream.CopyTo(target);
                        byte[] data = target.ToArray();
                        var client = new RestClient("http://128.199.108.177:8001/upload_image");
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("Content-Type", "multipart/form-data");
                        request.AlwaysMultipartFormData = true;
                        request.AddFile("book_cover", data, "image.jpeg");
                        IRestResponse response = client.Execute(request);
                        string resJsonRaw = response.Content;
                        JObject json = JObject.Parse(resJsonRaw);
                        Image imgObj = new Image();
                        imgObj.idBook = ID;
                        imgObj.Url = json.GetValue("url").ToString();
                        takaDB.Images.Add(imgObj);
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            takaDB.SaveChanges();
            return book;
        }

        public void ChangeQuantity(int idCart, int quantity)
        {
            takaDB.Carts.Where(x => x.ID == idCart).First().Quantity = quantity;
            takaDB.SaveChanges();
        }

    }
}