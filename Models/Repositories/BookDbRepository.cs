using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Models.Repositories
{
    public class BookDbRepository : IBookStoreRepository<Book>

    {
        BookStoreDbContext db;
        public BookDbRepository(BookStoreDbContext _db)
        {
            db = _db;
        }
       
        public void Add(Book entity)
        {
            db.Books.Add(entity);
            db.SaveChanges();

        }

        public void delete(int ID)
        {
            var book = find(ID);
            db.Books.Remove(book);
            db.SaveChanges();
        }

        public Book find(int ID)
        {
            var book = db.Books.Include(a=>a.Author).SingleOrDefault(b => b.ID == ID);
            return book;


        }

        public IList<Book> list()
        {
            return db.Books.Include(a=>a.Author).ToList();
        }

        public void update(int ID, Book NewBook)
        {
            db.Update(NewBook);
            db.SaveChanges();
        }

        public List<Book> Search(string term)
        {
            var result = db.Books.Include(a => a.Author)
                .Where(b => b.Description.Contains(term)
            || b.Title.Contains(term)
            || b.Author.FullName.Contains(term)).ToList();
            return result;
        }

    }


}
