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
    public class IndexModel : PageModel
    {
        private readonly LAB7.Data.MyContext _context;

        public IndexModel(LAB7.Data.MyContext context)
        {
            _context = context;
        }

        public IList<Ring> Ring { get;set; }

        public async Task OnGetAsync()
        {
            Ring = await _context.Ring.ToListAsync();
        }
    }
}
