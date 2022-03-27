using BooksApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BooksApp.Controllers
{
    public class BookController : Controller
    {
        private readonly BooksDBContext _context;

        public BookController(BooksDBContext context)
        {
            _context = context;
        }

        // GET: BookController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> Index()
        {
            var books = await _context.Books.ToListAsync();
            return View(books);
        }

        // GET: BookController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        public async Task<IActionResult> Details (int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: BookController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        public async Task<IActionResult> AddOrEdit(int? Id)
        {
            ViewBag.PageName = Id == null ? "Create Book" : "Edit Book";
            ViewBag.IsEdit = Id == null ? false : true;
            if (Id == null)
            {
                return View();
            }
            else
            {
                var book = await _context.Books.FindAsync(Id);

                if (book == null)
                {
                    return NotFound();
                }
                return View(book);
            }
        }

        // POST: BookController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int Id, [Bind("Id,Title,Author,PublishngYear,Style")]
        Book bookData)
        {
            bool IsBookExist = false;

            Book book = await _context.Books.FindAsync(Id);

            if (book != null)
            {
                IsBookExist = true;
            }
            else
            {
                book = new Book();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    book.Title = bookData.Title;
                    book.Author = bookData.Author;
                    book.PublishngYear = bookData.PublishngYear;
                    book.Style = bookData.Style;
                    

                    if (IsBookExist)
                    {
                        _context.Update(book);
                    }
                    else
                    {
                        _context.Add(book);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bookData);
        }

        // GET: BookController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: BookController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: BookController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        public async Task<IActionResult> Delete (int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == Id);

            return View(book);
        }

        // POST: BookController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            var book = await _context.Books.FindAsync(Id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
