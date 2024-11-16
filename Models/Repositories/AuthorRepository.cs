using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Models.Repositories
{
    public class AuthorRepository:IBookStoreRepository<Author>
    {
        List<Author> Authors;
        public AuthorRepository()
        {
            Authors = new List<Author>()
            {
                new Author
                {
                    ID=1,FullName="Alaa"
                },
                new Author
                {
                    ID=2,FullName="Mohamed"
                },
                new Author
                {
                    ID=3,FullName="Alaaaa"
                }
            };
        }

        public void Add(Author entity)
        {
            entity.ID = Authors.Max(a => a.ID) + 1;
            Authors.Add(entity);




        }
   

        public void delete(int ID)
        {
            var author = find(ID);
            Authors.Remove(author);
        }

        public Author find(int ID)
        {
            var author = Authors.SingleOrDefault(a => a.ID == ID);
            return author;
        }

        public IList<Author> list()
        {
            return Authors;
        }

        public List<Author> Search(string term)
        {
            return Authors.Where(a => a.FullName.Contains(term)).ToList();
        }

        public void update(int ID, Author NewAuthor)
        {
            var author = find(ID);
            author.FullName =NewAuthor.FullName;
        }




    }
}
