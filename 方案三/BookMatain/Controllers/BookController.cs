using BookMatain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using 方案三.Models;

namespace 方案三.Controllers
{
    public class BookController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {   
            CodeService codeService = new CodeService();
            ViewBag.BookClassCode = codeService.GetBookClassData();
            ViewBag.BookStatus = codeService.GetBookStatus();
            ViewBag.BookKeeper = codeService.GetBookKeeperData();
            BookSerchArg SerchArg = new BookSerchArg();
            ViewBag.StartTime = SerchArg.BookStartTime;
            ViewBag.StartTime = SerchArg.BookEndTime;

            return View();
        }
        [HttpPost]
        public IActionResult Index(BookSerchArg bookSerchArg)
        {
            CodeService codeService = new CodeService();
            ViewBag.BookClassCode = codeService.GetBookClassData();
            ViewBag.BookStatus = codeService.GetBookStatus();
            ViewBag.BookKeeper = codeService.GetBookKeeperData();
            BookSerchArg SerchArg = new BookSerchArg();
            ViewBag.StartTime = SerchArg.BookStartTime;
            ViewBag.EndTime = SerchArg.BookEndTime;
            BookService bookService = new BookService();
            ViewBag.SearchResult = bookService.FilterBook(bookSerchArg);
            return View();
        }
        [HttpPost]
        public IActionResult Delete(string BookID)
        {
            BookService bookService = new BookService();
            bookService.DeleteBook(BookID);
            return Json(new { success = true });
        }


        [HttpGet]
        public IActionResult Create() 
        {
            CodeService codeService = new CodeService();
            ViewBag.BookClassCode = codeService.GetBookClassData();
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateBookData bookData)
        {
            BookService bookService = new BookService();
            //ModelState.Remove("Status");
            if (ModelState.IsValid)
            {
                bookService.InsertBook(bookData);
            }
            CodeService codeService = new CodeService();
            ViewBag.BookClassCode = codeService.GetBookClassData();
            return View();
        }
        [HttpGet]
        public IActionResult Update(string BookID)
        {
            CodeService codeService = new CodeService();
            ViewBag.BookClassCode = codeService.GetBookClassData();
            ViewBag.BookStatus = codeService.GetBookStatus();
            ViewBag.BookKeeper = codeService.GetBookKeeperData();
            BookService bodeService = new BookService();
            var Book = bodeService.GetBookData(BookID);
            return View(Book);
        }
        [HttpPost]
        public IActionResult Update( BookDataForEdit bookData)
        {
            BookService bookService = new BookService();
            if (ModelState.IsValid)
            {
               bookService.UpdateBook(bookData);
            }
            CodeService codeService = new CodeService();
            ViewBag.BookClassCode = codeService.GetBookClassData();
            ViewBag.BookStatus = codeService.GetBookStatus();
            ViewBag.BookKeeper = codeService.GetBookKeeperData();
            //重啟後還在同個id
            return RedirectToAction("Update", new { BookID = bookData.BookID });
        }
        [HttpGet]
        public IActionResult Detail(string BookID)
        {
            BookService bookService = new BookService();
            var Book = bookService.GetBookData(BookID);
            return View(Book);
        }

    }
}
