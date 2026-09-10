using Microsoft.AspNetCore.Mvc;
using QuanLiSach.Models;

namespace QuanLiSach.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        //show all
        public IActionResult Index()
        {
            var books = _bookRepository.GetAll();
            return View(books);
        }
        public IActionResult Add() 
        {
            return View();
        }
        [HttpPost]

        //add book
        public IActionResult Add(Book book)
        {
            if (ModelState.IsValid)
            {
                _bookRepository.Add(book);
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // update book
        [HttpPost]
        public IActionResult Update(Book book)
        {
            if (ModelState.IsValid)
            {
                _bookRepository.Update(book);
                return RedirectToAction("Index");
            }
            return View(book);
        }
        public IActionResult Update(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // detail book
        public IActionResult Detail(int id) 
        { 
            var book = _bookRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // delete book
        public IActionResult Delete(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            _bookRepository.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
    }
}
