using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuanLiSach.Models;
using QuanLiSach.Repositories;
using System.Threading.Tasks;

namespace QuanLiSach.Controllers
{
    public class BookController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBookRepository _bookRepository;
        public BookController(IBookRepository bookRepository, ICategoryRepository categoryRepository)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
        }

        //show all
        public async Task <IActionResult> Index()
        {
            var books = await _bookRepository.GetAllAsync();
            return View(books);
        }
        public async Task <IActionResult> Add() 
        {
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        //add book
        [HttpPost]
        public async Task <IActionResult> Add(Book book)
        {
            if (ModelState.IsValid)
            {
                await _bookRepository.AddAsync(book);
                return RedirectToAction("Index");
            }
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(book);
        }

        public async Task<IActionResult> Update(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", book.CategoryId);
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, Book book, IFormFile? coverFile)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            ModelState.Remove("coverFile");
            ModelState.Remove("Cover");

            if (ModelState.IsValid)
            {
                if (coverFile != null && coverFile.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(coverFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await coverFile.CopyToAsync(stream);
                    }

                    book.Cover = fileName;
                }

                await _bookRepository.UpdateAsync(book);
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", book.CategoryId);
            return View(book);
        }

        // detail book
        public async Task <IActionResult> Detail(int id) 
        { 
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // delete book
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
