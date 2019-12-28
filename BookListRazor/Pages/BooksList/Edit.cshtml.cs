using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor
{
    public class EditModel : PageModel
    {
        private ApplicationDbContext _db; 
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Books books { set; get; }

        public async Task OnGet(int ID)
        {
            books = await _db.book.FindAsync(ID);
        }
        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                var booksFromDb = await _db.book.FindAsync(books.ID);
                booksFromDb.Name = books.Name;
                booksFromDb.ISBN = books.ISBN;
                booksFromDb.Author = books.Author;
                await _db.SaveChangesAsync();
                return  RedirectToPage("Index");
            }
            else
            {
                return RedirectToPage("Index");
            }
        }
    }
}