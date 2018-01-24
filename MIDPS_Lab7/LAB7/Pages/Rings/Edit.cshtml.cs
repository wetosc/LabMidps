using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LAB7.Data;
using LAB7.Models;

namespace LAB7.Pages_Rings
{
    public class EditModel : PageModel
    {
        private readonly LAB7.Data.MyContext _context;

        public EditModel(LAB7.Data.MyContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Ring).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RingExists(Ring.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RingExists(int id)
        {
            return _context.Ring.Any(e => e.ID == id);
        }
    }
}
