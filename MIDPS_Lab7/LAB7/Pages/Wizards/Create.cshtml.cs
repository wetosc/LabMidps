using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LAB7.Data;
using LAB7.Models;

namespace LAB7.Pages_Wizards
{
    public class CreateModel : PageModel
    {
        private readonly LAB7.Data.MyContext _context;

        public CreateModel(LAB7.Data.MyContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Wizard Wizard { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Wizard.Add(Wizard);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}