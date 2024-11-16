using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Models.Repositories
{
    public class AuthorDbRepository : IBookStoreRepository<Author>
    {
        BookStoreDbContext db;
        public AuthorDbRepository(BookStoreDbContext _db)
        {
            db = _db;
        }

        public void Add(Author entity)
        {
            db.Authors.Add(entity);
            db.SaveChanges();


        }


        public void delete(int ID)
        {
            var author = find(ID);
            db.Authors.Remove(author);
            db.SaveChanges();
        }

        public Author find(int ID)
        {
            var author = db.Authors.SingleOrDefault(a => a.ID == ID);
            return author;
        }

        public IList<Author> list()
        {
            return db.Authors.ToList();
        }

        public List<Author> Search(string term)
        {
            return db.Authors.Where(a => a.FullName.Contains(term)).ToList();        }

        public void update(int ID, Author NewAuthor)
        {
            db.Update(NewAuthor);
            db.SaveChanges();
        }

    }
}
