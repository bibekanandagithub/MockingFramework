using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor
{
    
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IndexModel(ApplicationDbContext db)
        {

            _db = db;
        }
        public List<Books> books { set; get; }
        public async Task OnGet()
        {
            books = await _db.book.ToListAsync();
        }
        
        public async Task<IActionResult> OnPostDelete(int id)
        {
            var book = await _db.book.FindAsync(id);
            if(book==null)
            {
                return NotFound();
            }
            else
            {
                _db.book.Remove(book);
              await  _db.SaveChangesAsync();
                return RedirectToPage("Index");

            }
        }
        
    }
}