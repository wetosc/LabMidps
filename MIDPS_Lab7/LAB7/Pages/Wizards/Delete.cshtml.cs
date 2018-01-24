using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LAB7.Data;
using LAB7.Models;

namespace LAB7.Pages_Wizards
{
    public class DeleteModel : PageModel
    {
        private readonly LAB7.Data.MyContext _context;

        public DeleteModel(LAB7.Data.MyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Wizard Wizard { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Wizard = await _context.Wizard.SingleOrDefaultAsync(m => m.ID == id);

            if (Wizard == null)
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

            Wizard = await _context.Wizard.FindAsync(id);

            if (Wizard != null)
            {
                _context.Wizard.Remove(Wizard);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
