using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Models.Repositories
{
    public class BookRepository:IBookStoreRepository<Book>

    {

        List<Book> Books;
        public BookRepository()
        {
            Books = new List<Book>()
            {
                new Book()
                {
                    ID=1,
                    Description="NoDescription",
                    Title="C#",
                    Image ="OIP.jpg",
                    Author=new Author(){ID=2  }
                },
                new Book()
                {
                    ID=2,
                    Description="NoDescription",
                    Title="Java" ,
                    Image ="OIP.jpg",
                    Author=new Author()
                } 
                ,new Book()
                {
                    ID=3,
                    Description="NoDescription",
                    Title="Java",
                    Image ="OIP.jpg",
                    Author=new Author()
                }
            };
        }

        public void Add(Book entity)
        {
            entity.ID = Books.Max(b => b.ID) + 1;
            Books.Add(entity);
          
        }




        public void delete(int ID)
        {
            var book = find(ID);
            Books.Remove(book);
        }

        public Book find(int ID)
        {
            var book = Books.SingleOrDefault(b => b.ID == ID);
            return book;


        }

        public IList<Book> list()
        {
            return Books;
        }

        public List<Book> Search(string term)
        {
            return Books.Where(a => a.Title.Contains(term)).ToList();

        }

        public void update(int ID, Book NewBook)
        {
            var book = find(ID);
            book.Description = NewBook.Description;
            book.Author = NewBook.Author;
            book.Title = NewBook.Title;
            book.Image = NewBook.Image;
        }


    }


}
