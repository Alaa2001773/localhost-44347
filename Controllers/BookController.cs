using BooksStore.Models;
using BooksStore.Models.Repositories;
using BooksStore.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookStoreRepository<Book> bookRepository;
        private readonly IBookStoreRepository<Author> authorRepository;
        private readonly IHostingEnvironment hosting;


        //wcf


        // GET: BookController
        public BookController(IBookStoreRepository<Book> bookRepository ,
            IBookStoreRepository<Author> authorRepository,
            IHostingEnvironment hosting)/* give me permission to handle( upload files)*/
        {
            this.bookRepository =bookRepository;
            this.authorRepository = authorRepository;
            this.hosting = hosting;
        }

        List<Author> FillSelectList()
        {
            var authors = authorRepository.list().ToList();
            authors.Insert(0, new Author { ID = -1, FullName = "---Please Select an Author ---" });
            return authors;
        }

        BookAuthorViewModel GetAllAuthors()
        {
            var vmodel = new BookAuthorViewModel
            {
                Authors = FillSelectList()
            };
            return (vmodel);

        }

        public ActionResult Index()
        {
            var book = bookRepository.list();
            return View(book);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = bookRepository.find(id);

            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var model = new BookAuthorViewModel
            {
                Authors = FillSelectList()

            };
            return View(model);
        }



        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    /*url to file save*/
                    string fileName = string.Empty;
                    if(model.File   != null)
                    {
                        string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                        fileName = model.File.FileName;
                        string fullpath = Path.Combine(uploads, fileName);
                        model.File.CopyTo(new FileStream(fullpath, FileMode.Create));
                        /*webrootpath => wwwRoot upload => iput file, we with this method go to url file */
                    }
                    /*not fount author*/
                    if (model.AuthorId == -1)
                    {
                        ViewBag.Message = "Please Select an Author from the list ";
                
                        return View(GetAllAuthors());

                    }

                    Book book = new Book
                    {
                        ID = model.BookID,
                        Title = model.Title,
                        Description = model.Description,
                        Author = authorRepository.find(model.AuthorId),
                        Image = fileName

                    };
                    bookRepository.Add(book);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }

            }
 
            ModelState.AddModelError("", "You have to fill all the required fields!");
            return View(GetAllAuthors());
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {


            var book = bookRepository.find(id);
            var authorID = book.Author == null ? book.Author.ID = 0 : book.Author.ID;




            //VVM


            var viewModel = new BookAuthorViewModel
            {
                /**/
                BookID = book.ID,
                Title = book.Title,
                Description = book.Description,
                AuthorId = book.Author.ID,
                notes = book.Description + book.Image + book.Title,
                Authors = authorRepository.list().ToList(),
                Image = book.Image
            };

            return View(viewModel);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookAuthorViewModel ViewModel)
        {
            try
            {
                // تعيين اسم الملف الافتراضي ليكون اسم الملف القديم
                string oldFileName = ViewModel.Image;
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");

                // تعيين اسم الملف الافتراضي ليكون اسم الملف القديم
                string fileName = oldFileName; // استخدام اسم الملف القديم بشكل افتراضي

                // التحقق إذا كان هناك ملف جديد مرفوع
                if (ViewModel.File != null)
                {
                    string newFileName = Path.GetFileName(ViewModel.File.FileName); // الحصول على اسم الملف الجديد
                    string fullNewPath = Path.Combine(uploads, newFileName);
                    string fullOldPath = Path.Combine(uploads, oldFileName);

                    // التحقق مما إذا كانت الصورة الجديدة هي نفس الصورة القديمة
                    if (!string.Equals(newFileName, oldFileName, StringComparison.OrdinalIgnoreCase))
                    {
                        // حفظ الملف الجديد فقط إذا كان مختلفًا
                        using (var stream = new FileStream(fullNewPath, FileMode.Create))
                        {
                            ViewModel.File.CopyTo(stream);
                        }
                        fileName = newFileName; // تحديث اسم الملف إذا تم تحميل ملف جديد
                    }
                    else
                    {
                        // إذا كانت الصورة الجديدة هي نفس القديمة، استخدم القديمة
                        Console.WriteLine("Dont any thing .");
                    }
                }

                // تحديث بيانات الكتاب
                var author = authorRepository.find(ViewModel.AuthorId);
                Book book = new Book
                {
                    ID = ViewModel.BookID,
                    Title = ViewModel.Title,
                    Description = ViewModel.Description,
                    Author = author,
                    Image = fileName  // استخدام اسم الملف الصحيح، سواءً كان قديمًا أو جديدًا
                };

                bookRepository.update(ViewModel.BookID, book);
                return RedirectToAction(nameof(Index));
            }
            
            catch (Exception ex)
            {
                Console.WriteLine(ex);
               
                return View();
            }
        }


        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookRepository.find(id);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                bookRepository.delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }

      public ActionResult Search(string term)
        {
            var result = bookRepository.Search(term);
            return View("Index", result);
        }
    }

    }

