using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LAB7.Data;
using LAB7.Models;

namespace LAB7.Pages_Rings
{
    public class DeleteModel : PageModel
    {
        private readonly LAB7.Data.MyContext _context;

        public DeleteModel(LAB7.Data.MyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Ring Ring { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ring = await _context.Ring.SingleOrDefaultAsync(m => m.ID == id);

            if (Ring == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ring = await _context.Ring.FindAsync(id);

            if (Ring != null)
            {
                _context.Ring.Remove(Ring);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
